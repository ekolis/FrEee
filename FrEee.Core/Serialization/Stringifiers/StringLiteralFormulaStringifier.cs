using FrEee.Modding;
using System.ComponentModel.Composition;
using FrEee.Serialization.Stringifiers;

namespace FrEee.Serialization.Stringifiers;

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
