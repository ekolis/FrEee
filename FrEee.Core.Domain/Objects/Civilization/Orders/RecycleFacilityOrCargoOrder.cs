using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Extensions;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;

namespace FrEee.Objects.Civilization.Orders;

public class RecycleFacilityOrCargoOrder : IOrder
{
    public RecycleFacilityOrCargoOrder(IRecycleBehavior behavior, IRecyclable target)
    {
        Behavior = behavior;
        Target = target;
    }

	public IEnumerable<IReferrable> Referrables => [];

	public IRecycleBehavior Behavior { get; private set; }

    public bool ConsumesMovement
    {
        get { return false; }
    }

    public long ID
    {
        get;
        set;
    }

    public bool IsComplete
    {
        get;
        set;
    }

    public bool IsDisposed
    {
        get;
        set;
    }

    /// <summary>
    /// The empire which issued the order.
    /// </summary>
    [DoNotSerialize]
    public Empire Owner { get; set; }

    /// <summary>
    /// The facility or unit in cargo to recycle.
    /// </summary>
    [DoNotSerialize]
    public IRecyclable Target { get; set; }

    private GameReference<Empire> owner
    {
        get => Owner;
        set => Owner = value;
	}
    private GameReference<IRecyclable> target
    {
        get => Target.ReferViaGalaxy();
        set => Target = value.Value;
	}

    public bool CheckCompletion(IOrderable executor)
    {
        return IsComplete;
    }

    public void Dispose()
    {
        if (IsDisposed)
            return;
        foreach (var v in Game.Current.Referrables.OfType<IMobileSpaceObject>())
            v.RemoveOrder(this);
        Game.Current.UnassignID(this);
    }

    public void Execute(IOrderable x)
    {
        if (x is IMobileSpaceObject executor)
        {
            var errors = GetErrors(executor);
            if (errors.Any())
            {
                if (Owner != null)
                {
                    foreach (var e in errors)
                        Owner.Log.Add(e);
                }
                else
                    IsComplete = true;
                return;
            }
        }

        Behavior.Execute(Target);
        IsComplete = true;
    }

    public IEnumerable<LogMessage> GetErrors(IOrderable executor)
    {
        return Behavior.GetErrors(executor as IMobileSpaceObject, Target).Concat(SelfErrors);
    }

    private IEnumerable<LogMessage> SelfErrors
    {
        get
        {
            if (Target is ICombatant c && c.IsHostileTo(Owner))
                yield return c.CreateLogMessage($"You can't {Behavior.Verb} {c} because it belongs to a hostile empire.", LogMessageType.Error);
        }
    }

    public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
		// This type does not use client objects, so nothing to do here.
		return this;
	}

    public override string ToString()
    {
        return Behavior.Verb + " " + Target;
    }
}