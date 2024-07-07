using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs
{
	/// <summary>
	/// An interaction that can be taken with an <see cref="IEntity"/> or <see cref="IAbility"/>.
	/// This is really an ECS system, but System means something else in .NET.
	/// </summary>
	public interface IInteraction
	{
		/// <summary>
		/// Executes the interaction. Should be called after passing it to the Interact methods of any appropriate entities/abilities.
		/// </summary>
		void Execute();
	}
}
