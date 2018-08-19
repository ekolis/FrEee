using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// Sets the name of an object.
    /// </summary>
    public class SetPublicNameCommand : Command<INameable>
    {
        #region Public Constructors

        public SetPublicNameCommand(INameable target, string name)
            : base(target)
        {
            Name = name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The name to set.
        /// </summary>
        public string Name { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
            Executor.Name = Name;
        }

        #endregion Public Methods
    }
}
