using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
