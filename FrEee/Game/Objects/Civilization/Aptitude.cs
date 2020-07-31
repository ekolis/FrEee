using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A racial aptitude.
	/// </summary>
	[Serializable]
	public class Aptitude : INamed
	{
		// TODO - moddable aptitudes?
		public static IEnumerable<Aptitude> All
		{
			get
			{
				return new Aptitude[]
				{
					PhysicalStrength,
					Intelligence,
					Cunning,
					EnvironmentalResistance,
					Reproduction,
					Happiness,
					Aggressiveness,
					Defensiveness,
					PoliticalSavvy,
					Mining,
					Farming,
					Refining,
					Construction,
					Repair,
					Maintenance,
				};
			}
		}

		public string? AbilityName { get; set; }

		public int Cost { get; set; }

		public string? Description { get; set; }

		public int HighCost { get; set; }

		public int LowCost { get; set; }

		public int MaxPercent { get; set; }

		public int MinPercent { get; set; }

		public string? Name { get; set; }

		public int Threshold { get; set; }

		public static readonly Aptitude Aggressiveness = new Aptitude
		{
			Name = "Aggressiveness",
			AbilityName = "Race - Combat To Hit Offense Plus",
			Description = "Affects accuracy of weapons.",
		};

		public static readonly Aptitude Construction = new Aptitude
		{
			Name = "Construction Aptitude",
			AbilityName = "Race - Construction Aptitude",
			Description = "Affects rate of construction.",
		};

		public static readonly Aptitude Cunning = new Aptitude
		{
			Name = "Cunning",
			AbilityName = "Race Point Generation Modifier - Intelligence",
			Description = "Affects intelligence point generation.",
		};

		public static readonly Aptitude Defensiveness = new Aptitude
		{
			Name = "Defensiveness",
			AbilityName = "Race - Combat To Hit Defense Plus",
			Description = "Affects evasion in combat.",
		};

		public static readonly Aptitude EnvironmentalResistance = new Aptitude
		{
			Name = "Environmental Resistance",
			AbilityName = "Race - Environmental Resistance",
			Description = "Affects penalties to reproduction from poor planetary conditions.",
		};

		public static readonly Aptitude Farming = new Aptitude
		{
			Name = "Farming Aptitude",
			AbilityName = "Resource Gen Modifier Race - Organics",
			Description = "Affects organics resource income.",
		};

		public static readonly Aptitude Happiness = new Aptitude
		{
			Name = "Happiness",
			AbilityName = "Race - Happiness",
			Description = "Affects population happines.",
		};

		public static readonly Aptitude Intelligence = new Aptitude
		{
			Name = "Intelligence",
			AbilityName = "Race Point Generation Modifier - Research",
			Description = "Affects research point generation.",
		};

		public static readonly Aptitude Maintenance = new Aptitude
		{
			Name = "Maintenance Aptitude",
			AbilityName = "Race - Maintenance Aptitude",
			Description = "Affects maintenance costs.",
		};

		public static readonly Aptitude Mining = new Aptitude
		{
			Name = "Mining Aptitude",
			AbilityName = "Resource Gen Modifier Race - Minerals",
			Description = "Affects minerals resource income.",
		};

		public static readonly Aptitude PhysicalStrength = new Aptitude
		{
			Name = "Physical Strength",
			AbilityName = "Race - Physical Strength",
			Description = "Affects ground combat damage.",
		};

		public static readonly Aptitude PoliticalSavvy = new Aptitude
		{
			Name = "Political Savvy",
			AbilityName = "Race - Political Savvy",
			Description = "Affects trade income.",
		};

		public static readonly Aptitude Refining = new Aptitude
		{
			Name = "Refining Aptitude",
			AbilityName = "Resource Gen Modifier Race - Radioactives",
			Description = "Affects radioactives resource income.",
		};

		public static readonly Aptitude Repair = new Aptitude
		{
			Name = "Repair Aptitude",
			AbilityName = "Race - Repair Aptitude",
			Description = "Affects repair rate.",
		};

		public static readonly Aptitude Reproduction = new Aptitude
		{
			Name = "Reproduction",
			AbilityName = "Race - Reproduction",
			Description = "Affects population reproduction rate.",
		};

		public int GetCost(int val)
		{
			if (val > 100)
			{
				var high = 100 + Threshold;
				if (val > high)
					return (val - high) * HighCost + (high - 100) * Cost;
				else
					return (val - 100) * Cost;
			}
			else if (val < 100)
			{
				var low = 100 - Threshold;
				if (val < low)
					return (val - low) * LowCost + (low - 100) * Cost;
				else
					return (val - 100) * Cost;
			}
			else
				return 0;
		}

		public override string ToString() => Name ?? string.Empty;
	}
}
