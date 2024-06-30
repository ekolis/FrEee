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
}
