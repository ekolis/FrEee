using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Technology;
using FrEee.Processes.Construction;
using FrEee.Serialization;
using FrEee.Utility;
using FrEee.Vehicles.Types;
using Newtonsoft.Json.Linq;

namespace FrEee.Objects.Civilization.Orders;

/// <summary>
/// An order for a construction queue to build something.
/// </summary>
[Serializable]
public class ConstructionOrder<T, TTemplate> : IConstructionOrder
    where T : IConstructable
    where TTemplate : ITemplate<T>, IReferrable, IConstructionTemplate
{
    public ConstructionOrder()
    {
        Owner = Empire.Current;
    }

	public IEnumerable<IReferrable> Referrables => [Item as IReferrable];

	public bool ConsumesMovement
    {
        get { return false; }
    }

    public ResourceQuantity Cost
    {
        get { return Template?.Cost ?? new ResourceQuantity(); }
    }

    public long ID { get; set; }

    public bool IsComplete
    {
        get
        {
            if (isComplete == null)
                return false; // haven't checked completion yet, so it's probably safe to say it's incomplete
            return isComplete.Value;
        }
        set
        {
            isComplete = value;
        }
    }

    public bool IsDisposed { get; set; }

    /// <summary>
    /// The item being built.
    /// </summary>
    public T Item { get; set; }

    IConstructable IConstructionOrder.Item
    {
        get { return Item; }
    }

    public string Name
    {
        get
        {
            return Template.Name;
        }
    }

    /// <summary>
    /// The empire which issued the order.
    /// </summary>
    [DoNotSerialize]
    public Empire Owner { get; set; }

    /// <summary>
    /// The construction template.
    /// </summary>
    [DoNotSerialize]
    public TTemplate Template { get; set; }

    private IReference<U> GetModReference<U>(string id)
    {
        // since T is not guaranteed to be a compile time IModObject implementation
        var type = typeof(ModReference<>).MakeGenericType(typeof(U));
        var r = (IReference<U>)Activator.CreateInstance(type, id);
        return r;
    }

    IConstructionTemplate IConstructionOrder.Template { get { return template.Value; } }

    [DoNotSerialize]
    private bool? isComplete
    {
        get;
        set;
    }

    private GameReference<Empire> owner
    {
        get { return Owner?.ReferViaGalaxy(); }
        set { Owner = value?.Value; }
	}
    private IReference<TTemplate>? template
    {
        get => Template switch
        {
            IModObject mo => GetModReference<TTemplate>(mo.ReferViaMod().ID),
            IReferrable r => new GameReference<TTemplate>(r.ReferViaGalaxy().ID),
            null => null
        };
        set => Template = value.Value;
    }
	public bool IsMemory { get; set; }
	public double Timestamp { get; set; }

	public bool CheckCompletion(IOrderable q)
    {
        var queue = (IConstructionQueue)q;
        isComplete = Item.ConstructionProgress >= Item.Cost || GetErrors(queue).Any();
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
            return Visibility.Owned;
        return Visibility.Unknown;
    }

    public void Dispose()
    {
        if (IsDisposed)
            return;
        foreach (var q in Game.Current.Referrables.OfType<IConstructionQueue>())
            q.Orders.Remove(this);
        Game.Current.UnassignID(this);
    }

    /// <summary>
    /// Does 1 turn's worth of building.
    /// </summary>
    public void Execute(IOrderable q)
    {
        var queue = (IConstructionQueue)q;
        var errors = GetErrors(queue);
        foreach (var error in errors)
            queue.Owner.Log.Add(error);

        if (!errors.Any())
        {
            // create item if needed
            if (Item is null)
            {
                Item = Template.Instantiate();
                if (!(Item is Facility))
                    Item.Owner = queue.Owner;
                if (Item is ISpaceVehicle sv)
                {
                    // space vehicles need their supplies filled up
                    sv.SupplyRemaining = sv.SupplyStorage;
                }
            }

            // apply build rate
            var costLeft = Item.Cost - Item.ConstructionProgress;
            var spending = ResourceQuantity.Min(costLeft, queue.UnspentRate);
            if (spending > queue.Owner.StoredResources)
            {
                spending = ResourceQuantity.Min(spending, queue.Owner.StoredResources);
                if (spending.IsEmpty)
                {
                    if (!queue.IsConstructionDelayed) // don't spam messages!
                        Owner.Log.Add(queue.Container.CreateLogMessage("Construction of " + Template + " at " + queue.Container + " was paused due to lack of resources.", LogMessageType.Generic));
                }
                else
                {
                    Owner.Log.Add(queue.Container.CreateLogMessage("Construction of " + Template + " at " + queue.Container + " was slowed due to lack of resources.", LogMessageType.Generic));
                }
                queue.IsConstructionDelayed = true;
            }
            queue.Owner.StoredResources -= spending;
            queue.UnspentRate -= spending;
            Item.ConstructionProgress += spending;
        }
    }

    public IEnumerable<LogMessage> GetErrors(IOrderable q)
    {
        var queue = (IConstructionQueue)q;

        // do we have a valid template?
        if (Template == null)
            yield return Owner.CreateLogMessage($"{queue.Container} cannot build a nonexistent template; skipping it. Probably a bug...", LogMessageType.Error);

        // validate that what's being built is unlocked
        if (!queue.Owner.HasUnlocked(Template))
            yield return Template.CreateLogMessage(Template + " cannot be built at " + queue.Container + " because we have not yet researched it.", LogMessageType.Warning);
    }

    public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
    {
        if (done == null)
            done = new HashSet<IPromotable>();
        if (!done.Contains(this))
        {
            done.Add(this);
            template.ReplaceClientIDs(idmap, done);
        }
		return this;
	}

    public void Reset()
    {
        Item = default;
    }

	public bool IsObsoleteMemory(Empire emp)
	{
        return false;
	}

	public void Redact(Empire emp)
	{
		if (CheckVisibility(emp) < Visibility.Owned)
        {
            // TODO: intel projects to see alien orders
            Dispose();
        }
	}
}
