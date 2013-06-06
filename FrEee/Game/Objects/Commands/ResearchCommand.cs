using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Command to set an empire's research priorities.
	/// </summary>
	public class ResearchCommand : Command<Empire>
	{
		public ResearchCommand(Empire issuer)
			: base(issuer, issuer)
		{
			Spending = new Dictionary<Technology.Technology, int>();
			Queue = new List<Technology.Technology>();
		}

		private Dictionary<Reference<Technology.Technology>, int> spending { get; set; }

		/// <summary>
		/// Priorities for spending, expressed as percentages of total research output.
		/// </summary>
		[DoNotSerialize]
		public IDictionary<Technology.Technology, int> Spending
		{
			get
			{
				return spending.Select(kvp => new KeyValuePair<Technology.Technology, int>(kvp.Key.Value, kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			}
			set
			{
				spending = value.Select(kvp => new KeyValuePair<Reference<Technology.Technology>, int>(kvp.Key.Reference(), kvp.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			}
		}

		private List<Reference<Technology.Technology>> queue { get; set; }

		/// <summary>
		/// Queue for usage of leftover points after spending priorities are taken care of.
		/// </summary>
		[DoNotSerialize]
		public IList<Technology.Technology> Queue
		{
			get
			{
				return queue.Select(t => t.Value).ToList();
			}
			set
			{
				queue = value.Select(t => t.Reference()).ToList();
			}
		}

		public override void Execute()
		{
			// make sure spending is not over 100%
			var totalSpending = Spending.Sum(kvp => kvp.Value);
			if (totalSpending > 100)
			{
				foreach (var tech in Spending.Keys)
				{
					Spending[tech] /= totalSpending / 100;
				}
			}

			// make sure no techs are prioritized or queued that the empire can't research
			foreach (var tech in Spending.Keys.ToArray())
			{
				if (!Target.HasUnlocked(tech))
					Spending[tech] = 0;
			}
			foreach (var tech in Queue.ToArray())
			{
				if (!Target.HasUnlocked(tech))
					Queue.Remove(tech);
			}

			// save to empire
			Target.ResearchSpending.Clear();
			foreach (var kvp in Spending)
				Target.ResearchSpending.Add(kvp);
			Target.ResearchQueue.Clear();
			foreach (var tech in Queue)
				Target.ResearchQueue.Add(tech);
		}
	}
}
