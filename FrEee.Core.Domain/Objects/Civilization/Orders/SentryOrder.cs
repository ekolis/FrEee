using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Vehicles.Types;

namespace FrEee.Objects.Civilization.Orders;

/// <summary>
/// An order for a mobile space object to hold position until enemies are sighted in the system.
/// </summary>
[Serializable]
public class SentryOrder : IOrder
{
    public SentryOrder()
    {
        Owner = Empire.Current;
    }

	public IEnumerable<IReferrable> Referrables => [];

	public bool ConsumesMovement
    {
        get { return true; }
    }

    public long ID { get; set; }

    public bool IsComplete
    {
        get;
        set;
    }

    public bool IsDisposed { get; set; }

    /// <summary>
    /// The empire which issued the order.
    /// </summary>
    [DoNotSerialize]
    public Empire Owner { get; set; }

	private GameReference<Empire> owner
	{
		get => Owner;
		set => Owner = value;
	}

	public bool CheckCompletion(IOrderable v)
    {
        return IsComplete;
    }

    /// <summary>
    /// Orders are visible only to their owners.
    /// </summary>
    /// <param name="emp"></param>
    /// <returns></returns>
    public Visibility CheckVisibility(Empire emp)
    {
        if (emp == Owner)
            return Visibility.Visible;
        return Visibility.Unknown;
    }

    public void Dispose()
    {
        if (IsDisposed)
            return;
        foreach (var v in Game.Current.Referrables.OfType<ISpaceVehicle>())
            v.Orders.Remove(this);
        Game.Current.UnassignID(this);
    }

    public void Execute(IOrderable ord)
    {
        if (ord is IMobileSpaceObject sobj)
        {
            // if hostiles in system, we are done sentrying
            if (sobj.FindStarSystem().FindSpaceObjects<ICombatSpaceObject>(s => s.IsHostileTo(sobj.Owner)).Any())
                IsComplete = true;

            // spend time
            sobj.SpendTime(sobj.TimePerMove);
        }
        else
            ord.Owner.RecordLog(ord, $"{ord} cannot sentry because it is not a mobile space object.", LogMessageType.Error);
    }

    public IEnumerable<LogMessage> GetErrors(IOrderable executor)
    {
        // this order doesn't error
        yield break;
    }

    public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
		// This type does not use client objects, so nothing to do here.
		return this;
	}

    public override string ToString()
    {
        return "Sentry";
    }
}