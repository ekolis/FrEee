using FrEee.Game.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// A command to some object.
    /// Commands are distinguished from orders by being instantaneous, rather than queued.
    /// </summary>
    public interface ICommand : IPromotable
    {
        #region Public Properties

        IReferrable Executor { get; }

        long ExecutorID { get; }

        /// <summary>
        /// The empire issuing the command.
        /// </summary>
        Empire Issuer { get; }

        /// <summary>
        /// Any new (from the client) objects referred to by this command.
        /// </summary>
        IEnumerable<IReferrable> NewReferrables { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();

        #endregion Public Methods
    }

    /// <summary>
    /// A command to some object.
    /// </summary>
    public interface ICommand<T> : ICommand where T : IReferrable
    {
        #region Public Properties

        /// <summary>
        /// The object whose queue is being manipulated.
        /// </summary>
        new T Executor { get; set; }

        #endregion Public Properties
    }
}
