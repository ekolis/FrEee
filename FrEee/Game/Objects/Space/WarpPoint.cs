using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Linq;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A warp point connecting two sectors.
	/// </summary>
	[Serializable]
	public class WarpPoint : StellarObject, ITemplate<WarpPoint>, IReferrable, IDataObject
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.WarpPoint; }
		}

		public override bool CanBeObscured => false;

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var dict = base.Data;
				dict[nameof(Target)] = Target;
				dict[nameof(IsUnusual)] = IsUnusual;
				return dict;
			}
			set
			{
				base.Data = value;
				Target = value[nameof(Target)].Default<Sector>();
				IsUnusual = value[nameof(IsUnusual)].Default<bool>();
			}
		}

		/// <summary>
		/// Should this warp point be one-way, or should it have a return connection?
		/// </summary>
		[DoNotSerialize]
		public bool IsOneWay { get; set; }

		/// <summary>
		/// Does this warp point have special abilities?
		/// </summary>
		public bool IsUnusual { get; set; }

		public Empire Owner
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// The sector that ships will appear in when they go through this warp point.
		/// If null, the warp point is still unexplored by the current empire.
		/// </summary>
		public Sector Target { get; set; }

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

		public void Close(bool closeBothWays = true)
		{
			if (closeBothWays)
			{
				var wps = Target.SpaceObjects.OfType<WarpPoint>().Where(q => q.Target == Sector);
				foreach (var wp in wps)
					wp.Dispose();
			}
			Dispose();
		}

		/// <summary>
		/// Just copy the warp point's data.
		/// </summary>
		/// <returns>A copy of the warp point.</returns>
		public WarpPoint Instantiate()
		{
			return this.CopyAndAssignNewID();
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
				else
				{
					Name = "Warp Point";
				}
			}
			else
			{
				Name = "Warp Point";
			}
		}
	}
}