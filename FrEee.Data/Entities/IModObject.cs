using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities
{
	public interface IModObject<out TEntity>
		: IEntity<string, TEntity>, IModObject
		where TEntity: IModObject<TEntity>
	{
	}

	public interface IModObject
	{
	}
}
