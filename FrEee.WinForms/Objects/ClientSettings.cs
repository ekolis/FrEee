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
			cfg.Name = "Default";
			cfg.Columns.Add(colIcon.Copy());
			cfg.Columns.Add(colName.Copy());
			cfg.Columns.Add(colStellarSize.Copy());
			cfg.Columns.Add(colSurface.Copy());
			cfg.Columns.Add(colAtmosphere.Copy());
			cfg.Columns.Add(colConditions.Copy());
			cfg.Columns.Add(colMineralsValue.Copy());
			cfg.Columns.Add(colOrganicsValue.Copy());
			cfg.Columns.Add(colRadioactivesValue.Copy());
			cfg.Columns.Add(colOwner.Copy());
			// TODO - filter to uncolonized planets?
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
			cfg.Columns.Add(colAnger.Copy());
			cfg.Columns.Add(colMineralsIncome.Copy());
			cfg.Columns.Add(colOrganicsIncome.Copy());
			cfg.Columns.Add(colRadioactivesIncome.Copy());
			cfg.Columns.Add(colResearchIncome.Copy());
			cfg.Columns.Add(colIntelligenceIncome.Copy());
			// TODO - filter to our colonies
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
		private static readonly GridColumnConfig colPopulation = new GridColumnConfig("PopulationFill", "Pop", typeof(DataGridViewProgressColumn), Color.Green, Format.UnitsBForBillions);
		private static readonly GridColumnConfig colAnger = new GridColumnConfig("Anger", "Anger", typeof(DataGridViewProgressColumn), Color.Red);
		private static readonly GridColumnConfig colRole = new GridColumnConfig("Role", "Role", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colMineralsIncome = new GridColumnConfig("MineralsIncome", "Min", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color, Format.Units);
		private static readonly GridColumnConfig colOrganicsIncome = new GridColumnConfig("OrganicsIncome", "Org", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color, Format.Units);
		private static readonly GridColumnConfig colRadioactivesIncome = new GridColumnConfig("RadioactivesIncome", "Rad", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color, Format.Units);
		private static readonly GridColumnConfig colResearchIncome = new GridColumnConfig("ResearchIncome", "Res", typeof(DataGridViewTextBoxColumn), Resource.Research.Color, Format.Units);
		private static readonly GridColumnConfig colIntelligenceIncome = new GridColumnConfig("IntelligenceIncome", "Int", typeof(DataGridViewTextBoxColumn), Resource.Intelligence.Color, Format.Units);
		private static readonly GridColumnConfig colFacilitySlotsFree = new GridColumnConfig("FacilityFill", "Facil", typeof(DataGridViewProgressColumn), Color.White, Format.Units);
		private static readonly GridColumnConfig colCargoSpaceFree = new GridColumnConfig("CargoFill", "Cargo", typeof(DataGridViewProgressColumn), Color.White, Format.Units);
		private static readonly GridColumnConfig colFirstConstructionItem = new GridColumnConfig("FirstConstructionItem", "Constr", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colFirstConstructionETA = new GridColumnConfig("FirstConstructionEta", "1st ETA", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colConstructionETA = new GridColumnConfig("ConstructionEta", "ETA", typeof(DataGridViewTextBoxColumn), Color.White);
		private static readonly GridColumnConfig colHasSpaceYard = new GridColumnConfig("HasSpaceYard", "SY", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colIsColony = new GridColumnConfig("IsColony", "Col", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colEmergencyBuild = new GridColumnConfig("EmergencyBuild", "Ebuild", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colRepeatBuild = new GridColumnConfig("RepeatBuild", "Repeat", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colOnHold = new GridColumnConfig("OnHold", "Hold", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colIsDomed = new GridColumnConfig("IsDomed", "Domed", typeof(DataGridViewCheckBoxColumn), Color.White);
		private static readonly GridColumnConfig colHasOrders = new GridColumnConfig("HasOrders", "Orders", typeof(DataGridViewCheckBoxColumn), Color.White);

		public static void Initialize()
		{
			// create instance
			Instance = new ClientSettings();

			// create default planet list config
			var cfg = CreateDefaultPlanetListConfig();
			Instance.CurrentPlanetListConfig = cfg;
			Instance.PlanetListConfigs = new List<GridConfig>();
			Instance.PlanetListConfigs.Add(cfg);
			Instance.PlanetListConfigs.Add(CreateDefaultColonyPlanetListConfig());
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
