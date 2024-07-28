using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Utility;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Ecs.Abilities;

/// <summary>
/// A special ability of some game object, or just a tag used by the AI or by modders.
/// </summary>
[Serializable]
public class Ability : IAbility, IContainable<IEntity>, IReferrable, IModObject, IDataObject, IFormulaHost
{
    public Ability(IEntity entity)
    {
        Container = entity;
        Values = new List<Formula<string>>();
    }

    public Ability(IEntity entity, AbilityRule rule, object[] values)
	{
        Container = entity;
        Rule = rule;
        Values = new List<Formula<string>>();
        foreach (var val in values)
        {
            if (val is IFormula f)
            {
                Values.Add(f.ToStringFormula());
            }
            else if (val is IEnumerable<IFormula> fs)
            {
                foreach (var ff in fs)
                {
                    Values.Add(ff.ToStringFormula());
                }
            }
            else if (val is null)
            {
                Values.Add(null);
            }
            else
            {
                Values.Add(new LiteralFormula<string>(val.ToString()));
            }
        }
    }

    public virtual void Interact(IInteraction interaction) { }

    [DoNotCopy]
    public IEntity Container { get; internal set; }

    public virtual SafeDictionary<string, object> Data
    {
        get
        {
            var dict = new SafeDictionary<string, object>();
            dict[nameof(rule)] = rule;
            dict[nameof(Values)] = Values;
            dict[nameof(Container)] = Container;
            dict[nameof(ID)] = ID;
            dict[nameof(IsDisposed)] = IsDisposed;
            dict[nameof(ModID)] = ModID;
            return dict;
        }

        set
        {
            rule = value[nameof(rule)].Default<ModReference<AbilityRule>>();
            Values = value[nameof(Values)].Default<IList<Formula<string>>>();
            Container = value[nameof(Container)].Default<IEntity>();
            ID = value[nameof(ID)].Default<long>();
            IsDisposed = value[nameof(IsDisposed)].Default<bool>();
            ModID = value[nameof(ModID)].Default<string>();
        }
    }

    /// <summary>
    /// A description of the ability's effects.
    /// Can use, e.g. [%Amount1%] to specify the amount in the Value 1 field.
    /// </summary>
    public Formula<string>? Description => Rule.Description;   

    public long ID
    {
        get;
        set;
    }

    public bool IsDisposed
    {
        get;
        set;
    }

    public string ModID
    {
        get;
        set;
    }

    public string Name
    {
        get { return null; } // TODO - should abilities even have names?
    }

    public Empire Owner
    {
        get
        {
            if (Container is IOwnable)
                return (Container as IOwnable).Owner;
            return null;
        }
    }

    /// <summary>
    /// The ability rule which defines what ability this is.
    /// </summary>
    [DoNotCopy]
    public AbilityRule Rule { get { return rule; } set { rule = value; } }

    /// <summary>
    /// Parameters from the mod meta templates.
    /// </summary>
    public IDictionary<string, object> TemplateParameters { get; set; }

    /// <summary>
    /// The first value of the ability. Not all abilities have values, so this might be null!
    /// </summary>
    public Formula<string> Value1
    {
        get
        {
            return Values.ElementAtOrDefault(0);
        }
    }

    /// <summary>
    /// The second value of the ability. Not all abilities have two values, so this might be null!
    /// </summary>
    public Formula<string> Value2
    {
        get
        {
            return Values.ElementAtOrDefault(1);
        }
    }

    /// <summary>
    /// Extra data for the ability.
    /// </summary>
    public IList<Formula<string>> Values { get; set; }

    public IDictionary<string, object> Variables
    {
        get
        {
            var dict = new Dictionary<string, object>();
            for (var i = 0; i < Values.Count; i++)
                dict.Add("Amount" + (i + 1), Values[i]);
            if (Rule.Matches("Shield Generation") || Rule.Matches("Phased Shield Generation") || Rule.Matches("Planet - Shield Generation"))
                dict.Add("ShieldPointsGenerated", Value1.ToInt()); // TODO - take into account mounts that affect shields
            return dict;
        }
    }

    private ModReference<AbilityRule> rule { get; set; }

    public void Dispose()
    {
        if (IsDisposed)
            return;
        //if (Container is IEntity)
        //  (Container as IEntity).Abilities.Remove(this);
        Galaxy.Current.UnassignID(this);
    }

    public override string ToString()
    {
        // get basic description
        string result;
        if (Description != null)
            result = Description.Evaluate(this, TemplateParameters);
        else if (Rule.Description != null)
            result = Rule.Description.Evaluate(this, TemplateParameters);
        else
            result = Rule.Name + ": " + string.Join(", ", Values.Select(v => v.Value));

        return result;
    }
}
