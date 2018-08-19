using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
    public interface IErrorProne
    {
        #region Public Properties

        IEnumerable<string> Errors { get; }

        #endregion Public Properties
    }
}
