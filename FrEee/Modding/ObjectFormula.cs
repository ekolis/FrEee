using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrEee.Modding;

/// <summary>
/// A formula which returns a generic object rather than a comparable scalar.
/// </summary>
/// <typeparam name="T">The type of object to return.</typeparam>
public class ObjectFormula<T> : IFormula<T>
{
	public ObjectFormula(string text, object context, bool isDynamic)
	{
		Text = text;
		this.context = context;
		this.isDynamic = isDynamic;
	}

	/// <summary>
	/// For serialization.
	/// </summary>
	protected ObjectFormula()
		: base()
	{
	}

	public object Context { get { return context; } }

	/// <summary>
	/// Object formulas are always dynamic.
	/// </summary>
	public bool IsDynamic
	{
		get
		{
			return true;
		}
	}

	/// <summary>
	/// Is this ObjectFormula{T} literal?
	/// </summary>
	public bool IsLiteral
	{
		get
		{
			return false;
		}
	}

	/// <summary>
	/// The formula text.
	/// </summary>
	public string Text { get; set; }

	object IFormula.Value { get { return Value; } }

	public T Value
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

	protected Lazy<T> value;

	[DoNotCopy]
	private object context { get; set; }

	private bool isDynamic;

	public static implicit operator T(ObjectFormula<T> f)
	{
		return f.Value;
	}

	public override bool Equals(object? obj)
	{
		var x = obj as ObjectFormula<T>;
		if (x == null)
			return false;
		return Equals(x);
	}

	public bool Equals(ObjectFormula<T> other)
	{
		return IsDynamic == other.IsDynamic && Text == other.Text && Context == other.Context;
	}

	public T Evaluate(object host, IDictionary<string, object> variables = null)
	{
		var vars = new Dictionary<string, object>(variables ?? new Dictionary<string, object>());
		vars.Add("self", Context);
		vars.Add("host", host);
		if (host is IFormulaHost)
		{
			foreach (var kvp in ((IFormulaHost)host).Variables)
				vars.Add(kvp.Key, kvp.Value);
		}
		if (host is IModObject mo)
		{
			if (mo.TemplateParameters != null)
			{
				foreach (var kvp in mo.TemplateParameters)
					vars.Add(kvp.Key, kvp.Value);
			}
		}
		string fulltext;
		if (ExternalScripts == null)
			fulltext = Text;
		else
			fulltext = string.Join("\n", ExternalScripts.Select(q => q.Text)) + "\n" + Text;
		return PythonScriptEngine.EvaluateExpression<T>(fulltext, vars);
	}

	object IFormula.Evaluate(object host, IDictionary<string, object> variables)
	{
		return Evaluate(host, variables);
	}

	public override int GetHashCode()
	{
		return HashCodeMasher.Mash(IsDynamic, Text, Context);
	}

	public Formula<string> ToStringFormula(CultureInfo c = null)
	{
		// HACK - could lead to desyncs for static formulas with their stringified counterparts if the values change
		return new ComputedFormula<string>("str(" + Text + ")", Context, IsDynamic);
	}

	protected T ComputeValue()
	{
		return Evaluate(Context, null);
	}

	public int CompareTo(object? obj)
	{
		throw new NotSupportedException();
	}

	public int CompareTo(T ?other)
	{
		throw new NotSupportedException();
	}

	public int CompareTo(ObjectFormula<T>? other)
	{
		throw new NotSupportedException();
	}

	/// <summary>
	/// The external scripts required to execute this formula.
	/// </summary>
	public PythonScript[] ExternalScripts { get; set; }
}