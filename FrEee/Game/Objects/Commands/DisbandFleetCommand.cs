using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to disband a fleet.
    /// </summary>
    public class DisbandFleetCommand : Command<Fleet>
    {
        #region Public Constructors

        public DisbandFleetCommand(Fleet fleet)
            : base(fleet)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Execute()
        {
            Executor.Dispose();
        }

        #endregion Public Methods
    }
}
