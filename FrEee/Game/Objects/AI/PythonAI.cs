using System;
using System.Collections.Generic;

using FrEee.Modding;
using FrEee.Utility;

#nullable enable

namespace FrEee.Game.Objects.AI
{
	[Serializable]
    public class PythonAI<TDomain, TContext> : AI<TDomain, TContext>
    {
        public PythonAI(string name, PythonScript script, SafeDictionary<string, ICollection<string>> ministerNames) : base(name, script, ministerNames)
        {
        }

        public override void Act(TDomain domain, TContext context, SafeDictionary<string, ICollection<string>> EnabledMinisters)
        {
            var variables = new Dictionary<string, object?>();
            variables.Add("domain", domain);
            var readOnlyVariables = new Dictionary<string, object?>();
            readOnlyVariables.Add("context", context);
            readOnlyVariables.Add("enabledMinisters", EnabledMinisters);
            PythonScriptEngine.RunScript<object>(Script as PythonScript, variables, readOnlyVariables);
        }
    }
}
