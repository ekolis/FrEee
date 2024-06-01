using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities.References
{
	public record Identifier<TIDValue>(TIDValue Value)
	   : IIdentifier<TIDValue>
		where TIDValue : IEquatable<TIDValue>
	{
	}
}
