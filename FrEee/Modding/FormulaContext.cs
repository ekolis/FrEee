using System.Collections.Generic;

namespace FrEee.Modding;

/// <summary>
/// Contextual variables for a formula.
/// </summary>
public class FormulaContext
{
	public FormulaContext(FormulaContext parent = null)
	{
		Parent = parent;
	}

	/// <summary>
	/// Ancestors from oldest to newest, and then this context.
	/// </summary>
	public IEnumerable<FormulaContext> AllContexts
	{
		get
		{
			var list = new List<FormulaContext>();
			var cur = this;
			while (cur != null)
				list.Add(cur);
			list.Reverse();
			return list;
		}
	}

	/// <summary>
	/// Variables in this context, including un-overridden variables in ancestor contexts.
	/// </summary>
	public IDictionary<string, object> AllVariables
	{
		get
		{
			// overwrite older variables with newer ones
			var variables = new Dictionary<string, object>();
			foreach (var ctx in AllContexts)
			{
				foreach (var kvp in ctx.AllVariables)
					ctx.AllVariables.Add(kvp.Key, kvp.Value);
			}
			return variables;
		}
	}

	public FormulaContext Parent { get; set; }
	public IDictionary<string, object> Variables = new Dictionary<string, object>();
}