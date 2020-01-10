//place any extra usings here. 

//Calling Load will execute the script. However, in this case, it will also allow the classes in the script to be called. 
#load "BasicResearchModule.csx"
#load "MinistryOfShipDesign.csx"
#load "MinistryOfConstruction.csx"
#load "Plan.csx"

#r "../../../bin/Debug/FrEee.Core.dll"
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Linq; 

//Note, at present, classes within scripts cannot be stored in the AI Notes as classes. 
//Since they are not recognized as the same class the next time a script is run. 
var AI = new Runner();

return AI.Run(Domain, Context);


public class Runner
{
    Empire empire;

    Galaxy galaxy;
    BasicResearch BasicResearch;
    MinistryOfShipDesign MinistryOfShipDesign;
    MinistryOfConstruction MinistryOfConstruction; 

    public Runner()
    {
        BasicResearch = new BasicResearch();
        MinistryOfShipDesign = new MinistryOfShipDesign();
        MinistryOfConstruction = new MinistryOfConstruction(); 
    }

    public Empire Run(Empire Domain, Galaxy Context)
    {
        try
        {
            galaxy = Context;
            empire = Domain;
            PlanManager.Empire = empire;
            PlanManager.Unpack(); 
            if (empire.EnabledMinisters.ContainsKey("Design Management"))
            {
                MinistryOfShipDesign.Run(empire, galaxy); 
            }

            if (empire.EnabledMinisters.ContainsKey("Empire Management"))
            {
                var managementMinisters = empire.EnabledMinisters["Empire Management"]; 
                if (managementMinisters.Contains("Research"))
                    BasicResearch.Run(empire, galaxy);
            }



            if (empire.EnabledMinisters.ContainsKey("Vehicle Management"))
            {
                var managementMinisters = empire.EnabledMinisters["Vehicle Management"];

                //PlanManager.CurrentPlans.ColonizationPlans.Add(new ColonizationPlan()
                //{
                //    AwaitingBuild = false,
                //    IsComplete = true,
                //    Planet = empire.ExploredStarSystems.ElementAt(0).SpaceObjects.OfType<Planet>().FirstOrDefault(x => !x.HasColony),
                //});

                //PlanManager.CurrentPlans.Add(new ColonizationPlan() { IsComplete = false, Type = "test" }); 
            }


            PlanManager.Pack(); 
        }
        catch (Exception e)
        {
            throw; 
        }
        return empire;  
        
    }


    //void SetResearch()
    //{
    //    if (empire.ResearchQueue.Count() < 3)
    //    {
    //        var cmd = new ResearchCommand();
    //        foreach (var current in empire.ResearchQueue)
    //        {
    //            cmd.Queue.Add(current);
    //        }
    //        cmd.Queue.Add(empire.AvailableTechnologies.ElementAt(0));
    //        empire.ResearchCommand = cmd;
    //    }
    //}
}

