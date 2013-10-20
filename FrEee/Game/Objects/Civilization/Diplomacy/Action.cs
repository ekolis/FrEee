using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		/// <summary>
		/// The empire that is the target of this action.
		/// </summary>
		public Empire Target { get; set; }

		public abstract string Description { get; }
	}
}
