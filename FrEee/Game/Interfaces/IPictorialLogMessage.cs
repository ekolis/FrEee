using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IPictorialLogMessage<out T> where T : IPictorial
	{
		T Context { get; }
	}
}
