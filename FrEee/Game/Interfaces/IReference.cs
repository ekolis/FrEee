using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IReference<out T> where T : IReferrable
	{
		int ID { get; }
		T Value { get; }
		bool IsGlobal { get; }
	}
}
