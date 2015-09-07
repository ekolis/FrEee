using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Modding
{
	/// <summary>
	/// A formula which is computed via a script.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ComputedFormula<T> : Formula<T>
		where T : IConvertible, IComparable
	{
		/// <summary>
		/// For serialization.
		/// </summary>
		protected ComputedFormula()
			: base()
		{
		}

		public ComputedFormula(string text, object context, bool isDynamic)
			: base(text)
		{
			this.context = context;
			this.isDynamic = isDynamic;
		}

		public override bool IsLiteral
		{
			get
			{
				return false;
			}
		}

		private bool isDynamic;

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

		[DoNotCopy]
		private object context { get; set; }

		public override object Context { get { return context; } }

		protected override T ComputeValue()
		{
			return Evaluate(Context);
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

		/// <summary>
		/// Compiles the formula into a literal formula.
		/// </summary>
		/// <returns></returns>
		public Formula<T> Compile()
		{
			return new LiteralFormula<T>(Value.ToStringInvariant());
		}

		public override Formula<string> ToStringFormula(CultureInfo c = null)
		{
			// HACK - could lead to desyncs for static formulas with their stringified counterparts if the values change
			return new ComputedFormula<string>("str(" + Text + ")", Context, IsDynamic);
		}

		public int CompareTo(object obj)
		{
			if (obj is IFormula)
				return Value.CompareTo(((IFormula)obj).Value);
			return Value.CompareTo(obj);
		}

		public int CompareTo(T other)
		{
			return Value.CompareTo(other);
		}

		public int CompareTo(Formula<T> other)
		{
			return Value.CompareTo(other.Value);
		}

		public static implicit operator T(ComputedFormula<T> f)
		{
			return f.Value;
		}

		public static bool operator ==(ComputedFormula<T> f1, ComputedFormula<T> f2)
		{
			if (f1.IsNull() && f2.IsNull())
				return true;
			if (f1.IsNull() || f2.IsNull())
				return false;
			return f1.Value.SafeEquals(f2.Value);
		}

		public static bool operator !=(ComputedFormula<T> f1, ComputedFormula<T> f2)
		{
			return !(f1 == f2);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ComputedFormula<T>))
				return false;
			var f = (ComputedFormula<T>)obj;
			return IsDynamic == f.IsDynamic && Text == f.Text && Context == f.Context;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(IsDynamic, Text, Context);
		}
	}
}