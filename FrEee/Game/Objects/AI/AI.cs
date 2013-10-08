using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.AI
{
	/// <summary>
	/// An AI script which has control over a domain.
	/// </summary>
	/// <typeparam name="TDomain">The type of domain.</typeparam>
	/// <typeparam name="TContext">The type of contextual data that the AI needs to be aware of.</typeparam>
	public class AI<TDomain, TContext>
	{
		/// <summary>
		/// The script to run.
		/// </summary>
		public string Script { get; set; }

		/// <summary>
		/// Allows the AI to act on its domain using information from its context.
		/// </summary>
		/// <param name="domain">The AI's domain of control.</param>
		/// <param name="context">Contextual data that the AI needs to be aware of.</param>
		public void Act(TDomain domain, TContext context)
		{
			var dict = new Dictionary<string, object>();
			dict.Add("domain", domain);
			dict.Add("context", context);
			ScriptEngine.RunScript(Script, dict);
		}
	}
}
