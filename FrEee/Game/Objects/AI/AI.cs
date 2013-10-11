using FrEee.Game.Interfaces;
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
	public class AI<TDomain, TContext> : INamed
	{
		public AI(string name, Script script, IDictionary<string, ICollection<string>> ministerNames)
		{
			Name = name;
			Script = script;
			MinisterNames = ministerNames;
		}

		/// <summary>
		/// The script to run.
		/// </summary>
		public Script Script { get; set; }

		/// <summary>
		/// The names of any ministers that the AI can use, keyed by category.
		/// </summary>
		public IDictionary<string, ICollection<string>> MinisterNames { get; private set; }

		/// <summary>
		/// Allows the AI to act on its domain using information from its context.
		/// </summary>
		/// <param name="domain">The AI's domain of control.</param>
		/// <param name="context">Contextual data that the AI needs to be aware of.</param>
		/// <param name="enabledMinisters">The names of any ministers that the player has enabled, keyed by category.</param>
		public void Act(TDomain domain, TContext context, IDictionary<string, ICollection<string>> enabledMinisters)
		{
			var dict = new Dictionary<string, object>();
			dict.Add("domain", domain);
			dict.Add("context", context);
			dict.Add("enabledMinisters", enabledMinisters);
			ScriptEngine.RunScript(Script, dict);
		}

		public string Name
		{
			get;
			private set;
		}
	}
}
