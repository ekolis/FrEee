﻿using FrEee.Game.Objects.Commands;
using FrEee.Utility;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A unilateral diplomatic action.
	/// </summary>
	public abstract class Action : Command<Empire>
	{
		protected Action(Empire target)
			: base(Empire.Current)
		{
			Target = target;
		}

		public abstract string Description { get; }

		/// <summary>
		/// The empire that is the target of this action.
		/// </summary>
		[DoNotSerialize]
		public Empire Target { get { return target; } set { target = value; } }

		private GalaxyReference<Empire> target { get; set; }
	}
}