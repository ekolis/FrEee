using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
    /// <summary>
    /// A value which can be used to uniquely identify an entity.
    /// </summary>
    /// <typeparam name="TID">The type of identifier value.</typeparam>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface IIdentifier<out TID, out TEntity>
        where TEntity : IEntity<TID, TEntity>
    {
        /// <summary>
        /// The unique identifier value.
        /// </summary>
        TID ID { get; }
    }
}
