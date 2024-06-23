using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Ecs
{
	/// <summary>
	/// A general purpose entity.
	/// Cannot be inherited; use <see cref="IAbility"/>s to give it features.
	/// </summary>
	public sealed class Entity
		: IEntity
	{
		public AbilityTargets AbilityTarget { get; set; }
		public IEnumerable<IEntity> Children { get; set; } = Enumerable.Empty<IEntity>();
		public IEnumerable<IEntity> Parents { get; set; } = Enumerable.Empty<IEntity>();
		public IList<Ability> IntrinsicAbilities { get; set; } = new List<Ability>();
		public IEnumerable<Ability> Abilities { get => IntrinsicAbilities; set { } }

		IEnumerable<Ability> IEntity.IntrinsicAbilities => IntrinsicAbilities;
	}
}
