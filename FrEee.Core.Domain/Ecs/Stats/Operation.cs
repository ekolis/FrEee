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
	/// <summary>
	/// A mathematical operation that can be used to aggregate stat values.
	/// </summary>
	/// <param name="Name"></param>
	/// <param name="Aggregate"></param>
	/// <param name="Priority"></param>
	public record Operation(string Name, Func<decimal, IEnumerable<decimal>, decimal> Aggregate, int Priority)
		: IOperation
	{
		public static IEnumerable<Operation> All => OperationLibrary.Instance.All;

		public static Operation Find(string name) => OperationLibrary.Instance.Find(name);

		[Export(typeof(IOperation))]
		public static readonly Operation Add = new Operation(
			"Add",
			(@base, nums) => @base + nums.Sum(),
			100);

		[Export(typeof(IOperation))]
		public static readonly Operation Subtract = new Operation(
			"Subtract",
			(@base, nums) => @base - nums.Sum(),
			101);

		[Export(typeof(IOperation))]
		public static readonly Operation Multiply = new Operation(
			"Multiply",
			(@base, nums) => @base * (nums.Any() ? nums.Aggregate((a, b) => a * b) : 1),
			200);

		[Export(typeof(IOperation))]
		public static readonly Operation Divide = new Operation(
			"Divide",
			(@base, nums) => @base / (nums.Any() ? nums.Aggregate((a, b) => a * b) : 1),
			201);

		[Export(typeof(IOperation))]
		public static readonly Operation TakeMaximum = new Operation(
			"Take Maximum",
			(@base, nums) => Math.Max(@base, nums.Any() ? nums.Max() : @base),
			300);

		[Export(typeof(IOperation))]
		public static readonly Operation TakeMinimum = new Operation(
			"Take Minimum",
			(@base, nums) => Math.Min(@base, nums.Any() ? nums.Min() : @base),
			301);

		// TODO: more operations like average, median, mode, standard deviation, Pythagoras in various dimensions, geometric mean...
	}

	public interface IOperation
	{
		string Name { get; }
		Func<decimal, IEnumerable<decimal>, decimal> Aggregate { get; }
		int Priority { get; }
	}
}
