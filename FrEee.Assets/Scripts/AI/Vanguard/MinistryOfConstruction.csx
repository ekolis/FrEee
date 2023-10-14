#r "../../../bin/Debug/FrEee.Core.dll"
#load "Plan.csx"
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Utility; 
using System;
using System.Linq;
using FrEee.Objects.Orders;
using FrEee.Objects.Vehicles;
using FrEee.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Objects.Commands;
using FrEee.Objects.LogMessages; 


/// <summary>
/// Class devoted to the construction of various ships and units. 
/// </summary>
public class MinistryOfConstruction
{
    public Empire Empire => PlanManager.Empire;

    public Galaxy Galaxy; 
     
    /// <summary>
    /// Finds all the newly constructed ships and deals with them. 
    /// </summary>
    /// <param name="empire"></param>
    /// <param name="galaxy"></param>
    public void HandleNewlyConstructedShips(Empire empire, Galaxy galaxy)
    {
        
        var completedShips = empire.Log.Where(x => x.LogMessageType == FrEee.Objects.LogMessages.LogMessageType.ConstructionComplete
            && x.TurnNumber == The.Game.TurnNumber); 
        foreach(var shiplog in completedShips)
        {
            var pict = shiplog as PictorialLogMessage<IConstructable>;
            if (pict != null)
            {
                var ship = pict.Context as Ship;
                if (ship != null)
                {

                    Empire.LogAIMessage($"Found ship construction message:{pict.Text}");
                    //get the plan from the PlanManager. 
                    var plan = PlanManager.CurrentPlans.ShipBuildPlans.FirstOrDefault(x => x.SentOrder
                        && x.DesignBaseName == ship.Design.BaseName
                        && x.ConstructionQueue.Container.Sector == ship.Sector);
                    if (plan == null || plan.RequestPlanId == null)
                        continue;
                    Empire.LogAIMessage($"Found build plan:{plan.PlanId} for {plan.RequestPlanId}");
                    var callingPlan = PlanManager.CurrentPlans.GetPlan(plan.RequestPlanId);
                    if (callingPlan == null) //forgot to cancel a ship we no longer need? Well, that sucks. 
                        continue;
                    switch (callingPlan.TypeShorthand)
                    {
                        case "C":
                            var p = callingPlan as ColonizationPlan;
                            p.Ship = ship;
                            p.AwaitingBuild = false;
                            break;


                    }

                    Empire.LogAIMessage($"Ship completed: planid:{plan.PlanId} shipId:{ship.ID}");
                    plan.IsComplete = true;
                }
                else
                    Empire.LogAIMessage($"Construction completed of {pict.Text}"); 
            }
            else
                Empire.LogAIMessage($"Construction of non IConstructable found: {shiplog?.Text}"); 
        }
    }

    /// <summary>
    /// Constructs any ships that have been requested at a free shipyard. 
    /// </summary>
    /// <param name="empire"></param>
    /// <param name="galaxy"></param>
    public void ConstructShips(Empire empire, Galaxy galaxy)
    {

        var toBuild = PlanManager.CurrentPlans.ShipBuildPlans.Where(x => !x.SentOrder && !x.IsComplete);

        //TODO: insert some way that the government can assign priority to individual build plans based on the plan they are for. 
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
                plan.DesignBaseName = design.BaseName; 
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
        //TODO. 
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



