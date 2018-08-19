using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// Clears the private name for an object.
    /// </summary>
    public class ClearPrivateNameCommand : Command<Empire>
    {
        #region Public Constructors

        public ClearPrivateNameCommand(Empire empire, INameable target)
            : base(empire)
        {
            Target = target;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// What are we clearing the name on?
        /// </summary>
        [DoNotSerialize]
        public INameable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<INameable> target { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void Execute()
        {
            Executor.PrivateNames.Remove(target);
        }

        #endregion Public Methods
    }
}
