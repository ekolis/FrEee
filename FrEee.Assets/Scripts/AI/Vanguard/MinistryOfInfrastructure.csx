#r "../../../bin/Debug/FrEee.Core.dll"
#load "Plan.csx"
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic; 
using System.Linq;
using FrEee.Objects.Orders;
using FrEee.Objects.Vehicles;
using FrEee.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Objects.Commands;
using FrEee.Objects.Technology;


/// <summary>
/// Class devoted to the management of facilities.  
/// </summary>
public class MinistryOfInfrastructure
{
    public Empire Empire;

    public Galaxy Galaxy;

    public List<FacilityTemplate> AvailableFacilities; 


    public void Run(Empire empire, Galaxy galaxy)
    {
        Empire = empire;
        Galaxy = galaxy;
        //get the latest versions of each facility. 
        AvailableFacilities = Empire.Current.UnlockedItems.OfType<FacilityTemplate>()
            .Where(f => f.ID == 0 && f.Cost.Any()).OnlyLatestVersions(f => f.Family).ToList();

        ConstructNewFacilities();

    }


    /// <summary>
    /// Construct facilities on any world where there are empty slots. 
    /// </summary>
    protected void ConstructNewFacilities()
    {
        var emptySlots = Empire.ColonizedPlanets.Where(x => x.Colony.ConstructionQueue.FacilitySlotsFree > 0); 

        foreach(var planet in emptySlots)
        {
            var colony = planet.Colony; 
            while(colony.ConstructionQueue.FacilitySlotsFree > 0)
            {
                var chosenFacility = GetEmpireVitalFaciltiesForSystem(planet.StarSystem);
                if (chosenFacility == null)
                    chosenFacility = GetPlanetVitalFacilities(planet);
                //TODO: add in non-vital system and planet facility checks. Ie; Resource boosters. Is it worth building one etc. 
                //Might want that to be facility replacement? 
                if (chosenFacility == null)
                    chosenFacility = GetResourceFacility(planet);
                if (chosenFacility == null)
                    continue; 
                

                var order = new ConstructionOrder<Facility, FacilityTemplate> { Template = chosenFacility };
                colony.ConstructionQueue.Orders.Add(order);
                var cmd = new AddOrderCommand
                (
                    colony.ConstructionQueue,
                    order
                );
                Empire.Commands.Add(cmd);
                Empire.LogAIMessage($"Construction of a {chosenFacility.Name} on {planet.Name} has been ordered");
            }
        }
    }


    /// <summary>
    /// Ensure that all vital system options are taken care of. 
    /// </summary>
    /// <param name="starSystem"></param>
    /// <returns></returns>
    protected FacilityTemplate GetEmpireVitalFaciltiesForSystem(StarSystem starSystem)
    {
        //spaceports. 
        var option = AvailableFacilities.FirstOrDefault(x => x.HasAbility("Spaceport"));
        if (option != null && !HasStarSystemFacility(starSystem, "Spaceport"))
            return option;
        //resupply. 
        option = AvailableFacilities.FirstOrDefault(x => x.HasAbility("Supply Generation"));
        if (option != null && !HasStarSystemFacility(starSystem, "Supply Generation"))
            return option;
        //spaceyard. Make sure every system has at least one. 
        //TODO; mod might make more than one facility be a space yard... make sure to select the right one. 
        option = AvailableFacilities.FirstOrDefault(x => x.HasAbility("Space Yard"));
        if (option != null && !HasStarSystemFacility(starSystem, "Space Yard"))
            return option;
        //warp point opening. Lets not let the player drop a fleet into the capital system. 
        option = AvailableFacilities.FirstOrDefault(x => x.HasAbility("Stop Open Warp Point"));
        if (option != null && !HasStarSystemFacility(starSystem, "Stop Open Warp Point"))
            return option;



        return null; 
    }

    /// <summary>
    /// Any facility that would be vital for the planet...
    /// </summary>
    /// <param name="planet"></param>
    /// <returns></returns>
    protected FacilityTemplate GetPlanetVitalFacilities(Planet planet)
    {
        //TODO: handle things that every planet needs. 
        return null; 
    }

