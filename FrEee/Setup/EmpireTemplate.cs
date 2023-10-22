using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Modding;
using FrEee.Utility;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Extensions;
using FrEee.Serialization;

namespace FrEee.Setup
{
	/// <summary>
	/// A template for configuring an empire.
	/// </summary>
	public class EmpireTemplate : ITemplate<Empire>
	{
		public EmpireTemplate()
		{
		}

		/// <summary>
		/// The name of the AI used by this empire.
		/// </summary>
		// XXX: make this a mod referenced AI
		public string AIName { get; set; }

		/// <summary>
		/// Can random AI empires use this empire template?
		/// </summary>
		public bool AIsCanUse { get; set; }

		/// <summary>
		/// The color used to represent this empire.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// The empire's culture.
		/// </summary>
		[ModReference]
		public Culture Culture { get; set; }

		/// <summary>
		/// The insignia of the empire.
		/// </summary>
		public Image Insignia
		{
			get
			{
				return Pictures.GetIcon(this);
			}
		}

		/// <summary>
		/// The name of this empire's insignia.
		/// </summary>
		public string InsigniaName { get; set; }

		/// <summary>
		/// Is this a minor empire? Minor empires cannot use warp points.
		/// </summary>
		public bool IsMinorEmpire { get; set; }

		/// <summary>
		/// Is this empire controlled by a human player?
		/// </summary>
		public bool IsPlayerEmpire { get; set; }

		/// <summary>
		/// The name of the leader of this empire.
		/// </summary>
		public string LeaderName { get; set; }

		/// <summary>
		/// The name of the leader portrait used by this empire.
		/// </summary>
		public string LeaderPortraitName { get; set; }

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Empire setup points spent.
		/// </summary>
		public int PointsSpent
		{
			get
			{
				var result = 0;
				foreach (var t in PrimaryRace.Traits)
					result += t.Cost.Value;
				result += PrimaryRace.Aptitudes.Sum(kvp => Aptitude.All.FindByName(kvp.Key).GetCost(kvp.Value));
				return result;
			}
		}

		/// <summary>
		/// The native race of this empire.
		/// </summary>
		public Race PrimaryRace { get; set; }

		/// <summary>
		/// The name of the shipset used by this empire.
		/// </summary>
		public string ShipsetName { get; set; }

		/// <summary>
		/// The name of the design names file used by this empire.
		/// </summary>
		public string DesignNamesFile { get; set; }

		public static EmpireTemplate Load(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open);
			var race = Serializer.Deserialize<EmpireTemplate>(fs);
			fs.Close(); fs.Dispose();
			return race;
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
			if (string.IsNullOrWhiteSpace(Name))
				yield return "You must specify a name for your empire.";
			if (string.IsNullOrWhiteSpace(LeaderName))
				yield return "You must specify a leader name for your empire.";
			if (string.IsNullOrWhiteSpace(LeaderPortraitName))
				yield return "You must specify a leader portrait for your empire.";
			if (string.IsNullOrWhiteSpace(InsigniaName))
				yield return "You must specify an insignia for your empire.";
			if (string.IsNullOrWhiteSpace(ShipsetName))
				yield return "You must specify a shipset for your empire.";
			if (Color.R < 85 && Color.G < 85 & Color.B < 85)
				yield return "The color you specified for your empire is too dark to be visible. Please make sure that at least one of the RGB values is 85 or more.";
			if (Color.A < 255)
				yield return "Transparent empire colors are not allowed.";
			if (Culture == null)
				yield return "You must specify a culture for your empire.";
			if (!IsPlayerEmpire && The.Mod.EmpireAIs.FindByName(AIName) == null)
				yield return "AI empires require an AI script.";
			if (PointsSpent > maxPoints)
				yield return "You have spent too many empire setup points. Only " + maxPoints + " are available.";
		}

		public Empire Instantiate(Game game)
		{
			var emp = new Empire();
			emp.Name = Name;
			emp.LeaderName = LeaderName;
			emp.Color = Color;
			emp.PrimaryRace = PrimaryRace;
			emp.LeaderPortraitName = LeaderPortraitName;
			emp.InsigniaName = InsigniaName ?? PrimaryRace.Name;
			emp.ShipsetPath = ShipsetName ?? PrimaryRace.Name;
			emp.LeaderPortraitName = LeaderPortraitName ?? PrimaryRace.Name;
			emp.Culture = Culture;
			emp.IsPlayerEmpire = IsPlayerEmpire;
			emp.IsMinorEmpire = IsMinorEmpire;
			emp.AI = The.Mod.EmpireAIs.FindByName(AIName);
			emp.DesignNamesFile = DesignNamesFile;

			return emp;
		}

		public void Save(string filename)
		{
			var fs = new FileStream(filename, FileMode.Create);
			Serializer.Serialize(this, fs);
			fs.Close(); fs.Dispose();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
