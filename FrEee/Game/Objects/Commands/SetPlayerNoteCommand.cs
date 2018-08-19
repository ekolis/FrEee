using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// Sets a player note.
    /// </summary>
    public class SetPlayerNoteCommand : Command<Empire>
    {
        #region Public Constructors

        public SetPlayerNoteCommand(IReferrable target, string note)
            : base(Empire.Current)
        {
            Target = target;
            Note = note;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Note { get; set; }

        [DoNotSerialize]
        public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<IReferrable> target { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void Execute()
        {
            Executor.PlayerNotes[target] = Note;
        }

        #endregion Public Methods
    }
}
