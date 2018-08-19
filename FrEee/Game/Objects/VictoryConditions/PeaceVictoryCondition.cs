using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.VictoryConditions
{
    /// <summary>
    /// Maintain galactic peace for a specified duration.
    /// </summary>
    public class PeaceVictoryCondition : IVictoryCondition
    {
        #region Public Constructors

        public PeaceVictoryCondition(int turns)
        {
            Turns = turns;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The number of turns of peace.
        /// </summary>
        public int Turns { get; set; }

        #endregion Public Properties

        #region Public Methods

        public string GetDefeatMessage(Empire emp, IEnumerable<Empire> winners)
        {
            return "The " + emp + " must have been a warmongering menace! The following empires have achieved a peace victory: " + string.Join(", ", winners.Select(e => e.ToString()).ToArray()) + ".";
        }

        public double GetProgress(Empire emp)
        {
            if (emp.IsDefeated)
                return 0;
            return (double)Galaxy.Current.TurnsOfPeace / (double)Turns;
        }

        public string GetVictoryMessage(Empire emp)
        {
            return "The " + emp + " has, with the help of its allies, ushered in a new era of peace! All hail " + emp.LeaderName + ", master diplomat!";
        }

        #endregion Public Methods
    }
}
