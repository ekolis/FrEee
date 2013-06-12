using System;
using System.Linq;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A warp point connecting two sectors.
	/// </summary>
	[Serializable]
	public class WarpPoint : StellarObject, ITemplate<WarpPoint>, IReferrable
	{
		/// <summary>
		/// The sector that ships will appear in when they go through this warp point.
		/// </summary>
		public Sector Target { get; set; }

		/// <summary>
		/// Should this warp point be one-way, or should it have a return connection?
		/// </summary>
		public bool IsOneWay { get; set; }

		/// <summary>
		/// Does this warp point have special abilities?
		/// </summary>
		public bool IsUnusual { get; set; }

		/// <summary>
		/// Just copy the warp point's data.
		/// </summary>
		/// <returns>A copy of the warp point.</returns>
		public new WarpPoint Instantiate()
		{
			return this.Clone();
		}

		/// <summary>
		/// The star system that ships will appear in when they go through this warp point.
		/// </summary>
		public ObjectLocation<StarSystem> TargetStarSystemLocation
		{
			get
			{
				return Galaxy.Current.StarSystemLocations.SingleOrDefault(ssl => ssl.Item.Contains(Target));
			}
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}
	}
}
