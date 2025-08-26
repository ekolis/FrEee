using FrEee.Objects.Civilization;
using FrEee.Processes.Combat;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Modding.Abilities;
using FrEee.Objects.Space;
using FrEee.Vehicles;

namespace FrEee.Objects.Technology;

/// <summary>
/// A combination of component template and mount.
/// </summary>
[Serializable]
public class MountedComponentTemplate : ITemplate<Component>, INamed, IAbilityObject, IPromotable, IContainable<IDesign>, IFormulaHost, IUpgradeable<MountedComponentTemplate>, IPictorial
{
	public MountedComponentTemplate(IDesign container, ComponentTemplate ct, Mount mount = null)
	{
		Container = container;
		ComponentTemplate = ct;
		Mount = mount;
	}

	public IEnumerable<Ability> Abilities
	{
		get
		{
			if (abilities == null)
				abilities = ComponentTemplate.Abilities.Select(a => ComputeAbility(a)).ToArray();
			return abilities;
		}
	}

	private Ability ComputeAbility(Ability a)
	{
		var dict = new Dictionary<string, object>(Variables);
		foreach (var kvp in a.Variables)
			dict.Add(kvp.Key, kvp.Value);
		string description;
		if (a.Description is not null)
			description = a.Description.Evaluate(dict);
		else if (a.Rule.Description is not null)
			description = a.Rule.Description.Evaluate(dict);
		else
			description = $"{a.Rule.Name}: {string.Join(",", a.Values.Select(q => q?.ToString()))}";
		var result = new Ability(this)
		{
			Rule = a.Rule,
			Values = new List<Formula<string>>(a.Values),
			Description = description,
		};
		if (Mount != null)
		{
			if (Mount.AbilityPercentages.ContainsKey(a.Rule))
			{
				foreach (var p in Mount.AbilityPercentages[a.Rule])
				{
					result.Values[p.Key] = (double.Parse(result.Values[p.Key].Evaluate(this)) * p.Value / 100).ToString();
					a.Description = null; // values have been modified, need to use generic description
				}
			}
			if (Mount.AbilityModifiers.ContainsKey(a.Rule))
			{
				foreach (var m in Mount.AbilityModifiers[a.Rule])
				{
					result.Values[m.Key] = (double.Parse(result.Values[m.Key].Evaluate(this)) + m.Value).ToString();
					a.Description = null; // values have been modified, need to use generic description
				}
			}
		}
		return result;
	}

	[DoNotSerialize(false)]
	private Ability[] abilities { get; set; }

