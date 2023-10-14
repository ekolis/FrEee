#r "../../../bin/Debug/FrEee.Core.dll"
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding.Templates;
using FrEee.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Objects.Vehicles; 
using System.Linq; 

/// <summary>
/// Class devoted to designing ships and units
/// </summary>
public class MinistryOfShipDesign
{
    Empire Empire;
    Galaxy Galaxy;

    public void Run(Empire empire, Galaxy Context)
    {
        Empire = empire;
        Galaxy = Context; 
        if (!empire.EnabledMinisters.ContainsKey("Design Management"))
            return;

        var managementMinisters = empire.EnabledMinisters["Design Management"]; 
        if (managementMinisters.Contains("Ship Design"))
        {
            DepartmentOfShipDesign(); 
        }
    }

    public void DepartmentOfShipDesign()
    {
        //check colony ships designed properly. 
        var empiresDesigns = Empire.KnownDesigns.Where(x => x.Owner == Empire);
        ComponentTemplate componentTemplate;
        if (!empiresDesigns.Any(x => x.Role == "Colonize Planet - Rock") 
            && (componentTemplate = Empire.UnlockedItems.OfType<ComponentTemplate>()
            .FirstOrDefault(x => !x.IsObsolete && x.HasAbility("Colonize Planet - Rock"))) != null)
        {
            DesignColonyShip(componentTemplate); 
        }
        if (!empiresDesigns.Any(x => x.Role == "Colonize Planet - Ice")
            && (componentTemplate = Empire.UnlockedItems.OfType<ComponentTemplate>()
            .FirstOrDefault(x => !x.IsObsolete && x.HasAbility("Colonize Planet - Ice"))) != null)
        {
            DesignColonyShip(componentTemplate);
        }
        if (!empiresDesigns.Any(x => x.Role == "Colonize Planet - Gas")
            && (componentTemplate = Empire.UnlockedItems.OfType<ComponentTemplate>()
            .FirstOrDefault(x => !x.IsObsolete && x.HasAbility("Colonize Planet - Gas Giant"))) != null)
        {
            DesignColonyShip(componentTemplate);
        }

    }


    void DesignColonyShip(ComponentTemplate componentTemplate)
    {
        var components = Empire.UnlockedItems.OfType<ComponentTemplate>().Where(x => !x.IsObsolete);
        var lifeSupport = components.Where(x => x.HasAbility("Ship Life Support")).OrderBy(x => x.Size).First(); 
        var crewquarters = components.Where(x => x.HasAbility("Ship Crew Quarters")).OrderBy(x => x.Size).First();
        var bridge = components.Where(x => x.HasAbility("Ship Bridge")).OrderBy(x => x.Size).First();

        var engine = GetLatestEngine(); 

        var hulls = Empire.UnlockedItems.OfType<IHull<IVehicle>>().Where(x => x.VehicleType == FrEee.Enumerations.VehicleTypes.Ship && !x.IsObsolete);

        var chosenhull = (from hull in hulls
                         where hull.Size > (lifeSupport.Size * hull.MinLifeSupport)
                                            + (crewquarters.Size * hull.MinCrewQuarters)
                                            + bridge.Size + componentTemplate.Size + engine.Size
                               && hull.MinPercentFighterBays == 0 && hull.MinPercentCargoBays == 0
                         orderby hull.Cost select hull).FirstOrDefault();
        if (chosenhull == null)
            return;
        var design = Design.Create(chosenhull);
        design.Role = componentTemplate.Abilities.FirstOrDefault(x => x.Rule.Name.Contains("Colonize Planet"))?.Rule.Name;
        design.AddComponent(componentTemplate);
        for (int idx = 0; idx < chosenhull.MinCrewQuarters; idx++)
            design.AddComponent(crewquarters);
        for (int idx = 0; idx < chosenhull.MinLifeSupport; idx++)
            design.AddComponent(lifeSupport);
        design.AddComponent(bridge);
        int engineNo = 0;
        do
        {
            design.AddComponent(engine);
            engineNo++; 
        } while (design.SpaceFree > engine.Size && engineNo < chosenhull.MaxEngines);

        design.BaseName = RandomName();

        if (design.Warnings.Count() > 0)
        {
            foreach (var warn in design.Warnings)
                Empire.LogAIMessage($"Error in colony design:{warn}"); 
            return; 
        }

        Empire.Commands.Add(design.CreateCreationCommand());
        Empire.KnownDesigns.Add(design); 
    }



    ComponentTemplate GetLatestEngine()
    {
        return Empire.UnlockedItems.OfType<ComponentTemplate>()
            .Where(x => !x.IsObsolete && x.HasAbility("Standard Ship Movement"))
            .OrderByDescending(x =>x.Abilities.FindByName("Movement Bonus")?.Value1).First();
    }

    /// <summary>
    /// Chooses a random name from the name list this empire uses. 
    /// </summary>
    /// <returns></returns>
    string RandomName()
    {
        var name = Empire.DesignNames.Except(Empire.KnownDesigns.Select(x => x.BaseName)).PickRandom();
        if (name == null)
            name = $"Design{Empire.ID}-{Empire.KnownDesigns.Count}";

        return name;  
    }
}

