using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A mount that can be applied to a component.
	/// </summary>
	public class Mount : IResearchable
	{
		// TODO - implement mount

		public Mount()
		{
			if (Galaxy.Current != null)
				Galaxy.Current.Register(this);
			TechnologyRequirements = new List<TechnologyRequirement>();
		}

		/// <summary>
		/// For reference tracking.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// No one owns mounts; they are shared.
		/// </summary>
		public Empire Owner { get { return null; } }

		public string Name { get; set; }

		public IList<TechnologyRequirement> TechnologyRequirements
		{
			get;
			private set;
		}

		public string ResearchGroup
		{
			get { return "Mount"; }
		}

		/// <summary>
		/// TODO - pics for mounts?
		/// </summary>
		public System.Drawing.Image Icon
		{
			get { return null; }
		}

		/// <summary>
		/// TODO - pics for mounts?
		/// </summary>
		public System.Drawing.Image Portrait
		{
			get { return null; }
		}
	}
}
