using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
    public class Identifier<TID, TEntity>
        : IIdentifier<TID, TEntity>
        where TEntity : IEntity<TID, TEntity>
    {
        public TID ID { get; }
    }
}
