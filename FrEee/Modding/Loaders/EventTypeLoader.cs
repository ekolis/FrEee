using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System;
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
				et.TargetSelector = rec.GetObject<IEnumerable<object>>("Target Type Selector", et);
				et.Parameters = rec.GetScripts("Parameter", et).ToList();
				et.Actions = rec.GetScripts("Action", et).ToList();

				yield return et;
			}
		}
	}
}