using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A large immobile installation on a colony.
	/// </summary>
	[Serializable]
	public class Facility : IAbilityObject, IConstructable, IOwnable
	{
		public Facility(FacilityTemplate template)
		{
			Template = template;
			ConstructionProgress = new Resources();
		}

		public Empire Owner { get; set; }

		/// <summary>
		/// The template for this facility.
		/// Specifies the basic stats of the facility and its abilities.
		/// </summary>
		public FacilityTemplate Template { get; private set; }

		public IEnumerable<Ability> Abilities
		{
			get { return Template.Abilities; }
		}

		public Resources Cost
		{
			get { return Template.Cost; }
		}

		public Resources ConstructionProgress
		{
			get;
			set;
		}

		[DoNotSerialize] public Image Icon
		{
			get { return Template.Icon; }
		}

		[DoNotSerialize] public Image Portrait
		{
			get { return Template.Portrait; }
		}

		/// <summary>
		/// Places the facility.
		/// </summary>
		/// <param name="sobj">Must be a colonized planet.</param>
		public void Place(ISpaceObject sobj)
		{
			if (sobj is Planet)
			{
				var planet = (Planet)sobj;
				if (planet.Colony == null)
					throw new ArgumentException("Facilities can only be placed on colonized planets.");
				if (planet.Colony.Facilities.Count >= planet.MaxFacilities)
					planet.Colony.Owner.Log.Add(planet.CreateLogMessage(this + " cannot be constructed at " + planet + " because there is no more space available for facilities there."));
				else
					planet.Colony.Facilities.Add(this);
			}
			else
				throw new ArgumentException("Facilities can only be placed on colonized planets.");
		}

		public string Name { get { return Template.Name; } }

		public override string ToString()
		{
			return Name;
		}
	}
}
