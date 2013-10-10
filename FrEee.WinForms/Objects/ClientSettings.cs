using FrEee.Utility;
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
			cfg.Name = "All";
			cfg.AddColumn("Icon", "Icon", typeof(DataGridViewImageColumn), Color.White);
			cfg.AddColumn("Name", "Name", typeof(DataGridViewTextBoxColumn), Color.White, Sort.Ascending);
			cfg.AddColumn("StellarSize", "Size", typeof(DataGridViewTextBoxColumn), Color.White);
			cfg.AddColumn("Surface", "Surface", typeof(DataGridViewTextBoxColumn), Color.White);
			cfg.AddColumn("Atmosphere", "Atmosphere", typeof(DataGridViewTextBoxColumn), Color.White);
			cfg.AddColumn("MineralsValue", "Min", typeof(DataGridViewTextBoxColumn), Resource.Minerals.Color);
			cfg.AddColumn("OrganicsValue", "Org", typeof(DataGridViewTextBoxColumn), Resource.Organics.Color);
			cfg.AddColumn("RadioactivesValue", "Rad", typeof(DataGridViewTextBoxColumn), Resource.Radioactives.Color);
			cfg.AddColumn("Owner", "Owner", typeof(DataGridViewTextBoxColumn), Color.White);
			return cfg;
		}

		public static void Initialize()
		{
			// create instance
			Instance = new ClientSettings();

			// create default planet list config
			var cfg = CreateDefaultPlanetListConfig();
			Instance.CurrentPlanetListConfig = cfg;
			Instance.PlanetListConfigs = new List<GridConfig>();
			Instance.PlanetListConfigs.Add(cfg);
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
