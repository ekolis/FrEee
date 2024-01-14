using System.Collections.Generic;

namespace FrEee.Modding.Interfaces;

public interface IFormulaHost
{
	IDictionary<string, object> Variables { get; }
}