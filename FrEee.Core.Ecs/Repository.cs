using System.Collections;
using System.Collections.Generic;

namespace FrEee.Ecs
{
	/// <summary>
	/// Repository for storing entities.
	/// </summary>
	/// <typeparam name="T">The type of entity.</typeparam>
	public class Repository<TEntity, TAbility>
		: IEnumerable<TEntity>
		where TEntity : IEntity<TAbility>
		where TAbility : IAbility
	{
		/// <summary>
		/// Entities in the repository.
		/// </summary>
		public ISet<TEntity> Entities { get; } = new HashSet<TEntity>();

		public IEnumerator<TEntity> GetEnumerator()
			=> Entities.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		public void Add(TEntity entity)
			=> Entities.Add(entity);

		public bool Remove(TEntity entity)
			=> Entities.Remove(entity);
	}
}
