using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding.Templates;
using FrEee.Utility;
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
	public struct MountedComponentTemplate : ITemplate<Component>, INamed, IAbilityObject
	{
		public MountedComponentTemplate(ComponentTemplate ct, Mount mount = null) : this()
		{
			ComponentTemplate = ct;
			Mount = mount;
		}

		/// <summary>
		/// The component template used.
		/// </summary>
		public ComponentTemplate ComponentTemplate { get; set; }

		/// <summary>
		/// The mount used.
		/// </summary>
		public Mount Mount { get; set; }

		/// <summary>
		/// The name of the component, prefixed with the name of the mount (if any).
		/// </summary>
		public string Name
		{
			get
			{
				if (Mount == null)
					return ComponentTemplate.Name;
				return Mount.Name + " " + ComponentTemplate.Name;
			}
		}

		public Image Icon
		{
			get
			{
				// TODO - draw mount code on the icon
				return ComponentTemplate.Icon;
			}
		}

		public Image Portrait
		{
			get
			{
				return ComponentTemplate.Portrait;
			}
		}

		public Component Instantiate()
		{
			return new Component(this);
		}

		public IEnumerable<Ability> Abilities
		{
			get
			{
				// TODO - take into effect mounts
				return ComponentTemplate.Abilities;
			}
		}

		public int Size
		{
			get
			{
				// TODO - take into effect mounts
				return ComponentTemplate.Size;
			}
		}

		public Resources Cost
		{
			get
			{
				// TODO - take into effect mounts
				return ComponentTemplate.Cost;
			}
		}

		public int SupplyUsage
		{
			get
			{
				// TODO - take into effect mounts
				return ComponentTemplate.SupplyUsage;
			}
		}

		public int Durability
		{
			get
			{
				// TODO - take into effect mounts
				return ComponentTemplate.Durability;
			}
		}
	}
}