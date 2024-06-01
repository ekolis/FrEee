using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
	public class ModIdentifier<TEntity>
		: Identifier<string, TEntity>
		where TEntity: IModObject<TEntity>
	{
	}
}
