using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.Identification;

namespace FrEee.Data.Entities.Identification
{
	public record Identifier<TIDValue>(TIDValue Value)
	   : IIdentifier<TIDValue>
		where TIDValue : IEquatable<TIDValue>
	{
	}
}
