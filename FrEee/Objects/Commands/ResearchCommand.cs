using FrEee.Objects.Civilization;
using FrEee.Extensions;
using System.Linq;
using Tech = FrEee.Objects.Technology.Technology;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;
using System.Collections.Generic;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// Command to set an empire's research priorities.
	/// </summary>
	public class ResearchCommand : Command<Empire>
	{
		public ResearchCommand()
			: base(Empire.Current)
		{
			Spending = new SafeDictionary<Tech, int>();
			Queue = new List<Tech>();
		}

		[ModReferenceEnumerable]
		public IList<Tech> Queue { get; private set; }
		[ModReferenceKeyedDictionary]
		public IDictionary<Tech, int> Spending { get; private set; }

		public override void Execute()
		{
			// make sure spending is not over 100%
			var totalSpending = Spending.Sum(kvp => kvp.Value);
			if (totalSpending > 100)
			{
				foreach (var kvp in Spending.ToArray())
				{
					Spending[kvp.Key] = kvp.Value / totalSpending / 100;
				}
			}

			// make sure no techs are prioritized or queued that the empire can't research
			foreach (var kvp in Spending.ToArray())
			{
				if (!Executor.HasUnlocked(kvp.Key))
					Spending[kvp.Key] = 0;
			}
			foreach (Technology.Technology tech in Queue.ToArray())
			{
				if (!Executor.HasUnlocked(tech))
					Queue.Remove(tech);
			}

			// save to empire
			Executor.ResearchSpending.Clear();
			foreach (var kvp in Spending)
				Executor.ResearchSpending.Add(kvp);
			Executor.ResearchQueue.Clear();
			foreach (var tech in Queue)
				Executor.ResearchQueue.Add(tech);
		}
	}
}
