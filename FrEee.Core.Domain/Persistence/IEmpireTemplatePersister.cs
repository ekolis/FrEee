using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Plugins;
using FrEee.Processes.Setup;

namespace FrEee.Persistence;

/// <summary>
/// Saves and loads empire templates.
/// </summary>
public interface IEmpireTemplatePersister
	: IPlugin<IEmpireTemplatePersister>
{
	/// <summary>
	/// Loads an empire template from a file.
	/// </summary>
	/// <param name="filename"></param>
	/// <returns></returns>
	EmpireTemplate LoadFromFile(string filename);

	/// <summary>
	/// Saves an empire template to a file.
	/// </summary>
	/// <param name="empireTemplate"></param>
	/// <param name="filename"></param>
	void SaveToFile(EmpireTemplate empireTemplate, string filename);
}
