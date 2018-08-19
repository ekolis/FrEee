using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// Clears a player note.
    /// </summary>
    public class ClearPlayerNoteCommand : Command<Empire>
    {
        #region Public Constructors

        public ClearPlayerNoteCommand(IReferrable target)
            : base(Empire.Current)
        {
            Target = target;
        }

        #endregion Public Constructors

        #region Public Properties

        [DoNotSerialize]
        public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<IReferrable> target { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void Execute()
        {
            Executor.PlayerNotes.Remove(target);
        }

        #endregion Public Methods
    }
}
