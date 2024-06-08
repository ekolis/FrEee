using System.Collections;
using System.Collections.Generic;

namespace FrEee.Ecs
{
	/// <summary>
	/// Repository for storing entities.
	/// </summary>
	/// <typeparam name="T">The type of entity.</typeparam>
	public class Repository<T>
		where T : IEntity
	{
		/// <summary>
		/// Entities in the repository.
		/// </summary>
		public ISet<T> Entities { get; } = new HashSet<T>();
	}
}
