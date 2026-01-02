using FrEee.Plugins;
using FrEee.Utility;

namespace FrEee.Processes.Construction;

/// <summary>
/// Creates <see cref="IConstructionQueue">s for <see cref="IConstructor"/>s
/// and serves as a utility class for <see cref=IConstructionQueue"/>s.
/// </summary>
public interface IConstructionQueueService
	: IPlugin<IConstructionQueueService>
{
	/// <summary>
	/// Creates a construction queue for a constructor.
	/// </summary>
	/// <param name="constructor"></param>
	/// <returns></returns>
	IConstructionQueue CreateConstructionQueue(IConstructor constructor);

	/// <summary>
	/// Computes the rate at which a construction queue can build.
	/// </summary>
	/// <param name="queue"></param>
	/// <returns></returns>
	ResourceQuantity ComputeRate(IConstructionQueue queue);
}