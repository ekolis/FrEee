using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FrEee.Modding.Interfaces
{
	[DoNotCopy]
	public interface IFormula : IComparable
	{
		object Context { get; }
		bool IsDynamic { get; }
		bool IsLiteral { get; }
		string Text { get; set; }

		object Value { get; }

		object Evaluate(IDictionary<string, object> variables);

		object Evaluate(object host);

		Formula<string> ToStringFormula(CultureInfo c = null);
	}

	public interface IFormula<out T> : IFormula
	{
		new T Value { get; }

		new T Evaluate(IDictionary<string, object> variables);

		new T Evaluate(object host);
	}
}