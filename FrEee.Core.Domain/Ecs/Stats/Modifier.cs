using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Ecs.Stats
{
	public record Modifier
	(
		IEntity Source,
		Operation Operation,
		decimal Value
	);
}
