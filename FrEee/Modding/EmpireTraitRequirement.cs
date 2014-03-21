﻿using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// Requirement that an empire's primary race have or not have some trait. 
	/// </summary>
	public class EmpireTraitRequirement : Requirement<Empire>
	{
		public EmpireTraitRequirement(Trait trait, bool requiredOrForbidden)
			: base(requiredOrForbidden ? ("Requires " + trait + ".") : ("Empire cannot have " + trait + "."))
		{
			Trait = trait;
			RequiredOrForbidden = requiredOrForbidden;
		}

		public override bool IsMetBy(Empire obj)
		{
			return obj.PrimaryRace.Traits.Contains(Trait) == RequiredOrForbidden;
		}

		/// <summary>
		/// The trait in question
		/// </summary>
		public Trait Trait { get; set; }

		/// <summary>
		/// Is this a required trait (true) or a forbidden one (false)?
		/// </summary>
		public bool RequiredOrForbidden { get; set; }

		public override bool IsStrict
		{
			get { return RequiredOrForbidden; }
		}
	}
}
