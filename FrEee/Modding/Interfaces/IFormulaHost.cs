using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Interfaces
{
	public interface IFormulaHost
	{
		IDictionary<string, object> Variables { get; }
	}
}
