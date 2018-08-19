﻿using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Linq;
using Tech = FrEee.Game.Objects.Technology.Technology;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// Command to set an empire's research priorities.
    /// </summary>
    public class ResearchCommand : Command<Empire>
    {
        #region Public Constructors

        public ResearchCommand()
            : base(Empire.Current)
        {
            Spending = new ModReferenceKeyedDictionary<Tech, int>();
            Queue = new ModReferenceList<Tech>();
        }

        #endregion Public Constructors

        #region Public Properties

        public ModReferenceList<Tech> Queue { get; private set; }
        public ModReferenceKeyedDictionary<Tech, int> Spending { get; private set; }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods
    }
}
