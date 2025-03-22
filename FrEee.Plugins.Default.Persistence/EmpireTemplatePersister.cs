using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Projects;
using FrEee.Persistence;
using FrEee.Processes.Setup;

namespace FrEee.Plugins.Default.Persistence;

/// <summary>
/// Saves and loads empire templates.
/// </summary>
[Export(typeof(IPlugin))]
public class EmpireTemplatePersister
	: Plugin<IEmpireTemplatePersister>, IEmpireTemplatePersister
{
	public override string Name { get; } = "EmpireTemplatePersister";

	public override IEmpireTemplatePersister Implementation => this;

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
