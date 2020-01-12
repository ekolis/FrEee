#r "../../../bin/Debug/FrEee.Core.dll"
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Orders;

public static class PlanManager
{
    public static Empire Empire;

    public static PlanList CurrentPlans; 

    /// <summary>
    /// Unpacks all plans into 
    /// </summary>
    public static void Unpack()
    {
        if (Empire.AINotes.ContainsKey("PlanList"))
        {
            var str = Empire.AINotes["PlanList"] as string;
            if (str != null)
            {
                CurrentPlans = FrEee.Utility.JsonSerializer.DeserializeObjectFromJson<PlanList>(str);
                return; 
            }
        }          
        CurrentPlans = new PlanList();
        Empire.LogAIMessage("Failed to load plans"); 
        
    }

    public static void Pack()
    {
        CurrentPlans.AssignAllIds(); 

        if (Empire.AINotes.ContainsKey("PlanList"))
            Empire.AINotes.Remove("PlanList");

        var serializedPlans = FrEee.Utility.JsonSerializer.SerializeObjectToJson(CurrentPlans);
        Empire.LogAIMessage(serializedPlans); 
        Empire.AINotes.Add("PlanList", serializedPlans); 
    }


    
}

/// <summary>
/// The method of storing plans between turns for the AI. 
/// </summary>
[Serializable]
public class PlanList
{
    public List<ColonizationPlan> ColonizationPlans;

    public List<ShipBuildPlan> ShipBuildPlans; 


    public PlanList()
    {
        ColonizationPlans = new List<ColonizationPlan>();
        ShipBuildPlans = new List<ShipBuildPlan>(); 
    }

    public void AssignAllIds()
    {
        foreach (var p in ColonizationPlans)
            if (p.PlanId == null)
                p.AssignId();
        foreach (var p in ShipBuildPlans)
            if (p.PlanId == null)
                p.AssignId();
    }
}


[Serializable]
public abstract class Plan
{
    public string PlanId; 


    [DoNotSerialize]
    public virtual string TypeShorthand { get; }

    public bool IsComplete;


    public void AssignId()
    {
        PlanId = $"{TypeShorthand}-{DateTime.Now.Ticks}";
    }
}


[Serializable]
public class ColonizationPlan : Plan
{
    [DoNotSerialize]
    public override string TypeShorthand => "C";

    /// <summary>
    /// The planet intended to colonize. 
    /// </summary>
    [DoNotSerialize]
    public Planet Planet { get { return new GalaxyReference<Planet>(PlanetId);  } set { PlanetId = value.ID; } }


    /// <summary>
    /// The Id of the planet. 
    /// </summary>
    public long PlanetId; 

    /// <summary>
    /// Whether this plan is awaiting the build to complete. 
    /// </summary>
    public bool AwaitingBuild;

    /// <summary>
    /// The Shipbuild plan that uses this plan. 
    /// </summary>
    public string ShipBuildPlanId; 

}

[Serializable]
public class ShipBuildPlan :Plan
{
    [DoNotSerialize]
    public override string TypeShorthand => "BS";

    /// <summary>
    /// The planId of the plan that requires this to be built. 
    /// </summary>
    public string RequestPlanId;

    /// <summary>
    /// The role this build is made for. 
    /// </summary>
    public string Role;

    /// <summary>
    /// Whether or not the order to build the ship has been sent. 
    /// </summary>
    public bool SentOrder;

    /// <summary>
    /// The construction order reference. 
    /// </summary>
    public GalaxyReference<ConstructionOrder> ConstructionOrderReference;



}
