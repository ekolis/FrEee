using FrEee.Modding.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Interfaces
{
	public interface IFormula : IComparable
	{
		string Text { get; set; }

		FormulaType FormulaType { get; set; }

		object Value { get; }
	}

	public interface IFormula<out T> : IFormula
	{
		new T Value { get; }
	}
}
