using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
    public interface IIdentifier<out TID, out TEntity>
        where TEntity : IEntity<TID, TEntity>
    {
        /// <summary>
        /// The unique identifier value.
        /// </summary>
        TID ID { get; }
    }
}
