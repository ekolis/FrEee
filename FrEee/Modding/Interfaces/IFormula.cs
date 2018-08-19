using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FrEee.Modding.Interfaces
{
    [DoNotCopy]
    public interface IFormula : IComparable
    {
        #region Public Properties

        object Context { get; }
        bool IsDynamic { get; }
        bool IsLiteral { get; }
        string Text { get; set; }

        object Value { get; }

        #endregion Public Properties

        #region Public Methods

        object Evaluate(IDictionary<string, object> variables);

        object Evaluate(object host);

        Formula<string> ToStringFormula(CultureInfo c = null);

        #endregion Public Methods
    }

    public interface IFormula<out T> : IFormula
    {
        #region Public Properties

        new T Value { get; }

        #endregion Public Properties

        #region Public Methods

        new T Evaluate(IDictionary<string, object> variables);

        new T Evaluate(object host);

        #endregion Public Methods
    }
}
