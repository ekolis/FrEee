using FrEee.Modding.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;
using System.Globalization;

namespace FrEee.Modding.Interfaces
{
	[DoNotCopy]
	public interface IFormula : IComparable
	{
		string Text { get; set; }

		object Value { get; }

		object Context { get; }

		object Evaluate(IDictionary<string, object> variables);

		object Evaluate(object host);

		Formula<string> ToStringFormula(CultureInfo c = null);

		bool IsLiteral { get; }

		bool IsDynamic { get; }
	}

	public interface IFormula<out T> : IFormula
	{
		new T Value { get; }

		new T Evaluate(IDictionary<string, object> variables);

		new T Evaluate(object host);
	}
}
