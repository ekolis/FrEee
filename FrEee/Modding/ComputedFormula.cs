﻿using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FrEee.Modding
{
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

		public override object Context { get { return context; } }

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
					return Evaluate(Context);
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
			if (f1.IsNull() && f2.IsNull())
				return true;
			if (f1.IsNull() || f2.IsNull())
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

		public override bool Equals(object obj)
		{
			var x = obj as ComputedFormula<T>;
			if (x == null)
				return false;
			return Equals(x);
		}

		public bool Equals(ComputedFormula<T> other)
		{
			return IsDynamic == other.IsDynamic && Text == other.Text && Context == other.Context;
		}

		public override T Evaluate(IDictionary<string, object> variables)
		{
			return ScriptEngine.EvaluateExpression<T>(Text, variables);
		}

		public override T Evaluate(object host)
		{
			var variables = new Dictionary<string, object>();
			variables.Add("self", Context);
			variables.Add("host", host);
			if (host is IFormulaHost)
			{
				foreach (var kvp in ((IFormulaHost)host).Variables)
					variables.Add(kvp.Key, kvp.Value);
			}
			return Evaluate(variables);
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
			return Evaluate(Context);
		}
	}
}