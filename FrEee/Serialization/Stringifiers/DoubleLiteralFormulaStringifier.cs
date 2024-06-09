using FrEee.Modding;
using System.ComponentModel.Composition;
using FrEee.Serialization.Stringifiers;

namespace FrEee.Serialization.Stringifiers;

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
