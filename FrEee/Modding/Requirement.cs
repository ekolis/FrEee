using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A requirement for some condition.
	/// </summary>
	public class Requirement
	{
		public Requirement(Formula<bool> formula, Formula<string> description)
		{
			Formula = formula;
			Description = description;
		}

		public Formula<bool> Formula { get; set; }
		public Formula<string> Description { get; set; }
	}
}
