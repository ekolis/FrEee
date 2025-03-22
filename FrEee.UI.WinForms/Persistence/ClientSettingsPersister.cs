using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Plugins;
using FrEee.Plugins.Persistence.Default;
using FrEee.UI.WinForms.Objects;
using FrEee.Utility;

namespace FrEee.UI.WinForms.Persistence;

[Export(typeof(IPlugin))]
public class ClientSettingsPersister
	: Plugin<IClientSettingsPersister>, IClientSettingsPersister
{
	public override string Name { get; } = "ClientSettingsPersister";

	public override IClientSettingsPersister Implementation => this;

	/// <summary>
	/// The full path to the client settings file.
	/// </summary>
	private static string FilePath => Path.Combine(ClientUtilities.ApplicationDataPath, "ClientSettings.dat");

	public ClientSettings Load()
	{
		ClientSettings? result = null;
		if (File.Exists(FilePath))
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(FilePath, FileMode.Open);
				result = Serializer.Deserialize<ClientSettings>(fs);
			}
			catch (Exception ex)
			{
				ClientSettings.Initialize();
				result = ClientSettings.Instance;
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
			if (result.PlanetListConfigs == null)
				result.InitializePlanetList();
			if (result.ShipListConfigs == null)
				result.InitializeShipList();
		}
		else
		{
			ClientSettings.Initialize();
			result = ClientSettings.Instance;
		}
		return result;
	}

	public void Save(ClientSettings clientSettings)
	{
		var path = Path.GetDirectoryName(FilePath);
		if (!Directory.Exists(path))
			Directory.CreateDirectory(path);
		var fs = new FileStream(FilePath, FileMode.Create);
		Serializer.Serialize(clientSettings, fs);
		fs.Close(); fs.Dispose();
	}
}
