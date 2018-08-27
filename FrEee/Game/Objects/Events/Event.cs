﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FrEee.Game.Objects.Events
{
	/// <summary>
	/// A specific occurrence of a random event.
	/// </summary>
	public class Event : IPictorial
	{
		public Event(EventTemplate t)
		{
			Template = t;
			TurnNumber = Galaxy.Current.TurnNumber + Template.TimeTillCompletion.Value;
			Dice = new PRNG(GetHashCode());
		}

		/// <summary>
		/// The empires which are affected by this event and need log messages.
		/// </summary>
		public IEnumerable<Empire> AffectedEmpires
		{
			get
			{
				// TODO - affected empires only
				return Galaxy.Current.Empires;
			}
		}

		public Image Icon => Pictures.GetModImage(IconPaths.ToArray());

		public IEnumerable<string> IconPaths => PortraitPaths;

		public Image Portrait => Pictures.GetModImage(PortraitPaths.ToArray());

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				yield return Path.Combine("Events", Template.Picture);
			}
		}

		/// <summary>
		/// The object affected by this script.
		/// </summary>
		public object Target { get; private set; }

		/// <summary>
		/// The template for this event.
		/// </summary>
		public EventTemplate Template { get; private set; }

		/// <summary>
		/// The turn number on which this event occurs.s
		/// </summary>
		public int TurnNumber { get; private set; }

		/// <summary>
		/// Makes this event happen right now.
		/// </summary>
		public void Execute()
		{
			foreach (var m in Template.OccurrenceMessages)
			{
				foreach (var emp in AffectedEmpires)
				{
					var context = new SafeDictionary<string, object>();
					foreach (var parm in Template.Type.Parameters)
						context.Add(parm.Key, parm.Value.Value);
					var text = m.Text.Evaluate(context);
					emp.RecordLog(this, text);
				}
			}

			var dict = new SafeDictionary<string, object>();
			dict.Add("target", Target);
			foreach (var action in Template.Type.Actions)
				ScriptEngine.RunScript(action, dict);
		}

		/// <summary>
		/// Picks a target for this event.
		/// </summary>
		private void RollTarget()
		{
			Target = Template.Type.TargetSelector.Evaluate(this).PickRandom(Dice);
		}

		/// <summary>
		/// A random number generator.
		/// </summary>
		private PRNG Dice { get; } 

		/// <summary>
		/// Warns players who should know about this event.
		/// </summary>
		public void Warn()
		{
			// try to find a target but don't try too hard (roll up to 10 times)
			bool hasTarget = true;
			for (var i = 0; i < 10; i++)
			{
				RollTarget();
				hasTarget = true;
				
				foreach (var p in AffectedEmpires)
				{
					if (RandomHelper.PercentageChance(p.GetAbilityValue("Luck").ToInt() - p.GetAbilityValue("Change Bad Event Chance - Empire").ToInt(), Dice)) ;
						hasTarget = false;
					if (Target is ILocated l)
					{
						if (RandomHelper.PercentageChance(100 + l.StarSystem.GetEmpireAbilityValue(p, "Change Bad Event Chance - System").ToInt(), Dice))
							hasTarget = false;
						if (RandomHelper.PercentageChance(100 + l.Sector.GetEmpireAbilityValue(p, "Change Bad Event Chance - Sector").ToInt(), Dice))
							hasTarget = false;
					}
				}
				if (hasTarget)
					break;
			}
			if (!hasTarget)
			{
				Target = null;
				return; // give up on picking a target
			}

			foreach (var m in Template.WarningMessages)
			{
				foreach (var emp in AffectedEmpires)
				{
					var context = new SafeDictionary<string, object>();
					foreach (var parm in Template.Type.Parameters)
						context.Add(parm.Key, parm.Value.Value);
					var text = m.Text.Evaluate(context);
					emp.RecordLog(this, text);
				}
			}
		}
	}
}