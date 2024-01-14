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
public class PopulationModifierStringifier : Stringifier<PopulationModifier>
{
	public override PopulationModifier Destringify(string s)
	{
		var split = s.Split(',').Select(x => x.Trim()).ToArray();
		return new PopulationModifier
		{
			PopulationAmount = long.Parse(split[0]),
			ProductionRate = int.Parse(split[1]),
			ConstructionRate = int.Parse(split[2]),
		};
	}

	public override string Stringify(PopulationModifier t)
	{
		return $"{t.PopulationAmount}, {t.ProductionRate}, {t.ConstructionRate}";
	}
}
