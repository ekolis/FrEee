using FrEee.Processes.Construction;
using FrEee.Utility;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// Builds <see cref="IConstructionQueue">s for <see cref="IConstructor"/>s
/// and serves as a utility class for <see cref=IConstructionQueue"/>s.
/// </summary>
public interface IConstructionQueueFactory
{
	/// <summary>
	/// Builds a construction queue for a constructor.
	/// </summary>
	/// <param name="constructor"></param>
	/// <returns></returns>
	IConstructionQueue Build(IConstructor constructor);

	/// <summary>
	/// Computes the rate at which a construction queue can build.
	/// </summary>
	/// <param name="queue"></param>
	/// <returns></returns>
	ResourceQuantity ComputeRate(IConstructionQueue queue);
}