using System.ComponentModel.Composition;

using FrEee.Modding;

#nullable enable

namespace FrEee.Utility.Stringifiers
{
	[Export(typeof(IStringifier))]
	public class IntLiteralFormulaStringifier : Stringifier<LiteralFormula<int>>
	{
		public override LiteralFormula<int> Destringify(string s)
		{
			return new LiteralFormula<int>(s);
		}

		public override string Stringify(LiteralFormula<int> t)
		{
			return t.Text;
		}
	}
}
