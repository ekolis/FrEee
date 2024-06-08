using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs
{
	/// <summary>
	/// An ability in the ECS.
	/// This is really an ECS component but components are something else in FrEee.
	/// </summary>
	public interface IAbility
	{
		/// <summary>
		/// Performs an interaction on this ability.
		/// </summary>
		/// <param name="interaction"></param>
		void Interact(IInteraction interaction);
	}
}
