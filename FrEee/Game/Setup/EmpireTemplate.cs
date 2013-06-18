using AutoMapper;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Setup
{
	/// <summary>
	/// A template for configuring an empire.
	/// </summary>
	public class EmpireTemplate : ITemplate<Empire>
	{
		public EmpireTemplate()
		{
			Traits = new List<Trait>();
			IsPlayerEmpire = true;
		}

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The name of the leader of this empire.
		/// </summary>
		public string LeaderName { get; set; }

		/// <summary>
		/// Traits of this empire.
		/// </summary>
		public IList<Trait> Traits { get; set; }

		/// <summary>
		/// The native race of this empire.
		/// </summary>
		public Race PrimaryRace { get; set; }

		/// <summary>
		/// Set this to override the color specified by the race.
		/// </summary>
		public Color? Color { get; set; }

		/// <summary>
		/// Set this to override the insignia name specified by the race.
		/// </summary>
		public string InsigniaName { get; set; }

		public Image Insignia
		{
			get
			{
				return Pictures.GetIcon(this);
			}
		}

		/// <summary>
		/// Set this to override the shipset path specified by the race.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// Set this to override the leader portrait specified by the race.
		/// </summary>
		public string LeaderPortraitName { get; set; }

		/// <summary>
		/// Set this to override the AI specified by the race.
		/// </summary>
		public string AIName { get; set; }

		public string HappinessModelName { get; set; }

		/// <summary>
		/// The empire's cultural happiness model.
		/// </summary>
		[DoNotSerialize]
		[IgnoreMap]
		public HappinessModel HappinessModel
		{
			get { return Mod.Current.HappinessModels.SingleOrDefault(h => h.Name == HappinessModelName); }
			set { HappinessModelName = value == null ? null : value.Name; }
		}

		public string CultureName { get; set; }

		/// <summary>
		/// The empire's culture.
		/// </summary>
		[DoNotSerialize]
		[IgnoreMap]
		public Culture Culture
		{
			get { return Mod.Current.Cultures.SingleOrDefault(c => c.Name == CultureName); }
			set { CultureName = value == null ? null : value.Name; }
		}

		/// <summary>
		/// Is this empire controlled by a human player?
		/// </summary>
		public bool IsPlayerEmpire { get; set; }

		/// <summary>
		/// Is this a minor empire? Minor empires cannot use warp points.
		/// </summary>
		public bool IsMinorEmpire { get; set; }

		public Empire Instantiate()
		{
			var emp = new Empire();
			emp.Name = Name ?? PrimaryRace.EmpireName;
			emp.LeaderName = LeaderName ?? PrimaryRace.LeaderName; ;
			emp.Color = Color ?? PrimaryRace.Color;
			emp.PrimaryRace = PrimaryRace;
			emp.NativeSurface = PrimaryRace.NativeSurface;
			foreach (var t in Traits)
				emp.Traits.Add(t);
			emp.LeaderPortraitName = LeaderPortraitName ?? PrimaryRace.LeaderPortraitName;
			emp.InsigniaName = InsigniaName ?? PrimaryRace.Name;
			emp.ShipsetPath = ShipsetPath ?? PrimaryRace.Name;
			emp.LeaderPortraitName = LeaderPortraitName ?? PrimaryRace.Name;
			// TODO - set empire AI
			emp.HappinessModel = HappinessModel ?? PrimaryRace.HappinessModel;
			emp.Culture = Culture ?? PrimaryRace.Culture;
			emp.IsPlayerEmpire = IsPlayerEmpire;
			emp.IsMinorEmpire = IsMinorEmpire;

			return emp;
		}

		public IEnumerable<string> GetWarnings(int maxPoints)
		{
			if (PrimaryRace == null)
				yield return "You must specify a primary race for your empire.";
			else
			{
				foreach (var w in PrimaryRace.Warnings)
					yield return w;
			}
			if (string.IsNullOrWhiteSpace(Name) && (PrimaryRace == null || string.IsNullOrWhiteSpace(PrimaryRace.EmpireName)))
				yield return "You must specify a name for your empire or a default empire name for your race.";
			if (string.IsNullOrWhiteSpace(LeaderName) && (PrimaryRace == null || string.IsNullOrWhiteSpace(PrimaryRace.LeaderName)))
				yield return "You must specify a leader name for your empire or race.";
			if (string.IsNullOrWhiteSpace(LeaderPortraitName) && (PrimaryRace == null || string.IsNullOrWhiteSpace(PrimaryRace.LeaderPortraitName)))
				yield return "You must specify a leader portrait for your empire or race.";
			if (string.IsNullOrWhiteSpace(InsigniaName) && (PrimaryRace == null || string.IsNullOrWhiteSpace(PrimaryRace.InsigniaName)))
				yield return "You must specify an insignia for your empire or race.";
			if (string.IsNullOrWhiteSpace(ShipsetPath) && (PrimaryRace == null || string.IsNullOrWhiteSpace(PrimaryRace.ShipsetPath)))
				yield return "You must specify a shipset for your empire or race.";
			// TODO - check presence of AI?
			if (HappinessModel == null && (PrimaryRace == null || PrimaryRace.HappinessModel == null))
				yield return "You must specify a happiness model for your empire or race.";
			if (Culture == null && (PrimaryRace == null || PrimaryRace.Culture == null))
				yield return "You must specify a culture for your empire or race.";
			if (PointsSpent > maxPoints)
				yield return "You have spent too many empire setup points. Only " + maxPoints + " are available.";
		}


		/// <summary>
		/// Empire setup points spent.
		/// </summary>
		public int PointsSpent
		{
			get
			{
				int result = 0;
				foreach (var t in PrimaryRace.Traits.Concat(Traits))
					result += t.Cost;
				result += PrimaryRace.Aptitudes.Sum(kvp => Aptitude.All.Find(kvp.Key).GetCost(kvp.Value));
				return result;
			}
		}
	}
}
