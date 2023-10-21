using FrEee.Interfaces;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Utility.Serialization;
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
				et.TemplateParameters = rec.Parameters;
				mod.EventTypes.Add(et);

				et.ModID = rec.Get<string>("ID", et);
				et.Name = rec.Get<string>("Name", et);
				et.Imports = rec.GetScript("Import", et);
				et.Parameters = rec.GetScript("Parameter", et);
				et.TargetSelector = rec.GetReferenceEnumerable<GalaxyReferenceSet<IReferrable>>(new string[] { "Target Selector" }, et);
				et.TargetSelector.ExternalScripts = et.TargetSelector.ExternalScripts.Append(et.Imports).ToArray();
				et.Action = rec.GetScript("Action", et);
				et.Action.ExternalScripts = new PythonScript[] { et.Imports, et.Parameters }.ToHashSet();
				yield return et;
			}
		}
	}
}