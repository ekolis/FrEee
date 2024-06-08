using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace FrEee.Ecs
{
	/// <summary>
	/// An entity in the ECS. Can contain <see cref="Ability"/>s.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// The abilities of the entity.
		/// </summary>
		ISet<IAbility> Abilities { get; }

		/// <summary>
		/// Performs an interaction on this entity.
		/// </summary>
		/// <param name="interaction"></param>
		void Interact(IInteraction interaction);
	}
}
