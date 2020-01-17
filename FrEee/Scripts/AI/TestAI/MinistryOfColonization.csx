#r "../../../bin/Debug/FrEee.Core.dll"
#load "Plan.csx"
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Linq;
using System.Collections.Generic;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Orders;
using FrEee.Utility.Extensions;

/// <summary>
/// Class devoted to the colonization of worlds. 
/// </summary>
public class MinistryOfColonization
{
    Empire Empire;
    Galaxy Galaxy;


    bool updatedPlans = false; 

    public void Run(Empire empire, Galaxy galaxy)
    {
        Empire = empire;
        Galaxy = galaxy;

        PlanExpansion();
        ProcessColonization();
        CreateBuildPlans(); 
    }

    /// <summary>
    /// Initial Planing of the expansion to new colonies.  
    /// </summary>
    public void PlanExpansion()
    {
        var currentplanetsPlanned = PlanManager.CurrentPlans.ColonizationPlans.Select(x => x.PlanetId);
        var currentTech = new List<string>();
        var empireDesigns = Empire.KnownDesigns.Where(x => x.Owner == Empire);
        if (empireDesigns.Any(x => x.Role == "Colonize Planet - Rock"))
            currentTech.Add("Colonize Planet - Rock");
        if (empireDesigns.Any(x => x.Role == "Colonize Planet - Ice"))
            currentTech.Add("Colonize Planet - Ice");
        if (empireDesigns.Any(x => x.Role == "Colonize Planet - Gas"))
            currentTech.Add("Colonize Planet - Gas");


        var possibles = Empire.ExploredStarSystems.SelectMany(x => x.FindSpaceObjects<Planet>
            (p => !p.HasColony && currentTech.Contains(p.ColonizationAbilityName) && !currentplanetsPlanned.Contains(p.ID)));

        foreach(var planet in possibles)
        {

            var colonyPlan = new ColonizationPlan()
            {
                AwaitingBuild = true,
                IsComplete = false,
                Planet = planet,
                Priority = 1000,
                ColonyShipDespatched = false
            };
            //work out source planet; todo; factor in atmosphere type. 
            int distance = int.MaxValue;
            Planet sourcePlant; 
            foreach (var colony in Empire.ColonizedPlanets.Where(x => x.ConstructionQueue.IsSpaceYardQueue))
            {
                var curdistance = Pathfinder.EstimateDistance(colony.Sector, planet.Sector, Empire);
                if (curdistance < distance)
                {
                    colonyPlan.SourcePlanetId = colony.ID;
                    distance = curdistance;
                    sourcePlant = colony; 
                }
            }
            //can't reach this world currently? then leave for now.  
            if (distance == int.MaxValue)
                continue;
            //work out the priority. 

            CalculateScore(colonyPlan); 

            PlanManager.CurrentPlans.ColonizationPlans.Add(colonyPlan); 
            
        }
        
    }

