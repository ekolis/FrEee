using System;
using System.Linq;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;

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
		/// If null, the warp point is still unexplored by the current empire.
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
		public WarpPoint Instantiate()
		{
			return this.Copy();
		}

		/// <summary>
		/// The star system that ships will appear in when they go through this warp point.
		/// </summary>
		public ObjectLocation<StarSystem> TargetStarSystemLocation
		{
			get
			{
				if (Target == null)
					return null;
				return Galaxy.Current.StarSystemLocations.SingleOrDefault(ssl => ssl.Item == Target.StarSystem);
			}
		}

		public override void Redact(Empire emp)
		{
			base.Redact(emp);

			// Don't let players see the target sector coordinates or star system name if it's not explored yet
			if (Target != null)
			{
				var sys = Target.StarSystem;
				if (sys != null)
				{
					if (!sys.ExploredByEmpires.Contains(emp))
					{
						Name = "Warp Point";
						Target = null;
					}
				}
			}
		}

		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.WarpPoint; }
		}

		[DoNotSerialize(false)]
		public override Empire Owner
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("Cannot set the owner of a warp point; it is always null.");
			}
		}
	}
}
