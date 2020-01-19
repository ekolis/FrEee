//place any extra usings here. 

//Calling Load will execute the script. However, in this case, it will also allow the classes in the script to be called. 
#load "BasicResearchModule.csx"
#load "MinistryOfShipDesign.csx"
#load "MinistryOfConstruction.csx"
#load "MinistryOfColonization.csx"
#load "MinistryOfInfrastructure.csx"
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
    MinistryOfColonization MinistryOfColonization;
    MinistryOfInfrastructure MinistryOfInfrastructure; 

    public Runner()
    {
        BasicResearch = new BasicResearch();
        MinistryOfShipDesign = new MinistryOfShipDesign();
        MinistryOfConstruction = new MinistryOfConstruction();
        MinistryOfColonization = new MinistryOfColonization();
        MinistryOfInfrastructure = new MinistryOfInfrastructure(); 
    }

    public Empire Run(Empire Domain, Galaxy Context)
    {
        try
        {
            galaxy = Context;
            empire = Domain;
            PlanManager.Empire = empire;
            PlanManager.Unpack(); 

            if (empire.EnabledMinisters.ContainsKey("Colony Management"))
            {
                //TODO: move this into Empire Management, and remove Colony management entirely. 
                //split up the facility management into construction, upgrades, and replacement? make it its own category? 
                var colonyManagement = empire.EnabledMinisters["Colony Management"];
                if (colonyManagement.Contains("Facility Construction"))
                    MinistryOfInfrastructure.Run(empire, galaxy);
            }

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

                if (managementMinisters.Contains("Ship Construction"))
                    MinistryOfConstruction.HandleNewlyConstructedShips(empire, galaxy); 

                   
               if (managementMinisters.Contains("Colonization"))
                    MinistryOfColonization.Run(empire, galaxy);


               
                if (managementMinisters.Contains("Ship Construction"))
                    MinistryOfConstruction.ConstructShips(empire, galaxy);
                if (managementMinisters.Contains("Unit Construction"))
                    MinistryOfConstruction.ConstructUnits(empire, galaxy); 
            }

            PlanManager.RemoveCompleted(); 
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

