using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Interfaces
{
	public interface IFormula
	{
		string Text { get; set; }

		FormulaType FormulaType { get; set; }

		object Value { get; }
	}

	/// <summary>
	/// A script formula.
	/// </summary>
	/// <typeparam name="T">Return type.</typeparam>
	public interface IFormula<out T> : IFormula
	{
		new T Value { get; }
	}
}
