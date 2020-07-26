using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.AI
{
	/// <summary>
	/// An AI script which has control over a domain.
	/// </summary>
	/// <typeparam name="TDomain">The type of domain.</typeparam>
	/// <typeparam name="TContext">The type of contextual data that the AI needs to be aware of.</typeparam>
	[Serializable]
	public abstract class AI<TDomain, TContext> : IModObject
	{
		public AI(string name, IScript script, SafeDictionary<string, ICollection<string>> ministerNames)
		{
			Name = name;
			Script = script;
			MinisterNames = ministerNames;
		}

		public bool IsDisposed { get; private set; }

		/// <summary>
		/// The names of any ministers that the AI can use, keyed by category.
		/// </summary>
		public SafeDictionary<string, ICollection<string>> MinisterNames { get; private set; }

		public string? ModID { get; set; }

		public string Name { get; private set; }

		/// <summary>
		/// The script to run.
		/// </summary>
		public IScript Script { get; protected set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object>? TemplateParameters { get; set; }

		/// <summary>
		/// Allows the AI to act on its domain using information from its context.
		/// </summary>
		/// <param name="domain">The AI's domain of control.</param>
		/// <param name="context">Contextual data that the AI needs to be aware of.</param>
		/// <param name="enabledMinisters">The names of any ministers that the player has enabled, keyed by category.</param>
		public abstract void Act(TDomain domain, TContext context, SafeDictionary<string, ICollection<string>> enabledMinisters);

		public void Dispose()
		{
			// TODO - remove from mod?
			IsDisposed = true;
		}

		public override string ToString() => Name;
	}
}