    /// <summary>
    /// Chooses what resource facility to build on this planet. 
    /// </summary>
    /// <param name="planet"></param>
    /// <returns></returns>
    protected FacilityTemplate GetResourceFacility(Planet planet, int randMin = 10, int randMax = 100)
    {
        return (from faci in AvailableFacilities
        where (faci.HasAbility("Resource Generation - Minerals")
           || faci.HasAbility("Resource Generation - Organics")
           || faci.HasAbility("Resource Generation - Radioactives")
           || faci.HasAbility("Point Generation - Research")
           || faci.HasAbility("Point Generation - Intelligence"))
           //ensure a little random in the scores, so not always building what is best. 
        orderby GetScoreForFacility(planet, faci) + RandomHelper.Range(randMin, randMax) descending
        select faci).FirstOrDefault(); 

            
    }

    /// <summary>
    /// Gets the score for the given facility. 
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="facilityTemplate"></param>
    /// <returns></returns>
    int GetScoreForFacility(Planet planet, FacilityTemplate facilityTemplate)
    {
        int score = 0;

        var prefix = "Resource Generation - ";
        var pcts = planet.StandardIncomePercentages;
        foreach (var abil in facilityTemplate.Abilities().Where(abil => abil.Rule.Name.StartsWith(prefix)))
        {
            var resourceName = abil.Rule.Name.Substring(prefix.Length);
            var resource = Resource.Find(resourceName);
            var amount = abil.Value1.ToInt();

            if (resource.HasValue)
                amount = Galaxy.Current.StandardMiningModel.GetRate(amount, planet.ResourceValue[resource], pcts[resource] / 100d);

            score += amount + GetResourceScoreMultiplier(resource); 
        }
        prefix = "Point Generation - ";
        foreach (var abil in facilityTemplate.Abilities().Where(abil => abil.Rule.Name.StartsWith(prefix)))
        {
            var resoruceName = abil.Rule.Name.Substring(prefix.Length);
            var resource = Resource.Find(resoruceName);
            var amount = abil.Value1.ToInt() * pcts[resource] / 100;

            score += amount + GetResourceScoreMultiplier(resource); 
        }

        return score; 
    }

    /// <summary>
    /// Modifies the default score of a facility by the current aspects of the empire. 
    /// </summary>
    /// <param name="resourceName"></param>
    /// <returns></returns>
    int GetResourceScoreMultiplier(Resource resourceName)
    {
        var income = Empire.NetIncomeLessConstruction[resourceName]; 
        switch(resourceName.Name)
        {
            case "Minerals":
                if (income < 0)
                    return 10000; 
                if (income < 10000)
                    return 1000;
                if (income < 100000)
                    return 100;
                return 10;
            case "Organics":
                //TODO: add changes for Organic tech. 
                if (income < 0)
                    return 5000;
                if (income < 10000)
                    return 500;
                if (income < 100000)
                    return 50; 
                return 0;
            case "Radioactives":
                //TODO: add changes for Crystal/Temporal tech. 
                if (income < 0)
                    return 5000;
                if (income < 10000)
                    return 500;
                if (income < 100000)
                    return 50;
                return 0;

            case "Research":
                if (income / Empire.ColonizedPlanets.Count() < 1000)
                    return 1000;
                return 100;

            case "Intelligence":
                if (income / Empire.ColonizedPlanets.Count() < 1000)
                    return 1000;
                return 0;
        }

        return 0; 
    }


    /// <summary>
    /// Whether or not the star system has a facility with the given ability. 
    /// </summary>
    /// <param name="starSystem"></param>
    /// <param name="facilityAbilityName"></param>
    /// <returns></returns>
    bool HasStarSystemFacility(StarSystem starSystem, string facilityAbilityName)
    {
        var colonies = starSystem.SpaceObjects.OfType<Planet>().Where(x => x.Owner == Empire); 
        foreach(var planet in colonies)
        {
            var colony = planet.Colony;
            if (colony.Facilities.FirstOrDefault(x => x.HasAbility(facilityAbilityName)) != null)
                return true;
            if (colony.ConstructionQueue.Orders.OfType<ConstructionOrder<Facility, FacilityTemplate>>()
                .FirstOrDefault(x => x.Template.HasAbility(facilityAbilityName)) != null)
                return true; 
        }
        return false; 
    }
}