using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// A requirement for some condition.
	/// </summary>
	/// <typeparam name="T">The object which needs to meet the requirement.</typeparam>
	public abstract class Requirement<T>
	{
		public Requirement(Formula<string> description)
		{
			Description = description;
		}

		public abstract bool IsMetBy(T obj);

		public Formula<string> Description { get; set; }
	}
}
