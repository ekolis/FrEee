using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace FrEee.Ecs
{
	/// <summary>
	/// An entity in the ECS. Can contain <see cref="IAbility"/>s.
	/// </summary>
	public interface IEntity<TAbility>
		where TAbility : IAbility
	{
		/// <summary>
		/// The abilities of the entity.
		/// </summary>
		IEnumerable<TAbility> Abilities { get; set; }

		/// <summary>
		/// Performs an <see cref="IInteraction"> on this entity.
		/// By default this uses the intrinsic ability sort order.
		/// </summary>
		/// <param name="interaction"></param>
		void Interact(IInteraction interaction)
		{
			foreach (var ability in Abilities)
			{
				ability.Interact(interaction);
			}
		}
	}
}
