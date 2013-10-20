using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Enumerations
{
	public enum FormulaType
	{
		/// <summary>
		/// Text should not be evaluated as a formula.
		/// </summary>
		Literal,
		/// <summary>
		/// Formula should be evaluated once on mod load.
		/// </summary>
		Static,
		/// <summary>
		/// Formula should be evaluated at runtime as needed.
		/// </summary>
		Dynamic
	}
}
