using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IReference<out T> : IPromotable
	{
		long ID { get; }
		T Value { get; }
	}
}
