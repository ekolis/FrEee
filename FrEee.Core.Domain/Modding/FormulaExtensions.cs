using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Modding
{
	public static class FormulaExtensions
	{
		public static LiteralFormula<T> ToLiteralFormula<T>(this T t)
			where T : IConvertible, IComparable =>
			new(t);
	}
}
