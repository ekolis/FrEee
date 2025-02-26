﻿using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;

namespace FrEee.Serialization.Stringifiers;

[Export(typeof(IStringifier))]
public class SizeStringifier : Stringifier<Size>
{
	public override Size Destringify(string s)
	{
		var split = s.Split(',').Select(x => x.Trim()).ToArray();
		return new Size(int.Parse(split[0]), int.Parse(split[1]));
	}

	public override string Stringify(Size t)
	{
		return $"{t.Width}, {t.Height}";
	}
}
