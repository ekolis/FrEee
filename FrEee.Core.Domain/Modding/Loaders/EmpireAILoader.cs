using FrEee.Processes.AI;
using FrEee.Objects.Civilization;
using FrEee.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FrEee.Objects.GameState;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads empire AIs.
/// </summary>
public class EmpireAILoader : ILoader
{
	public EmpireAILoader(string modPath)
	{
		FileName = "AI.script";
		ModPath = modPath;
	}

	public string FileName
	{
		get;
		set;
	}

	public string ModPath
	{
		get;
		set;
	}

	public IEnumerable<IModObject> Load(Mod mod)
	{
		string empsFolder;
		if (ModPath == null)
			//empsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Pictures", "Races");
			empsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts", "AI");
		else
			empsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ModPath, "Scripts", "AI");
		//empsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ModPath, "Pictures", "Races");
		if (Directory.Exists(empsFolder))
		{
			foreach (var empFolder in Directory.GetDirectories(empsFolder))
			{
				if (File.Exists(Path.Combine(empFolder, "AI.py")))
				{
					var script = PythonScript.Load(Path.Combine(empFolder, "AI"));
					if (script == null)
						continue; // script does not exist for this shipset
					var ministers = new SafeDictionary<string, ICollection<string>>();
					string curCategory = "Uncategorized";
					var ministersFile = Path.Combine(empFolder, "AI.ministers");
					if (File.Exists(ministersFile))
					{
						foreach (var line in File.ReadAllLines(ministersFile))
						{
							if (line.StartsWith("\t"))
							{
								// found a minister name
								var ministerName = line.Substring(1);
								if (ministers[curCategory] == null)
									ministers[curCategory] = new List<string>();
								ministers[curCategory].Add(ministerName);
							}
							else
							{
								// found a minister category
								curCategory = line;
							}
						}
					}
					var ai = new PythonAI<Empire, Galaxy>(Path.GetFileName(empFolder), script, ministers);
					mod.EmpireAIs.Add(ai);
					yield return ai;
				}
				//C# AI. 
				else if (File.Exists(Path.Combine(empFolder, "AI.csx")))
				{
					var script = CSScript.Load(Path.Combine(empFolder, "AI"));
					if (script == null)
						continue; // script does not exist for this shipset
					var ministers = new SafeDictionary<string, ICollection<string>>();
					string curCategory = "Uncategorized";
					var ministersFile = Path.Combine(empFolder, "AI.ministers");
					if (File.Exists(ministersFile))
					{
						foreach (var line in File.ReadAllLines(ministersFile))
						{
							if (line.StartsWith("\t"))
							{
								// found a minister name
								var ministerName = line.Substring(1);
								if (ministers[curCategory] == null)
									ministers[curCategory] = new List<string>();
								ministers[curCategory].Add(ministerName);
							}
							else
							{
								// found a minister category
								curCategory = line;
							}
						}
					}
					// TODO: fix CSAI crash loading FrEee.Core.dll
					//var ai = new CSAI<Empire, Galaxy>(Path.GetFileName(empFolder), script, ministers);
					//mod.EmpireAIs.Add(ai);
					//yield return ai;
				}
			}
		}
	}
}
