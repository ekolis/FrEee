using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Commands;
using FrEee.Utility; using Newtonsoft.Json;
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

		private Reference<Empire> target { get; set; }

		/// <summary>
		/// The empire that is the target of this action.
		/// </summary>
		[DoNotSerialize]
		public Empire Target { get { return target; } set { target = value; } }

		public abstract string Description { get; }
	}
}
