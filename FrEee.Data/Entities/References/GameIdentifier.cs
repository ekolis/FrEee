using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
	public class GameIdentifier<TEntity>
		: Identifier<long, TEntity>
		where TEntity: IGameObject<TEntity>, IEntity<long, TEntity>
	{
	}
}
