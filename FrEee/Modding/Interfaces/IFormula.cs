using FrEee.Modding.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;

namespace FrEee.Modding.Interfaces
{
	[DoNotCopy]
	public interface IFormula : IComparable
	{
		string Text { get; set; }

		FormulaType FormulaType { get; set; }

		object Value { get; }

		object Context { get; set; }

		object Evaluate(IDictionary<string, object> variables);

		object Evaluate(object host);

		Formula<string> ToStringFormula();
	}

	public interface IFormula<out T> : IFormula
	{
		new T Value { get; }

		new T Evaluate(IDictionary<string, object> variables);

		new T Evaluate(object host);
	}
}
