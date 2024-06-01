using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities
{
	public interface IGameObject<out TEntity>
		: IEntity<long, TEntity>, IGameObject
		where TEntity: IGameObject<TEntity>
	{
	}

	public interface IGameObject
	{
	}
}
