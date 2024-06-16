using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// A stat of some entity, such as cost or thrust points, which is granted by abilities.
	/// </summary>
	public class Stat(string name, IStackingRule? stackingRule = null, params decimal[] values)
	{
		public string Name { get; set; } = name;

		public IStackingRule? StackingRule { get; set; } = stackingRule;

		public IList<decimal> Values { get; set; } = values;

		public decimal Value => (StackingRule ?? new AdditionStackingRule()).Stack(Values);
	}
}
