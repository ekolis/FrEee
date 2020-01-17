#r "../../../bin/Debug/FrEee.Core.dll"
#load "Plan.csx"
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility; 
using System;
using System.Linq;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Commands; 



/// <summary>
/// Class devoted to the construction of various ships and units. 
/// </summary>
public class MinistryOfConstruction
{
    public Empire Empire => PlanManager.Empire;

    public Galaxy Galaxy; 
     

    public void ConstructShips(Empire empire, Galaxy galaxy)
    {

        var toBuild = PlanManager.CurrentPlans.ShipBuildPlans.Where(x => !x.SentOrder && !x.IsComplete);

        //insert some way that the government can assign priority to individual build plans based on the plan they are for. 
        foreach (var constrQueue in empire.ConstructionQueues.Where(x => x.IsIdle && x.IsSpaceYardQueue).ToList())
        {
            var possibles = from potential in toBuild
                            where potential.ConstructionQueue == constrQueue
                            || (potential.ConstructionQueue == null && potential.StarSystem == constrQueue.Container.StarSystem)
                            || (potential.ConstructionQueue == null && potential.StarSystem == null)
                            orderby potential.Priority descending
                            select potential;
            var plan = possibles.FirstOrDefault();
            if (plan == null)
                continue;

            try
            {
                var design = GetDesignForBuildPlan(plan);
                var order = design.CreateConstructionOrder(constrQueue);

                constrQueue.AddOrder(order); 
                plan.ConstructionQueue = constrQueue;
                plan.SentOrder = true;
                var cmd = new AddOrderCommand
                        (
                            constrQueue,
                            order
                        );
                Empire.Commands.Add(cmd); 

            }
            catch(Exception e)
            {
                Empire.LogAIMessage($"Error in Ministry of Construction creating ship build command: {e.Message}");
                continue; 
            }


        }
    }


    public void ConstructUnits(Empire empire, Galaxy galaxy)
    {

    }


    IDesign GetDesignForBuildPlan(ShipBuildPlan buildPlan)
    {
        if (buildPlan.DesignBaseName != null)
        {
            var design = Empire.KnownDesigns.Where(x => x.BaseName == buildPlan.DesignBaseName && !x.IsObsolete).FirstOrDefault(); 
            if (design != null)
            {
                return design.LatestVersion; 
            }

        }
        if (buildPlan.Role != null)
        {
            var designs = Empire.KnownDesigns.Where(x => x.Owner == Empire && x.Role == buildPlan.Role && !x.IsObsolete).FirstOrDefault();
            if (designs != null)
                return designs.LatestVersion; 
        }
        
        throw new Exception("Unable to find design to build"); 
    }

}



