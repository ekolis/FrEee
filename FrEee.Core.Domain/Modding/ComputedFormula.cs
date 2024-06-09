using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.Serialization;

namespace FrEee.Modding;

/// <summary>
/// A formula which is computed via a script.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ComputedFormula<T> : Formula<T>, IEquatable<ComputedFormula<T>>
	where T : IConvertible, IComparable
{
	public ComputedFormula(string text, object context, bool isDynamic)
				: base(text)
	{
		this.context = context;
		this.isDynamic = isDynamic;
	}

	/// <summary>
	/// For serialization.
	/// </summary>
	protected ComputedFormula()
		: base()
	{
	}

	[DoNotSerialize(false)]
	public override object Context { get { return context; } set { context = value; } }

	/// <summary>
	/// Is this a dynamic formula? Dynamic formulas cannot be cached.
	/// </summary>
	public override bool IsDynamic
	{
		get
		{
			return isDynamic;
		}
	}

	public override bool IsLiteral
	{
		get
		{
			return false;
		}
	}

	public override T Value
	{
		get
		{
			if (IsDynamic)
				return Evaluate(Context, null);
			else
			{
				if (value == null)
					value = new Lazy<T>(ComputeValue); // HACK - why is it not being set when we load?
				return value.Value;
			}
		}
	}

	[DoNotCopy]
	private object context { get; set; }

	private bool isDynamic;

	public static implicit operator T(ComputedFormula<T> f)
	{
		return f.Value;
	}

	public static bool operator !=(ComputedFormula<T> f1, ComputedFormula<T> f2)
	{
		return !(f1 == f2);
	}

	public static bool operator ==(ComputedFormula<T> f1, ComputedFormula<T> f2)
	{
		if (f1 is null && f2 is null)
			return true;
		if (f1 is null || f2 is null)
			return false;
		return f1.Value.SafeEquals(f2.Value);
	}

	/// <summary>
	/// Compiles the formula into a literal formula.
	/// </summary>
	/// <returns></returns>
	public Formula<T> Compile()
	{
		return new LiteralFormula<T>(Value.ToStringInvariant());
	}

	public override bool Equals(object? obj)
	{
		var x = obj as ComputedFormula<T>;
		if (x is null)
			return false;
		return Equals(x);
	}

	public bool Equals(ComputedFormula<T>? other)
	{
		return other is not null && IsDynamic == other.IsDynamic && Text == other.Text && Context == other.Context;
	}

	public override T Evaluate(object host, IDictionary<string, object> variables = null)
	{
		var parms = new Dictionary<string, object>(variables ?? new Dictionary<string, object>());
		parms.Add("self", Context);
		parms.Add("host", host);
		if (host is IFormulaHost fh)
		{
			foreach (var kvp in fh.Variables)
				parms.Add(kvp.Key, kvp.Value);
		}
		if (host is IModObject mo)
		{
			if (mo.TemplateParameters != null)
			{
				foreach (var kvp in mo.TemplateParameters)
					parms.Add(kvp.Key, kvp.Value);
			}
		}
		if (host is IDictionary<string, object> d)
		{
			foreach (var kvp in d)
				parms[kvp.Key] = kvp.Value;
		}
		if (variables != null)
		{
			foreach (var kvp in variables)
				parms[kvp.Key] = kvp.Value;
		}
		string fulltext;
		if (ExternalScripts == null)
			fulltext = Text;
		else
			fulltext = string.Join("\n", ExternalScripts.Select(q => q.Text)) + "\n" + Text;
		return PythonScriptEngine.EvaluateExpression<T>(fulltext, parms);
	}

	public override int GetHashCode()
	{
		return HashCodeMasher.Mash(IsDynamic, Text, Context);
	}

	public override Formula<string> ToStringFormula(CultureInfo c = null)
	{
		// HACK - could lead to desyncs for static formulas with their stringified counterparts if the values change
		return new ComputedFormula<string>("str(" + Text + ")", Context, IsDynamic);
	}

	protected override T ComputeValue()
	{
		return Evaluate(Context, null);
	}
}
