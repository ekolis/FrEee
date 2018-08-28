using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads event types from EventTypes.txt.
	/// </summary>
	public class EventTypeLoader : DataFileLoader
	{
		public EventTypeLoader(string modPath)
			: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public const string Filename = "EventTypes.txt";

		public override IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var et = new EventType();
				mod.EventTypes.Add(et);

				et.ModID = rec.Get<string>("ID", et);
				et.Name = rec.Get<string>("Name", et);
				et.Imports = rec.GetScript("Import", et);
				et.TargetSelector = rec.GetObject<IEnumerable<object>>("Target Selector", et);
				et.TargetSelector.ExternalScripts = new Script[] { et.Imports };
				et.Parameters = new SafeDictionary<string, ObjectFormula<object>>();
				var actionParams = new List<Script>();
				foreach (var f in rec.Fields.Where(f => f.Name == "Parameter"))
				{
					var split = f.Value.Split('=').Select(s => s.Trim()).ToArray();
					et.Parameters[split[0]] = new ObjectFormula<object>(split[1], et, true);
					actionParams.Add(new Script("EventType", f.Value));
				}
				et.Action = rec.GetScript("Action", et);
				et.Action.ExternalScripts = actionParams.ConcatSingle(et.Imports).ToArray();

				yield return et;
			}
		}
	}
}