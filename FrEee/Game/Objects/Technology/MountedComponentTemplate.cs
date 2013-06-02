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
	[Serializable]
	public class MountedComponentTemplate : ITemplate<Component>, INamed, IAbilityObject
	{
		public MountedComponentTemplate(ComponentTemplate ct, Mount mount = null)
		{
			ComponentTemplate = ct;
			Mount = mount;
		}

		/// <summary>
		/// The component template used.
		/// </summary>
		public ComponentTemplate ComponentTemplate { get { return componentTemplate; } set { componentTemplate = value; } }

		private Reference<ComponentTemplate> componentTemplate;

		/// <summary>
		/// The mount used.
		/// </summary>
		public Mount Mount { get { return mount; } set { mount = value; } }

		private Reference<Mount> mount;

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

		public static bool operator ==(MountedComponentTemplate t1, MountedComponentTemplate t2)
		{
			if ((object)t1 == null && (object)t2 == null)
				return true;
			if ((object)t1 == null || (object)t2 == null)
				return false;
			return t1.ComponentTemplate == t2.ComponentTemplate && t1.Mount == t2.Mount;
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
			if (Mount == null)
				return ComponentTemplate.GetHashCode();
			return ComponentTemplate.GetHashCode() ^ Mount.GetHashCode();
		}
	}
}