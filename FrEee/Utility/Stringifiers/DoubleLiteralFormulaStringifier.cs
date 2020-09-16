using System.ComponentModel.Composition;

using FrEee.Modding;

#nullable enable

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
