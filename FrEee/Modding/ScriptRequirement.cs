using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A requirement that a script return true.
	/// WARNING: script execution is rather slow; use sparingly!
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ScriptRequirement<T> : Requirement<T>
	{
		public ScriptRequirement(Formula<bool> formula, Formula<string> description)
			: base(description)
		{
			Formula = formula;
		}

		public Formula<bool> Formula { get; set; }

		public override bool IsMetBy(T obj)
		{
			return Formula.Evaluate(obj);
		}

		public override bool IsStrict
		{
			get { return false; }
		}
	}
}
