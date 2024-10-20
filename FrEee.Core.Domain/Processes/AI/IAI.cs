using System.Collections.Generic;
using FrEee.Modding;
using FrEee.Modding.Scripts;
using FrEee.Utility;

namespace FrEee.Processes.AI;

/// <summary>
/// An AI script which has control over a domain.
/// </summary>
/// <typeparam name="TDomain">The type of domain.</typeparam>
/// <typeparam name="TContext">The type of contextual data that the AI needs to be aware of.</typeparam>
public interface IAI<TDomain, TContext>
	: IModObject
{
	/// <summary>
	/// The names of any ministers that the AI can use, keyed by category.
	/// </summary>
	SafeDictionary<string, ICollection<string>> MinisterNames { get; }

	/// <summary>
	/// The script to run.
	/// </summary>
	IScript Script { get; }

	/// <summary>
	/// Allows the AI to act on its domain using information from its context.
	/// </summary>
	/// <param name="domain">The AI's domain of control.</param>
	/// <param name="context">Contextual data that the AI needs to be aware of.</param>
	/// <param name="enabledMinisters">The names of any ministers that the player has enabled, keyed by category.</param>
	void Act(TDomain domain, TContext context, SafeDictionary<string, ICollection<string>> enabledMinisters);
}