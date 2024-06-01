using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.References;

namespace FrEee.Data.Entities
{
	public interface IEntity<out TID, out TEntity>
		where TEntity: IEntity<TID, TEntity>
	{
		IIdentifier<TID, TEntity> ID { get; }
	}
}
