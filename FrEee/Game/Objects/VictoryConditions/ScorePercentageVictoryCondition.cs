using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.VictoryConditions
{
    /// <summary>
    /// Reach a specified percentage of the second place player's score.
    /// </summary>
    public class ScorePercentageVictoryCondition : IVictoryCondition
    {
        #region Public Constructors

        public ScorePercentageVictoryCondition(int percentage)
        {
            Percentage = percentage;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The percentage needed to achieve.
        /// </summary>
        public int Percentage { get; set; }

        #endregion Public Properties

        #region Public Methods

        public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
        {
            return "The " + emp + " has stumbled where others flourished! The " + winners.Single() + " has achieved a score percentage victory.";
        }

        public double GetProgress(Empire emp)
        {
            if (emp.IsDefeated)
                return 0;
            var secondPlace = Galaxy.Current.Empires.OrderByDescending(e => e.Scores).ElementAtOrDefault(1);
            if (secondPlace == null)
                return double.PositiveInfinity;
            return (double)(int)(emp.Score) / ((double)(int)secondPlace.Score * (double)Percentage / 100d);
        }

        public string GetVictoryMessage(Empire emp)
        {
            return "The " + emp + " has vastly outstripped her peers! All hail " + emp.LeaderName + ", leader of a great empire!";
        }

        #endregion Public Methods
    }
}
