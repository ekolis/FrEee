using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Serialization.Stringifiers;

[Export(typeof(IStringifier))]
public class PointStringifier : Stringifier<Point>
{
	public override Point Destringify(string s)
	{
		var split = s.Split(',').Select(x => x.Trim()).ToArray();
		return new Point(int.Parse(split[0]), int.Parse(split[1]));
	}

	public override string Stringify(Point t)
	{
		return $"{t.X}, {t.Y}";
	}
}
