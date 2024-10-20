using FrEee.Modding;
using FrEee.Modding.Scripts;
using FrEee.Utility;
using System;
using System.Collections.Generic;

namespace FrEee.Processes.AI;

/// <summary>
/// Stock implementation of <see cref="IAI{TDomain, TContext}"/>.
/// </summary>
/// <typeparam name="TDomain"></typeparam>
/// <typeparam name="TContext"></typeparam>
[Serializable]
public abstract class AI<TDomain, TContext> 
	: IAI<TDomain, TContext>
{
	public AI(string name, IScript script, SafeDictionary<string, ICollection<string>> ministerNames)
	{
		Name = name;
		Script = script;
		MinisterNames = ministerNames;
	}

	public bool IsDisposed { get; private set; }

	public SafeDictionary<string, ICollection<string>> MinisterNames { get; private set; }


	public string ModID { get; set; }

	public string Name
	{
		get;
		private set;
	}

	public IScript Script { get; protected set; }

	/// <summary>
	/// Parameters from the mod meta templates.
	/// </summary>
	public IDictionary<string, object> TemplateParameters { get; set; }

	public abstract void Act(TDomain domain, TContext context, SafeDictionary<string, ICollection<string>> enabledMinisters);


	public void Dispose()
	{
		// TODO - remove from mod?
		IsDisposed = true;
	}

	public override string ToString()
	{
		return Name;
	}



}