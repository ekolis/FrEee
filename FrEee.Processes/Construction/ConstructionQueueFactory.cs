using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Space;
using FrEee.Processes.Construction;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// Stock implementation of <see cref="IConstructionQueueFactory"/>.
/// </summary>
public class ConstructionQueueFactory
	: IConstructionQueueFactory
{
	public IConstructionQueue Build(IConstructor constructor)
	{
		return new ConstructionQueue(constructor);
	}
}
