using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Abilities;
using FrEee.Data.Entities;

namespace FrEee.Data.Repositories
{
    /// <summary>
    /// A repository of entities.
    /// </summary>
    /// <typeparam name="TIDValue"></typeparam>
    /// <typeparam name="TEntity"></typeparam> // TODO: do we really need a distinction between IDs and entities? all entities are is holders for IDs...
    public interface IRepository<TIDValue, TEntity>
        where TIDValue : IEquatable<TIDValue>
        where TEntity : IEntity
    {
        /// <summary>
        /// The entities in the repository.
        /// </summary>
        public IEnumerable<TEntity> Entities { get; }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The ID of the added entity.</returns>
        public TIDValue Add(TEntity entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to remove.</param>
        /// <returns>true if it was there to remove, otherwise false.</returns>
        public bool Remove(TIDValue id);

        /// <summary>
        /// Gets the ID of an entity in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TIDValue? GetID(TEntity entity);

        /// <summary>
        /// Gets the entity with a particular ID in the repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity? GetEntity(TIDValue id);

        /// <summary>
        /// Gets any entities that are children of a particular entity in the repository.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetChildren(TEntity parent);

        /// <summary>
        /// Gets any entities that are parents of a particular entity in the repository.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetParents(TEntity child);

        /// <summary>
        /// Links a parent entity to a child entity in the repository.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void LinkParentToChild(TEntity parent, TEntity child);

        // TODO: get descendants, ancestors, implemented by default here, allow circular hierarchies and cut them off

        /// <summary>
        /// Gets the intrinsic abilities of a particular entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<IAbility> GetIntrinsicAbilities(TEntity entity);

		/// <summary>
		/// Gets the intrinsic and inherited abilities of a particular entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public IEnumerable<IAbility> GetAllAbilities(TEntity entity);

		/// <summary>
		/// Adds an ability to an entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="ability"></param>
		public void AddAbility(IEntity entity, IAbility ability);

		/// <summary>
		/// Removes an ability from an entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="ability"></param>
        /// <returns></returns>
		public bool RemoveAbility(IEntity entity, IAbility ability);
	}
}
