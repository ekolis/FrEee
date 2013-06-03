using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A component of a vehicle.
	/// </summary>
	[Serializable]
	public class Component : IAbilityObject, INamed
	{
		public Component(MountedComponentTemplate template)
		{
			this.Template = template;
		}

		/// <summary>
		/// The template for this component.
		/// Specifies the basic stats of the component and its abilities.
		/// </summary>
		public MountedComponentTemplate Template { get; private set; }

		public IEnumerable<Ability> Abilities
		{
			get
			{
				return Template.Abilities;
			}
		}

		public string Name { get { return Template.Name; } }

		/// <summary>
		/// Is this component out of commission?
		/// </summary>
		public bool IsDamaged { get; set; }
	}
}
