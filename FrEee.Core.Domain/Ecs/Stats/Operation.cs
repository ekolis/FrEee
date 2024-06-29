using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Vehicles;
using FrEee.Serialization.Stringifiers;
using Svg;

namespace FrEee.Ecs.Stats
{
	public class Operation(Func<decimal, IEnumerable<decimal>, decimal> aggregator, int priority)
	{
		public static IEnumerable<Operation> All => OperationLibrary.Instance.All;

		public int Priority => priority;

		public decimal Aggregate(decimal @base, IEnumerable<decimal> modifiers) =>
			aggregator(@base, modifiers);

		[Export(typeof(IOperation))]
		public static readonly Operation Add = new Operation(
			(@base, nums) => @base + nums.Sum(),
			1);

		[Export(typeof(IOperation))]
		public static readonly Operation Subtract = new Operation(
			(@base, nums) => @base - nums.Sum(),
			2);

		[Export(typeof(IOperation))]
		public static readonly Operation Multiply = new Operation(
			(@base, nums) => @base * nums.Aggregate((a, b) => a * b),
			3);

		[Export(typeof(IOperation))]
		public static readonly Operation Divide = new Operation(
			(@base, nums) => @base / nums.Aggregate((a, b) => a * b),
			4);

		// TODO: more operations like average, median, mode, min, max, standard deviation, Pythagoras in various dimensions, geometric mean...
	}

	public interface IOperation
	{
		Func<decimal, IEnumerable<decimal>, decimal> aggregator { get; }
		int Priority { get; }
	}
}
