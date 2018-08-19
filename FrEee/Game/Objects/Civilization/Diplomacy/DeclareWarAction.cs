using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
    /// <summary>
    /// Declares war on the target empire.
    /// </summary>
    public class DeclareWarAction : Action
    {
        #region Public Constructors

        public DeclareWarAction(Empire target)
            : base(target)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Description
        {
            get { return "Declare War"; }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
            foreach (var clause in Executor.GetTreaty(Target))
                clause.Dispose();
            // TODO - some sort of formal war state
            Executor.Log.Add(Target.CreateLogMessage("We have declared war on the " + Target + "."));
            Target.Log.Add(Executor.CreateLogMessage("The " + Target + " has declared war on us!"));
        }

        #endregion Public Methods
    }
}
