using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Modding.Templates;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A combination of component template and mount.
	/// </summary>
	[Serializable]
	public class MountedComponentTemplate : ITemplate<Component>, INamed, IAbilityObject, IPromotable, IContainable<IDesign>, IFormulaHost
	{
		public MountedComponentTemplate(IDesign container, ComponentTemplate ct, Mount mount = null)
		{
			Container = container;
			ComponentTemplate = ct;
			Mount = mount;
		}

		/// <summary>
		/// The component template used.
		/// </summary>
		[DoNotSerialize]
		public ComponentTemplate ComponentTemplate { get { return componentTemplate; } set { componentTemplate = value; } }

		private Reference<ComponentTemplate> componentTemplate { get; set; }

		/// <summary>
		/// The mount used.
		/// </summary>
		[DoNotSerialize]
		public Mount Mount { get { return mount; } set { mount = value; } }

		private Reference<Mount> mount { get; set; }

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

		[DoNotSerialize]
		public Image Portrait
		{
			get
			{
				return ComponentTemplate.Portrait;
			}
		}

		public Component Instantiate()
		{
			return new Component(null, this);
		}

		public IEnumerable<Ability> Abilities
		{
			get
			{
				return ComponentTemplate.Abilities.Select(a =>
					{
						var result = new Ability(this)
						{
							Rule = a.Rule,
							Values = new List<Formula<string>>(a.Values),
							Description = a.Description.Evaluate(this),
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
					});
			}
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
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

		public ResourceQuantity Cost
		{
			get
			{
				if (Mount == null)
					return ComponentTemplate.Cost.Evaluate(this);
				return ComponentTemplate.Cost.Evaluate(this) * Mount.CostPercent.Evaluate(this) / 100;
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

		public int Durability
		{
			get
			{
				if (Mount == null)
					return ComponentTemplate.Durability.Evaluate(this);
				return ComponentTemplate.Durability.Evaluate(this) * Mount.DurabilityPercent.Evaluate(this) / 100;
			}
		}

		public static bool operator ==(MountedComponentTemplate t1, MountedComponentTemplate t2)
		{
			if (t1.IsNull() && t2.IsNull())
				return true;
			if (t1.IsNull() || t2.IsNull())
				return false;
			return t1.Container == t2.Container && t1.ComponentTemplate == t2.ComponentTemplate && t1.Mount == t2.Mount;
		}

		public static bool operator !=(MountedComponentTemplate t1, MountedComponentTemplate t2)
		{
			return !(t1 == t2);
		}

		public override bool Equals(object obj)
		{
			if (obj is MountedComponentTemplate)
				return this == (MountedComponentTemplate)obj;
			return false;
		}

		public override int GetHashCode()
		{
			return Container.GetSafeHashCode() ^ ComponentTemplate.GetSafeHashCode() ^ Mount.GetSafeHashCode();
		}

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Damage inflicted by this component at range, if it is a weapon.
		/// </summary>
		public Formula<int> WeaponDamage
		{
			get
			{
				var w = ComponentTemplate.WeaponInfo;
				if (w == null)
					return null;
				if (Mount == null)
					return w.Damage;

				return w.Damage * Mount.WeaponDamagePercent / 100;
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
				return w.MinRange + Mount.WeaponRangeModifier;
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
				return w.MaxRange + Mount.WeaponRangeModifier;
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

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				container.ReplaceClientIDs(idmap, done);
			}
		}


		private Reference<IDesign> container { get; set; }

		/// <summary>
		/// The design which contains this mounted component template.
		/// </summary>
		[DoNotSerialize]
		public IDesign Container { get { return container == null ? null : container.Value; } set { container = value.Reference(); } }

		public IDictionary<string, object> Variables
		{
			get 
			{
				var design = Container ?? Design.Create(Mod.Current.Hulls.FirstOrDefault(h => ComponentTemplate.VehicleTypes.HasFlag(h.VehicleType)));
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

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Component; }
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { yield return ComponentTemplate; }
		}

		public IAbilityObject Parent
		{
			get { return Container; }
		}
	}
}