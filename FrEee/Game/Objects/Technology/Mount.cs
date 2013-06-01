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
	public class Mount : IReferrable<Mount>
	{
		// TODO - implement mount

		public Mount()
		{
			Galaxy.Current.Register(this);
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
	}
}
