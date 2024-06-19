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
	}

	public static class EntityExtensions
	{
		/// <summary>
		/// Performs an <see cref="IInteraction"> on this entity via its abilities.
		/// </summary>
		/// <param name="interaction"></param>
		public static void Interact<TAbility>(this IEntity<TAbility> entity, IInteraction interaction)
			where TAbility : IAbility
		{
			foreach (var ability in entity.Abilities)
			{
				ability.Interact(interaction);
			}
		}
	}
}
