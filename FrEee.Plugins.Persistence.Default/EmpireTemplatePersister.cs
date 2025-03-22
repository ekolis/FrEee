using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Persistence;
using FrEee.Processes.Setup;

namespace FrEee.Plugins.Persistence.Default;

/// <summary>
/// Saves and loads empire templates.
/// </summary>
public class EmpireTemplatePersister
	: IEmpireTemplatePersister
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "EmpireTemplatePersister";
	public Version Version { get; } = IPlugin.DefaultVersion;

	public EmpireTemplate LoadFromFile(string filename)
	{
		using FileStream fs = new(filename, FileMode.Open);
		return Serializer.Deserialize<EmpireTemplate>(fs);
	}

	public void SaveToFile(EmpireTemplate empireTemplate, string filename)
	{
		using FileStream fs = new(filename, FileMode.Create);
		Serializer.Serialize(this, fs);
	}
}
