using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Plugins;
using FrEee.UI.WinForms.Objects;

namespace FrEee.UI.WinForms.Persistence;

/// <summary>
/// Saves and loads client settings.
/// </summary>
public interface IClientSettingsPersister
	: IPlugin
{
	/// <summary>
	/// Saves client settings.
	/// </summary>
	/// <param name="clientSettings"></param>
	public void Save(ClientSettings clientSettings);

	/// <summary>
	/// Loads client settings.
	/// </summary>
	/// <returns></returns>
	public ClientSettings Load();
}
