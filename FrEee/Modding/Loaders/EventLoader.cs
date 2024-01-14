using FrEee.Enumerations;
using FrEee.Objects.Events;
using FrEee.Modding.Interfaces;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads event templates from Events.txt.
/// </summary>
public class EventLoader : DataFileLoader
{
	public EventLoader(string modPath)
		: base(modPath, Filename, DataFile.Load(modPath, Filename))
	{
	}

	public const string Filename = "Events.txt";

	public override IEnumerable<IModObject> Load(Mod mod)
	{
		foreach (var rec in DataFile.Records)
		{
			var et = new EventTemplate();
			et.TemplateParameters = rec.Parameters;
			mod.EventTemplates.Add(et);

			et.ModID = rec.Get<string>("ID", et);
			et.Type = mod.EventTypes.FindByName(rec.Get<string>("Type", et));
			et.Severity = rec.Get<EventSeverity>("Severity", et);
			et.EffectAmount = rec.Get<int>("Effect Amount", et);
			et.MessageTarget = rec.Get<EventMessageTarget>("Message To", et);
			et.OccurrenceMessages = LoadMessages(rec, et, "Message").ToList();
			et.Picture = rec.Get<string>("Picture", et);
			et.TimeTillCompletion = rec.Get<int>("Time Till Completion", et);
			et.WarningMessages = LoadMessages(rec, et, "Start Message").ToList();

			yield return et;
		}
	}

	/// <summary>
	/// Loads event messages from a record.
	/// </summary>
	/// <param name="rec"></param>
	public static IEnumerable<EventMessage> LoadMessages(Record rec, EventTemplate et, string prefix)
	{
		int count = 0;
		int index = -1;
		while (true)
		{
			count++;

			var m = new EventMessage();

			var titleField = rec.FindField(new string[]
					{
						$"{prefix} Title {count}",
						$"{prefix} Title"
					}, ref index, false, index + 1);
			if (titleField == null)
				break; // no more messages
			var title = titleField.CreateFormula<string>(et);

			var textField = rec.FindField(new string[]
					{
						$"{prefix} {count}",
						$"{prefix}"
					}, ref index, false, index + 1);
			if (textField == null)
				break; // no more messages
			var text = textField.CreateFormula<string>(et);

			yield return new EventMessage { Title = title, Text = text };
		}
	}
}