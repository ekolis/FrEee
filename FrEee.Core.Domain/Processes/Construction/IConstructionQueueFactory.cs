using FrEee.Processes.Construction;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// Builds <see cref="IConstructionQueue">s for <see cref="IConstructor"/>s.
/// </summary>
public interface IConstructionQueueFactory
{
	/// <summary>
	/// Builds a construction queue for a constructor.
	/// </summary>
	/// <param name="constructor"></param>
	/// <returns></returns>
	IConstructionQueue Build(IConstructor constructor);
}