    /// <summary>
    /// Processes the current colonization to order it to move. 
    /// </summary>
    public void ProcessColonization()
    {
        //Assign unassigned colony ships to the colony plan
        var unassignedShips = Empire.OwnedSpaceObjects.OfType<Ship>().Where(x =>
        (x.HasAbility("Colonize Planet - Rock") || x.HasAbility("Colonize Planet - Ice") || x.HasAbility("Colonize Planet - Gas"))
        && x.IsIdle && !PlanManager.CurrentPlans.ColonizationPlans.Select(x => x.ShipId).Contains(x.ID)); 
        foreach(var ship in unassignedShips)
        {
            UpdateUnstartedPlans(); 
            ColonizationPlan currentPlan = null;
            int currentPriority = int.MinValue; 
            foreach(var plan in PlanManager.CurrentPlans.ColonizationPlans.Where(x => x.Ship == null 
                && (x.ShipBuildPlanId == null || !x.ShipBuildPlan.SentOrder)  && ship.HasAbility(x.Planet.ColonizationAbilityName)))
            {
                var distance = Pathfinder.EstimateDistance(ship.Sector, plan.Planet.Sector, Empire);
                var potentialPriority =  plan.Priority - (distance * 100); 

                if (potentialPriority > currentPriority)
                {
                    currentPriority = potentialPriority;
                    currentPlan = plan; 
                }
            }
            if (currentPlan == null)
                continue; 

            currentPlan.Ship = ship;
            currentPlan.AwaitingBuild = false;
            if (currentPlan.ShipBuildPlan != null)
            {
                currentPlan.ShipBuildPlan.IsComplete = true;
            }
        }



        var plansToProcess = PlanManager.CurrentPlans.ColonizationPlans.Where(x => !x.ColonyShipDespatched && x.Ship != null); 
        foreach(var plan in plansToProcess)
        {
            ///someone got here first?! Cancel the plan. 
            if (plan.Planet.Colony != null)
            {
                plan.IsComplete = true; 
                continue; 
            }
            var ship = plan.Ship; 
            //load some population. 
            if (ship.Cargo.Population.Count < 1)
            {
                var planets = ship.Sector.SpaceObjects.OfType<Planet>().Where(x => x.Owner == Empire); 
                if (planets.Count() > 0)
                {
                    bool foundBreathers = false; 
                    foreach (var pHere in planets)
                    {
                        var delta = new CargoDelta();
                        foreach (var kvp in pHere.AllPopulation)
                        {
                            if (kvp.Key.NativeAtmosphere == plan.Planet.Atmosphere)
                            {
                                delta.RacePopulation[kvp.Key] = null; // load all population of this race
                                foundBreathers = true;
                            }
                        }
                        if (foundBreathers)
                        {
                            var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
                            ship.IssueOrder(loadPopOrder);
                        }
                    }
                    if (!foundBreathers)
                    {
                        foreach (var pHere in planets)
                        {
                            var delta = new CargoDelta();
                            delta.AllPopulation = true;
                            var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
                            ship.IssueOrder(loadPopOrder); 
                        }
                    }
                }
                else
                {
                    //insert code to head to nearest colony? 
                    continue; 
                }
            }

            plan.ColonyCommandId = DateTime.UtcNow.Ticks; 
            ship.IssueOrder(new MoveOrder(plan.Planet.Sector, true));
            ship.IssueOrder(new ColonizeOrder(plan.Planet) { ID = plan.ColonyCommandId });
            plan.ColonyShipDespatched = true; 
        }

        //If the plan is completed, say so, or someone else got there first? 
        foreach (var plan in PlanManager.CurrentPlans.ColonizationPlans.Where(x => x.ColonyShipDespatched && x.Planet.HasColony))
        {
            plan.IsComplete = true;
            if (plan.Ship != null)
                plan.Ship.Orders.Clear(); 
        }
    }

    /// <summary>
    /// Creates any build plans that need creating. 
    /// </summary>
    public void CreateBuildPlans()
    {
        foreach(var plan in PlanManager.CurrentPlans.ColonizationPlans.Where(x => x.AwaitingBuild && x.ShipBuildPlan == null))
        {
            var buildPlan = new ShipBuildPlan()
            {
                IsComplete = false,
                RequestPlanId = plan.PlanId,
                SentOrder = false,
                Role = plan.Planet.ColonizationAbilityName, 
                Priority = plan.Priority, 
                ConstructionQueue = plan.SourcePlanet?.ConstructionQueue
            };
            buildPlan.AssignId();
            plan.ShipBuildPlanId = buildPlan.PlanId;
            PlanManager.CurrentPlans.ShipBuildPlans.Add(buildPlan); 
        }
    }

    /// <summary>
    /// Updates any plan that has not yet truly started. 
    /// </summary>
    public void UpdateUnstartedPlans()
    {
        if (updatedPlans)
            return; 
        foreach(var plan in PlanManager.CurrentPlans.ColonizationPlans.Where(x => x.AwaitingBuild && x.ShipBuildPlanId == null))
        {
            //someone else settled this world, so cancel the plan. 
            if (plan.Planet.Colony != null)
            {
                plan.IsComplete = true; 
                continue;
            }


            int distance = int.MaxValue;
            Planet sourcePlant;
            foreach (var colony in Empire.ColonizedPlanets.Where(x => x.ConstructionQueue.IsSpaceYardQueue))
            {
                var curdistance = Pathfinder.EstimateDistance(colony.Sector, plan.Planet.Sector, Empire);
                if (curdistance < distance)
                {
                    plan.SourcePlanetId = colony.ID;
                    distance = curdistance;
                    sourcePlant = colony;
                }
            }
            //Lost connection to this world? set it to min priority. 
            if (distance == int.MaxValue)
            {
                plan.Priority = int.MinValue; 
                continue; 
            }

            CalculateScore(plan); 
        }
        updatedPlans = true; 
    }

    /// <summary>
    /// Calculates the priority of the plan. 
    /// </summary>
    /// <param name="colonyPlan"></param>
    void CalculateScore(ColonizationPlan colonyPlan)
    {       
        var planet = colonyPlan.Planet;
        colonyPlan.Priority = 1000; //initial value; 


        colonyPlan.Priority -= Pathfinder.EstimateDistance(colonyPlan.SourcePlanet.Sector, planet.Sector, Empire);
        if (planet.StarSystem.HasAbility(Empire, "Spaceport"))
            colonyPlan.Priority += 100;
        if (planet.Atmosphere == Empire.PrimaryRace.NativeAtmosphere)
            colonyPlan.Priority += 500;




        colonyPlan.Priority += (int)planet.Size.StellarSize * 10;
    }

}