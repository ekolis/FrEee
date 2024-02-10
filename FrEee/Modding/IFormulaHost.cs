using System.Collections.Generic;

namespace FrEee.Modding;

public interface IFormulaHost
{
    IDictionary<string, object> Variables { get; }
}