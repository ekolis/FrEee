using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.DataGridView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		/// The full path to the client settings file.
		/// </summary>
		public static string FilePath
		{
			get
			{
				return Path.Combine(ClientUtilities.ApplicationDataPath, "ClientSettings.dat");
			}
		}

		public static ClientSettings Instance { get; private set; }

		/// <summary>
		/// Current planet list config.
		/// </summary>
		public GridConfig CurrentPlanetListConfig { get; set; }

		/// <summary>
		/// Current ship list config.
		/// </summary>
		public GridConfig CurrentShipListConfig { get; set; }

		public int EffectsVolume { get; set; }

		/// <summary>
		/// Volume settings.  Valid ranges are 0(off) - 100(full).
		/// </summary>
		public int MasterVolume { get; set; }

		public int MusicVolume { get; set; }

		/// <summary>
		/// Configurations for the planet list.
		/// </summary>
		public IList<GridConfig> PlanetListConfigs { get; private set; }

		public PlayerInfo PlayerInfo { get; set; } = new PlayerInfo();

		/// <summary>
		/// Configurations for the ship list.
		/// </summary>
		public IList<GridConfig> ShipListConfigs { get; private set; }

		// TODO: make this huge list of columns data driven

		private static readonly GridColumnConfig colAnger = new GridColumnConfig("AngerProgress", "Anger", typeof(DataGridViewProgressColumn), Color.Red);

		private static readonly GridColumnConfig colArmorHitpoints = new GridColumnConfig("ArmorHitpointsFill", "Armor HP", typeof(DataGridViewProgressColumn), Color.Yellow, Format.Units);

		private static readonly GridColumnConfig colAtmosphere = new GridColumnConfig("Atmosphere", "Atmosphere", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colCargo = new GridColumnConfig("CargoFill", "Cargo", typeof(DataGridViewProgressColumn), Color.White, Format.Units);

		private static readonly GridColumnConfig colConditions = new GridColumnConfig("ConditionsProgress", "Conditions", typeof(DataGridViewProgressColumn), Color.Green);

		private static readonly GridColumnConfig colConstructionETA = new GridColumnConfig("ConstructionEta", "ETA", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colDesign = new GridColumnConfig("Design", "Design", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colEmergencyBuild = new GridColumnConfig("EmergencyBuild", "Ebuild", typeof(DataGridViewCheckBoxColumn), Color.White);

		private static readonly GridColumnConfig colFacilities = new GridColumnConfig("FacilityFill", "Facil", typeof(DataGridViewProgressColumn), Color.White, Format.Units);

		private static readonly GridColumnConfig colFirstConstructionETA = new GridColumnConfig("FirstConstructionEta", "1st ETA", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colFirstConstructionItem = new GridColumnConfig("FirstConstructionItem", "Constr", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colHasColony = new GridColumnConfig("HasColony", "Col", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, true);

		private static readonly GridColumnConfig colHasNoColony = new GridColumnConfig("HasColony", "Col", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, false);

		private static readonly GridColumnConfig colHasOrders = new GridColumnConfig("HasOrders", "Orders", typeof(DataGridViewCheckBoxColumn), Color.White);

		private static readonly GridColumnConfig colHasSpaceYard = new GridColumnConfig("HasSpaceYard", "SY", typeof(DataGridViewCheckBoxColumn), Color.White);

		private static readonly GridColumnConfig colHull = new GridColumnConfig("Hull", "Hull", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colHullHitpoints = new GridColumnConfig("HullHitpointsFill", "Hull HP", typeof(DataGridViewProgressColumn), Color.White, Format.Units);

		private static readonly GridColumnConfig colIcon = new GridColumnConfig("Icon32", "Icon", typeof(DataGridViewImageColumn), Color.White);

		private static readonly GridColumnConfig colIntelligenceIncome = new GridColumnConfig("IntelligenceIncome", "Int", typeof(DataGridViewTextBoxColumn), Resource.Intelligence.Color, Format.Units);

		private static readonly GridColumnConfig colIsAlien = new GridColumnConfig("IsOurs", "Ours", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, false);

		private static readonly GridColumnConfig colIsDomed = new GridColumnConfig("IsDomed", "Domed", typeof(DataGridViewCheckBoxColumn), Color.White);

		private static readonly GridColumnConfig colIsOurs = new GridColumnConfig("IsOurs", "Ours", typeof(DataGridViewCheckBoxColumn), Color.White, Format.Raw, Sort.None, 0, Filter.Exact, true);

		private static readonly GridColumnConfig colMineralsIncome = new GridColumnConfig("MineralsIncome", "Min", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);

		private static readonly GridColumnConfig colMineralsMaintenance = new GridColumnConfig("MineralsMaintenance", "Min", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);

		private static readonly GridColumnConfig colMineralsValue = new GridColumnConfig("MineralsValue", "Min Val", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);

		private static readonly GridColumnConfig colName = new GridColumnConfig("Name", "Name", typeof(DataGridViewTextBoxColumn), Color.White, Format.Raw, Sort.Ascending);

		private static readonly GridColumnConfig colOnHold = new GridColumnConfig("OnHold", "Hold", typeof(DataGridViewCheckBoxColumn), Color.White);

		private static readonly GridColumnConfig colOrganicsIncome = new GridColumnConfig("OrganicsIncome", "Org", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);

		private static readonly GridColumnConfig colOrganicsMaintenance = new GridColumnConfig("OrganicsMaintenance", "Org", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);

		private static readonly GridColumnConfig colOrganicsValue = new GridColumnConfig("OrganicsValue", "Org Val", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);

		private static readonly GridColumnConfig colOwner = new GridColumnConfig("Owner", "Owner", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colPopulation = new GridColumnConfig("PopulationFill", "Population", typeof(DataGridViewProgressColumn), Color.Green, Format.UnitsBForBillions);

		private static readonly GridColumnConfig colRadioactivesIncome = new GridColumnConfig("RadioactivesIncome", "Rad", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);

		private static readonly GridColumnConfig colRadioactivesMaintenance = new GridColumnConfig("RadioactivesMaintenance", "Rad", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);

		private static readonly GridColumnConfig colRadioactivesValue = new GridColumnConfig("RadioactivesValue", "Rad Val", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);

		private static readonly GridColumnConfig colRepeatBuild = new GridColumnConfig("RepeatBuild", "Repeat", typeof(DataGridViewCheckBoxColumn), Color.White);

		private static readonly GridColumnConfig colResearchIncome = new GridColumnConfig("ResearchIncome", "Res", typeof(DataGridViewTextBoxColumn), Resource.Research.Color, Format.Units);

		private static readonly GridColumnConfig colRole = new GridColumnConfig("Role", "Role", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colShieldHitpoints = new GridColumnConfig("ShieldHitpointsFill", "Shield HP", typeof(DataGridViewProgressColumn), Color.Cyan, Format.Units);

		private static readonly GridColumnConfig colSpeed = new GridColumnConfig("Speed", "Speed", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colStellarSize = new GridColumnConfig("StellarSize", "Size", typeof(DataGridViewTextBoxColumn), Color.White);

		private static readonly GridColumnConfig colSupply = new GridColumnConfig("SupplyFill", "Supply", typeof(DataGridViewProgressColumn), Color.Blue, Format.Units);

		private static readonly GridColumnConfig colSurface = new GridColumnConfig("Surface", "Surface", typeof(DataGridViewTextBoxColumn), Color.White);

		public SafeDictionary<string, Size> WindowSizes { get; private set; } = new SafeDictionary<string, Size>();

		public SafeDictionary<string, FormWindowState> WindowStates { get; private set; } = new SafeDictionary<string, FormWindowState>();

		public SafeDictionary<string, Point> WindowLocations { get; private set; } = new SafeDictionary<string, Point>();

		public static GridConfig CreateDefaultAlienShipListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Alien Ships";
			cfg.Columns.Add(colIcon.Copy());
			cfg.Columns.Add(colOwner.Copy());
			cfg.Columns.Add(colName.Copy());
			cfg.Columns.Add(colDesign.Copy());
			cfg.Columns.Add(colRole.Copy());
			cfg.Columns.Add(colHull.Copy());
			cfg.Columns.Add(colSpeed.Copy());
			cfg.Columns.Add(colHullHitpoints.Copy());
			cfg.Columns.Add(colArmorHitpoints.Copy());
			cfg.Columns.Add(colShieldHitpoints.Copy());
			cfg.Columns.Add(colSupply.Copy());
			cfg.Columns.Add(colCargo.Copy());
			cfg.Columns.Add(colIsAlien.Copy());
			return cfg;
		}

		public static GridConfig CreateDefaultColonyPlanetListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Colonies";
			cfg.Columns.Add(colIcon.Copy());
			cfg.Columns.Add(colName.Copy());
			cfg.Columns.Add(colRole.Copy());
			cfg.Columns.Add(colIsDomed.Copy());
			cfg.Columns.Add(colConditions.Copy());
			cfg.Columns.Add(colPopulation.Copy());
			cfg.Columns.Add(colFacilities.Copy());
			cfg.Columns.Add(colCargo.Copy());
			cfg.Columns.Add(colAnger.Copy());
			cfg.Columns.Add(colHasSpaceYard.Copy());
			cfg.Columns.Add(colMineralsIncome.Copy());
			cfg.Columns.Add(colOrganicsIncome.Copy());
			cfg.Columns.Add(colRadioactivesIncome.Copy());
			cfg.Columns.Add(colResearchIncome.Copy());
			cfg.Columns.Add(colIntelligenceIncome.Copy());
			cfg.Columns.Add(colHasColony.Copy());
			return cfg;
		}

		public static GridConfig CreateDefaultPlanetListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Uncolonized";
			cfg.Columns.Add(colIcon.Copy());
			cfg.Columns.Add(colName.Copy());
			cfg.Columns.Add(colStellarSize.Copy());
			cfg.Columns.Add(colSurface.Copy());
			cfg.Columns.Add(colAtmosphere.Copy());
			cfg.Columns.Add(colConditions.Copy());
			cfg.Columns.Add(colMineralsValue.Copy());
			cfg.Columns.Add(colOrganicsValue.Copy());
			cfg.Columns.Add(colRadioactivesValue.Copy());
			cfg.Columns.Add(colHasNoColony.Copy());
			return cfg;
		}

		public static GridConfig CreateDefaultShipListConfig()
		{
			var cfg = new GridConfig();
			cfg.Name = "Our Ships";
			cfg.Columns.Add(colIcon.Copy());
			cfg.Columns.Add(colName.Copy());
			cfg.Columns.Add(colDesign.Copy());
			cfg.Columns.Add(colRole.Copy());
			cfg.Columns.Add(colHull.Copy());
			cfg.Columns.Add(colSpeed.Copy());
			cfg.Columns.Add(colHullHitpoints.Copy());
			cfg.Columns.Add(colArmorHitpoints.Copy());
			cfg.Columns.Add(colShieldHitpoints.Copy());
			cfg.Columns.Add(colSupply.Copy());
			cfg.Columns.Add(colCargo.Copy());
			cfg.Columns.Add(colMineralsMaintenance.Copy());
			cfg.Columns.Add(colOrganicsMaintenance.Copy());
			cfg.Columns.Add(colRadioactivesMaintenance.Copy());
			cfg.Columns.Add(colHasOrders.Copy());
			cfg.Columns.Add(colIsOurs.Copy());
			return cfg;
		}

		public static void Initialize()
		{
			// create instance
			Instance = new ClientSettings();

			Instance.MasterVolume = 100;
			Instance.MusicVolume = 100;
			Instance.EffectsVolume = 100;

			InitializePlanetList();
			InitializeShipList();
		}

		public static void Load()
		{
			if (File.Exists(FilePath))
			{
				FileStream fs = null;
				try
				{
					fs = new FileStream(FilePath, FileMode.Open);
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
					{
						fs.Close(); fs.Dispose();
					}
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
			var path = Path.GetDirectoryName(FilePath);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			var fs = new FileStream(FilePath, FileMode.Create);
			Serializer.Serialize(Instance, fs);
			fs.Close(); fs.Dispose();
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
	}
}