	public AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Component; }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { yield return ComponentTemplate; }
	}

	/// <summary>
	/// The component template used.
	/// </summary>
	[DoNotSerialize]
	public ComponentTemplate ComponentTemplate { get; set; }

	/// <summary>
	/// The design which contains this mounted component template.
	/// </summary>
	[DoNotSerialize]
	[Populate<MountedComponentTemplateContainerPopulator>]
	public IDesign Container { get; set; }

	public ResourceQuantity Cost
	{
		get
		{
			if (Mount == null)
				return ComponentTemplate.Cost.Evaluate(this);
			return ComponentTemplate.Cost.Evaluate(this) * Mount.CostPercent.Evaluate(this) / 100;
		}
	}

	public int Durability
	{
		get
		{
			if (Mount == null)
				return ComponentTemplate.Durability.Evaluate(this);
			return ComponentTemplate.Durability.Evaluate(this) * Mount.DurabilityPercent.Evaluate(this) / 100;
		}
	}

	[DoNotSerialize]
	public Image Icon
	{
		get
		{
			var icon = (Image)ComponentTemplate.Icon.Clone();
			var g = Graphics.FromImage(icon);
			var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
			var brush = Brushes.White;
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Near;
			sf.LineAlignment = StringAlignment.Far;
			if (Mount != null)
				g.DrawString(Mount.Code, font, brush, new Point(0, 32), sf);
			return icon;
		}
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return ComponentTemplate.IconPaths;
		}
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { yield break; }
	}

	public bool IsObsolescent
	{
		get { return this != LatestVersion; }
	}

	/// <summary>
	/// Is this template obsolete (can be upgraded to a newer component)?
	/// </summary>
	public bool IsObsolete
	{
		get
		{
			return ComponentTemplate.IsObsolete || Mount != null && Mount.IsObsolete;
		}
	}

	/// <summary>
	/// Does this template have valid component and mount templates? (Or a null mount template)
	/// </summary>
	public bool IsValidInMod => componentTemplate.HasValue && (mount == null || mount.HasValue);

	public MountedComponentTemplate LatestVersion
	{
		get
		{
			if (IsObsolete)
				return new MountedComponentTemplate(Container, ComponentTemplate.LatestVersion, Mount == null ? null : Mount.LatestVersion);
			else
				return this;
		}
	}

	/// <summary>
	/// The mount used.
	/// </summary>
	[DoNotSerialize]
	public Mount? Mount { get; set; }

	/// <summary>
	/// The name of the component, prefixed with the short name of the mount (if any).
	/// </summary>
	public string Name
	{
		get
		{
			if (Mount == null)
				return ComponentTemplate.Name;
			return Mount.ShortName + " " + ComponentTemplate.Name;
		}
	}

	public IEnumerable<MountedComponentTemplate> NewerVersions
	{
		get { return Galaxy.Current.FindSpaceObjects<IVehicle>().SelectMany(v => v.Components).Select(c => c.Template).Where(mct => mct.LatestVersion == this).Distinct(); }
	}

	public IEnumerable<MountedComponentTemplate> OlderVersions
	{
		get { return Galaxy.Current.FindSpaceObjects<IVehicle>().SelectMany(v => v.Components).Select(c => c.Template).Where(mct => LatestVersion == mct).Distinct(); }
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			if (Container != null)
				yield return Container;
		}
	}

	[DoNotSerialize]
	public Image Portrait
	{
		get
		{
			return ComponentTemplate.Portrait;
		}
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			return ComponentTemplate.PortraitPaths;
		}
	}

	public int Size
	{
		get
		{
			if (Mount == null)
				return ComponentTemplate.Size.Evaluate(this);
			return ComponentTemplate.Size.Evaluate(this) * Mount.SizePercent.Evaluate(this) / 100;
		}
	}

	public int SupplyUsage
	{
		get
		{
			if (Mount == null)
				return ComponentTemplate.SupplyUsage.Evaluate(this);
			return ComponentTemplate.SupplyUsage.Evaluate(this) * Mount.SupplyUsagePercent.Evaluate(this) / 100;
		}
	}

	public IEnumerable<Ability> UnstackedAbilities
	{
		get { return Abilities; }
	}

	public IDictionary<string, object> Variables
	{
		get
		{
			var design = Container ?? Services.Designs.CreateDesign(Mod.Current.Hulls.FirstOrDefault(h => ComponentTemplate.VehicleTypes.HasFlag(h.VehicleType)));
			var empire = Container == null ? Empire.Current : Container.Owner;
			return new Dictionary<string, object>
			{
				{"component", ComponentTemplate},
				{"mount", Mount},
				{"design", design},
				{"hull", design.Hull},
				{"empire", empire}
			};
		}
	}

	/// <summary>
	/// Accuracy rating of this component, if it is a weapon.
	/// </summary>
	public int WeaponAccuracy
	{
		get
		{
			var w = ComponentTemplate.WeaponInfo;
			if (w == null)
				return 0;
			if (w is DirectFireWeaponInfo)
				return ((DirectFireWeaponInfo)w).AccuracyModifier.Evaluate(this) + (Mount == null ? 0 : Mount.WeaponAccuracyModifier.Evaluate(this));
			return 999; // seekers/warheads
		}
	}

	public int WeaponMaxRange
	{
		get
		{
			var w = ComponentTemplate.WeaponInfo;
			if (w == null)
				return 0;
			if (Mount == null)
				return w.MaxRange;
			return w.MaxRange.Value + Mount.WeaponRangeModifier.Value;
		}
	}

	public int WeaponMinRange
	{
		get
		{
			var w = ComponentTemplate.WeaponInfo;
			if (w == null)
				return 0;
			if (Mount == null)
				return w.MinRange;
			if (w.MinRange == 0)
				return 0; // don't create a blind spot for weapons with a min range of zero
			return w.MinRange.Value + Mount.WeaponRangeModifier.Value;
		}
	}

	private ModReference<ComponentTemplate> componentTemplate
	{
		get => ComponentTemplate;
		set => ComponentTemplate = value;
	}
	private ModReference<Mount> mount
	{
		get => Mount;
		set => Mount = value;
	}

	public static bool operator !=(MountedComponentTemplate t1, MountedComponentTemplate t2)
	{
		return !(t1 == t2);
	}

	public static bool operator ==(MountedComponentTemplate t1, MountedComponentTemplate t2)
	{
		if (t1 is null && t2 is null)
			return true;
		if (t1 is null || t2 is null)
			return false;
		return t1.Container == t2.Container && t1.ComponentTemplate == t2.ComponentTemplate && t1.mount == t2.mount;
	}

	public override bool Equals(object? obj)
	{
		// TODO - upgrade equals to use "as" operator
		if (obj is MountedComponentTemplate)
			return this == (MountedComponentTemplate)obj;
		return false;
	}

	public override int GetHashCode()
	{
		// can't mash the container itself because that would cause a circular dependency on this template
		return HashCodeMasher.Mash(Container?.ID, ComponentTemplate, Mount);
	}

	/// <summary>
	/// Damage inflicted by this component at range, if it is a weapon.
	/// </summary>
	public int GetWeaponDamage(int range)
	{
		var w = ComponentTemplate.WeaponInfo;
		if (w == null)
			return 0;

		var shot = new Shot(null, null, null, range - (Mount == null ? 0 : Mount.WeaponRangeModifier.Value));
		var dict = new SafeDictionary<string, object>();
		dict["range"] = range;
		return w.Damage.Evaluate(null, dict) * (Mount?.WeaponDamagePercent.Value ?? 100) / 100;
	}

	public Component Instantiate()
	{
		return new Component(null, this);
	}

	public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			// we don't need to do this because the container is only loosely coupled to the MCT
			// and it will be sanitized for itself anyway
			//Container.ReplaceClientIDs(idmap, done);
		}
		return this;
	}

	public override string ToString()
	{
		return Name;
	}

	/// <summary>
	/// Compares templates ignoring the containing vehicle (only compares component template and mount)
	/// </summary>
	public class SimpleEqualityComparer : IEqualityComparer<MountedComponentTemplate>
	{
		public bool Equals(MountedComponentTemplate? x, MountedComponentTemplate? y)
		{
			return x?.ComponentTemplate == y?.ComponentTemplate && x?.Mount == y?.Mount;
		}

		public int GetHashCode(MountedComponentTemplate obj)
		{
			return HashCodeMasher.Mash(obj.ComponentTemplate, obj.Mount);
		}
	}
}
