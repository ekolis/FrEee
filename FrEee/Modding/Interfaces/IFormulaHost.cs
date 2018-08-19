using System.Collections.Generic;

namespace FrEee.Modding.Interfaces
{
    public interface IFormulaHost
    {
        #region Public Properties

        IDictionary<string, object> Variables { get; }

        #endregion Public Properties
    }
}
