using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility.Stringifiers
{
	[Export(typeof(IStringifier))]
	public class DoubleLiteralFormulaStringifier : Stringifier<LiteralFormula<double>>
	{
		public override LiteralFormula<double> Destringify(string s)
		{
			return new LiteralFormula<double>(s);
		}

		public override string Stringify(LiteralFormula<double> t)
		{
			return t.Text;
		}
	}
}
