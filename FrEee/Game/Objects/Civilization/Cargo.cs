using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// Cargo stored on a colony or ship/base.
	/// </summary>
	public class Cargo
	{
		public Cargo()
		{
			Population = new SafeDictionary<Race, long>();
			Units = new HashSet<Unit>();
		}

		/// <summary>
		/// The population stored in cargo.
		/// </summary>
		public SafeDictionary<Race, long> Population { get; set; }

		/// <summary>
		/// The units stored in cargo.
		/// </summary>
		public ICollection<Unit> Units { get; set; }

		private int? fakeSize { get; set; }

		/// <summary>
		/// Sets this cargo's fake size to its size and clears the actual population and units.
		/// Used for fog of war.
		/// </summary>
		public void SetFakeSize()
		{
			fakeSize = Size;
			Population.Clear();
			Units.Clear();
		}

		/// <summary>
		/// The amount of space taken by this cargo.
		/// </summary>
		public int Size
		{
			get
			{
				if (fakeSize != null)
					return fakeSize.Value;

				// TODO - moddable population size, perhaps per race?
				return (int)Math.Round(Population.Sum(kvp => kvp.Value) * 5 / 1e6) + Units.Sum(u => u.Design.Hull.Size);
			}
		}
	}
}
