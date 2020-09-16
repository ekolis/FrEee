using System.ComponentModel.Composition;

using FrEee.Modding;

#nullable enable

namespace FrEee.Utility.Stringifiers
{
	[Export(typeof(IStringifier))]
	public class StringLiteralFormulaStringifier : Stringifier<LiteralFormula<string>>
	{
		public override LiteralFormula<string> Destringify(string s)
		{
			return new LiteralFormula<string>(s);
		}

		public override string Stringify(LiteralFormula<string> t)
		{
			return t.Text;
		}
	}
}
