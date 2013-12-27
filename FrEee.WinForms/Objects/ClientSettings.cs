using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.DataGridView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Objects
{
	/// <summary>
	/// Client settings that don't need to be persisted to the server.
	/// </summary>
	public class ClientSettings
	{
		static ClientSettings()
		{
			Load();
		}

		/// <summary>
		/// Configurations for the planet list.
		/// </summary>
		public IList<GridConfig> PlanetListConfigs { get; private set; }

		/// <summary>
		/// Current planet list config.
		/// </summary>
		public GridConfig CurrentPlanetListConfig { get; set; }

		/// <summary>
		/// Configurations for the ship list.
		/// </summary>
		public IList<GridConfig> ShipListConfigs { get; private set; }

		/// <summary>
		/// Current ship list config.
		/// </summary>
		public GridConfig CurrentShipListConfig { get; set; }

		public static ClientSettings Instance { get; private set; }

		public static string ConfigFile
		{
			get
			{
				return Path.Combine(
					Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming).FilePath),
					"ClientSettings.dat");
			}
		}

		public static GridConfig CreateDefaultPlanetListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Uncolonized";
			cfg.Columns.Add(colIcon.Copy(true));
			cfg.Columns.Add(colName.Copy(true));
			cfg.Columns.Add(colStellarSize.Copy(true));
			cfg.Columns.Add(colSurface.Copy(true));
			cfg.Columns.Add(colAtmosphere.Copy(true));
			cfg.Columns.Add(colConditions.Copy(true));
			cfg.Columns.Add(colMineralsValue.Copy(true));
			cfg.Columns.Add(colOrganicsValue.Copy(true));
			cfg.Columns.Add(colRadioactivesValue.Copy(true));
			cfg.Columns.Add(colHasNoColony.Copy(true));
			return cfg;
		}

		public static GridConfig CreateDefaultColonyPlanetListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Colonies";
			cfg.Columns.Add(colIcon.Copy(true));
			cfg.Columns.Add(colName.Copy(true));
			cfg.Columns.Add(colRole.Copy(true));
			cfg.Columns.Add(colIsDomed.Copy(true));
			cfg.Columns.Add(colConditions.Copy(true));
			cfg.Columns.Add(colPopulation.Copy(true));
			cfg.Columns.Add(colFacilities.Copy(true));
			cfg.Columns.Add(colCargo.Copy(true));
			cfg.Columns.Add(colAnger.Copy(true));
			cfg.Columns.Add(colHasSpaceYard.Copy(true));
			cfg.Columns.Add(colMineralsIncome.Copy(true));
			cfg.Columns.Add(colOrganicsIncome.Copy(true));
			cfg.Columns.Add(colRadioactivesIncome.Copy(true));
			cfg.Columns.Add(colResearchIncome.Copy(true));
			cfg.Columns.Add(colIntelligenceIncome.Copy(true));
			cfg.Columns.Add(colHasColony.Copy(true));
			return cfg;
		}

		public static GridConfig CreateDefaultShipListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Our Ships";
			cfg.Columns.Add(colIcon.Copy(true));
			cfg.Columns.Add(colName.Copy(true));
			cfg.Columns.Add(colDesign.Copy(true));
			cfg.Columns.Add(colRole.Copy(true));
			cfg.Columns.Add(colHull.Copy(true));
			cfg.Columns.Add(colSpeed.Copy(true));
			cfg.Columns.Add(colHullHitpoints.Copy(true));
			cfg.Columns.Add(colArmorHitpoints.Copy(true));
			cfg.Columns.Add(colShieldHitpoints.Copy(true));
			cfg.Columns.Add(colSupply.Copy(true));
			cfg.Columns.Add(colCargo.Copy(true));
			cfg.Columns.Add(colMineralsMaintenance.Copy(true));
			cfg.Columns.Add(colOrganicsMaintenance.Copy(true));
			cfg.Columns.Add(colRadioactivesMaintenance.Copy(true));
			cfg.Columns.Add(colHasOrders.Copy(true));
			cfg.Columns.Add(colIsOurs.Copy(true));
			return cfg;
		}

		public static GridConfig CreateDefaultAlienShipListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Alien Ships";
			cfg.Columns.Add(colIcon.Copy(true));
			cfg.Columns.Add(colOwner.Copy(true));
			cfg.Columns.Add(colName.Copy(true));
			cfg.Columns.Add(colDesign.Copy(true));
			cfg.Columns.Add(colRole.Copy(true));
			cfg.Columns.Add(colHull.Copy(true));
			cfg.Columns.Add(colSpeed.Copy(true));
			cfg.Columns.Add(colHullHitpoints.Copy(true));
			cfg.Columns.Add(colArmorHitpoints.Copy(true));
			cfg.Columns.Add(colShieldHitpoints.Copy(true));
			cfg.Columns.Add(colSupply.Copy(true));
			cfg.Columns.Add(colCargo.Copy(true));
			cfg.Columns.Add(colIsAlien.Copy(true));
			return cfg;
		}

		private static readonly GridColumnConfig colIcon = new GridColumnConfig("Icon", "Icon", typeof(DataGridViewImageColumn), Color.White);
		private static readonly GridColumnConfig colName = new GridColumnConfig("Name", "Name", typeof(DataGridViewTextBoxColumn), Color.White, Format.Raw, Sort.Ascending);
		private static readonly GridColumnConfig colStellarSize = new GridColumnConfig("StellarSize", "Size", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colSurface = new GridColumnConfig("Surface", "Surface", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colAtmosphere = new GridColumnConfig("Atmosphere", "Atmosphere", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colConditions = new GridColumnConfig("Conditions", "Conditions", typeof(DataGridViewProgressColumn), Color.Green);
		private static readonly GridColumnConfig colMineralsValue = new GridColumnConfig("MineralsValue", "Min Val", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);
		private static readonly GridColumnConfig colOrganicsValue = new GridColumnConfig("OrganicsValue", "Org Val", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);
		private static readonly GridColumnConfig colRadioactivesValue = new GridColumnConfig("RadioactivesValue", "Rad Val", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);
		private static readonly GridColumnConfig colOwner = new GridColumnConfig("Owner", "Owner", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colPopulation = new GridColumnConfig("PopulationFill", "Population", typeof(DataGridViewProgressColumn), Color.Green, Format.UnitsBForBillions);
		private static readonly GridColumnConfig colAnger = new GridColumnConfig("Anger", "Anger", typeof(DataGridViewProgressColumn), Color.Red);
		private static readonly GridColumnConfig colRole = new GridColumnConfig("Role", "Role", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colMineralsIncome = new GridColumnConfig("MineralsIncome", "Min", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);
		private static readonly GridColumnConfig colOrganicsIncome = new GridColumnConfig("OrganicsIncome", "Org", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);
		private static readonly GridColumnConfig colRadioactivesIncome = new GridColumnConfig("RadioactivesIncome", "Rad", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);
		private static readonly GridColumnConfig colResearchIncome = new GridColumnConfig("ResearchIncome", "Res", typeof(DataGridViewTextBoxColumn), Resource.Research.Color, Format.Units);
		private static readonly GridColumnConfig colIntelligenceIncome = new GridColumnConfig("IntelligenceIncome", "Int", typeof(DataGridViewTextBoxColumn), Resource.Intelligence.Color, Format.Units);
		private static readonly GridColumnConfig colFacilities = new GridColumnConfig("FacilityFill", "Facil", typeof(DataGridViewProgressColumn), Color.White, Format.Units);
		private static readonly GridColumnConfig colCargo = new GridColumnConfig("CargoFill", "Cargo", typeof(DataGridViewProgressColumn), Color.White, Format.Units);
		private static readonly GridColumnConfig colFirstConstructionItem = new GridColumnConfig("FirstConstructionItem", "Constr", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colFirstConstructionETA = new GridColumnConfig("FirstConstructionEta", "1st ETA", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colConstructionETA = new GridColumnConfig("ConstructionEta", "ETA", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colHasSpaceYard = new GridColumnConfig("HasSpaceYard", "SY", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colHasColony = new GridColumnConfig("HasColony", "Col", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, true);
		private static readonly GridColumnConfig colHasNoColony = new GridColumnConfig("HasColony", "Col", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, false);
		private static readonly GridColumnConfig colEmergencyBuild = new GridColumnConfig("EmergencyBuild", "Ebuild", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colRepeatBuild = new GridColumnConfig("RepeatBuild", "Repeat", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colOnHold = new GridColumnConfig("OnHold", "Hold", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colIsDomed = new GridColumnConfig("IsDomed", "Domed", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colHasOrders = new GridColumnConfig("HasOrders", "Orders", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colHull = new GridColumnConfig("Hull", "Hull", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colDesign = new GridColumnConfig("Design", "Design", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colSpeed = new GridColumnConfig("Speed", "Speed", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colHullHitpoints = new GridColumnConfig("HullHitpointsFill", "Hull HP", typeof(DataGridViewProgressColumn), Color.White, Format.Units);
		private static readonly GridColumnConfig colArmorHitpoints = new GridColumnConfig("ArmorHitpointsFill", "Armor HP", typeof(DataGridViewProgressColumn), Color.Yellow, Format.Units);
		private static readonly GridColumnConfig colShieldHitpoints = new GridColumnConfig("ShieldHitpointsFill", "Shield HP", typeof(DataGridViewProgressColumn), Color.Cyan, Format.Units);
		private static readonly GridColumnConfig colSupply = new GridColumnConfig("SupplyFill", "Supply", typeof(DataGridViewProgressColumn), Color.Blue, Format.Units);
		private static readonly GridColumnConfig colMineralsMaintenance = new GridColumnConfig("MineralsMaintenance", "Min", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);
		private static readonly GridColumnConfig colOrganicsMaintenance = new GridColumnConfig("OrganicsMaintenance", "Org", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);
		private static readonly GridColumnConfig colRadioactivesMaintenance = new GridColumnConfig("RadioactivesMaintenance", "Rad", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);
		private static readonly GridColumnConfig colIsOurs = new GridColumnConfig("IsOurs", "Ours", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, true);
		private static readonly GridColumnConfig colIsAlien = new GridColumnConfig("IsOurs", "Ours", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, false);


		public static void Initialize()
		{
			// create instance
			Instance = new ClientSettings();

			InitializePlanetList();
		}

		private static void InitializePlanetList()
		{
			// create default planet list config
			var cfg = CreateDefaultPlanetListConfig();
			Instance.CurrentPlanetListConfig = cfg;
			Instance.PlanetListConfigs = new List<GridConfig>();
			Instance.PlanetListConfigs.Add(cfg);
			Instance.PlanetListConfigs.Add(CreateDefaultColonyPlanetListConfig());
		}

		private static void InitializeShipList()
		{
			// create default ship list config
			var cfg = CreateDefaultShipListConfig();
			Instance.CurrentShipListConfig = cfg;
			Instance.ShipListConfigs = new List<GridConfig>();
			Instance.ShipListConfigs.Add(cfg);
			Instance.ShipListConfigs.Add(CreateDefaultAlienShipListConfig());
		}

		public static void Load()
		{
			if (File.Exists(ConfigFile))
			{
				FileStream fs = null;
				try
				{
					fs = new FileStream(ConfigFile, FileMode.Open);
					Instance = Serializer.Deserialize<ClientSettings>(fs);
				}
				catch (Exception ex)
				{
					Initialize();
					Console.Error.WriteLine(ex);
				}
				finally
				{
					if (fs != null)
						fs.Close();
				}

				// initialize anything that wasn't there
				if (Instance.PlanetListConfigs == null)
					InitializePlanetList();
				if (Instance.ShipListConfigs == null)
					InitializeShipList();
			}
			else
				Initialize();
		}

		public static void Save()
		{
			var path = Path.GetDirectoryName(ConfigFile);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			var fs = new FileStream(ConfigFile, FileMode.Create);
			Serializer.Serialize(Instance, fs);
			fs.Close();
		}
	}
}
