using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FrEee.Modding
{
	/// <summary>
	/// A formula used in the game's data files.
	/// </summary>
	/// <typeparam name="T">Return type of the formula.</typeparam>
	[Serializable]
	public abstract class Formula<T> : IFormula<T>, IComparable<T>, IComparable<Formula<T>>
		where T : IComparable, IConvertible
	{
		public Formula(string text)
					: this()
		{
			Text = text;
		}

		/// <summary>
		/// For serialization.
		/// </summary>
		protected Formula()
		{
			value = new Lazy<T>(ComputeValue);
		}

		public abstract object Context { get; set; }

		/// <summary>
		/// The external scripts required to execute this formula.
		/// </summary>
		public PythonScript[] ExternalScripts { get; set; }

		public abstract bool IsDynamic { get; }
		public abstract bool IsLiteral { get; }

		/// <summary>
		/// The formula text.
		/// </summary>
		public string Text { get; set; }

		public abstract T Value { get; }
		object IFormula.Value { get { return Value; } }
		protected Lazy<T> value;

		public static implicit operator Formula<T>(T t)
		{
			return new LiteralFormula<T>(t.ToStringInvariant());
		}

		public static implicit operator T(Formula<T> f)
		{
			if (f == null)
				return default(T);
			return f.Value;
		}

		public static Formula<T> operator -(Formula<T> f1, Formula<T> f2)
		{
			var text = $"({f1.Text}) - ({f2.Text})";
			if (f1.IsLiteral && f2.IsLiteral)
				return new ComputedFormula<T>(text, null, false);
			else
				return new ComputedFormula<T>(text, f1.Context ?? f2.Context, f1.IsDynamic || f2.IsDynamic);
		}

		public static Formula<T> operator -(Formula<T> f)
		{
			var text = $"-({f.Text})";
			if (f.IsLiteral)
				return new ComputedFormula<T>(text, null, false);
			else
				return new ComputedFormula<T>(text, f.Context, f.IsDynamic);
		}

		public static Formula<T> operator -(Formula<T> f, double scalar)
		{
			var text = $"{f.Value} - {scalar}";
			return new ComputedFormula<T>(text, f.Context, f.IsDynamic);
		}

		public static Formula<T> operator *(Formula<T> f1, Formula<T> f2)
		{
			var text = $"({f1.Text}) * ({f2.Text})";
			if (f1.IsLiteral && f2.IsLiteral)
				return new ComputedFormula<T>(text, null, false);
			else
				return new ComputedFormula<T>(text, f1.Context ?? f2.Context, f1.IsDynamic || f2.IsDynamic);
		}

		public static Formula<T> operator *(Formula<T> f, double scalar)
		{
			var text = $"{f.Value} * {scalar}";
			return new ComputedFormula<T>(text, f.Context, f.IsDynamic);
		}

		public static Formula<T> operator /(Formula<T> f1, Formula<T> f2)
		{
			var text = $"({f1.Text}) / ({f2.Text})";
			if (f1.IsLiteral && f2.IsLiteral)
				return new ComputedFormula<T>(text, null, false);
			else
				return new ComputedFormula<T>(text, f1.Context ?? f2.Context, f1.IsDynamic || f2.IsDynamic);
		}

		public static Formula<T> operator /(Formula<T> f, double scalar)
		{
			var text = $"{f.Value} / {scalar}";
			return new ComputedFormula<T>(text, f.Context, f.IsDynamic);
		}

		public static Formula<T> operator +(Formula<T> f1, Formula<T> f2)
		{
			string combined = "({0}) + ({1})";
			if (typeof(T) == typeof(string))
			{
				if (f1.IsLiteral)
					combined = combined.Replace("({0})", "\"{0}\"");
				if (f2.IsLiteral)
					combined = combined.Replace("({1})", "\"{1}\"");
			}
			if (f1.IsLiteral && f2.IsLiteral)
				return new ComputedFormula<T>(string.Format(combined, f1.Text, f2.Text), null, false);
			else
				return new ComputedFormula<T>(string.Format(combined, f1.Text, f2.Text), f1.Context ?? f2.Context, f1.IsDynamic || f2.IsDynamic);
		}

		public static Formula<T> operator +(Formula<T> f, double scalar)
		{
			if (typeof(T) == typeof(string))
			{
				var text = (string)(object)f.Value + scalar.ToStringInvariant();
				if (f.IsLiteral)
					return new LiteralFormula<T>(text);
				else
					return new ComputedFormula<T>(text, f.Context, f.IsDynamic);
			}
			else
			{
				var text = $"{f.Value} + {scalar}";
				return new ComputedFormula<T>(text, f.Context, f.IsDynamic);
			}
		}

		public static Formula<string> operator +(Formula<string> f1, Formula<T> f2)
		{
			if (f1 == null && f2 == null)
				return null;
			if (f1 == null)
			{
				if (f2.IsLiteral)
					return new LiteralFormula<string>(f2.Text);
				else
					return new ComputedFormula<string>(string.Format("str({0})", f2.Text), f2.Context, f2.IsDynamic);
			}
			if (f2 == null)
				return f1.Copy(); // don't leak a reference to the original formula!
			if (f1.IsLiteral && f2.IsLiteral)
				return f1.Value + f2.Value;
			if (f1.IsDynamic || f2.IsDynamic)
			{
				if (f1.IsLiteral)
					return new ComputedFormula<string>(string.Format("(\"{0}\") + str({1})", f1.Value, f2.Text), f1.Context, true);
				else
					return new ComputedFormula<string>(string.Format("({0}) + str({1})", f1.Text, f2.Text), f1.Context, true);
			}
			if (f1.IsLiteral)
				return new ComputedFormula<string>(string.Format("(\"{0}\") + str({1})", f1.Value, f2.Text), f1.Context, false);
			else
				return new ComputedFormula<string>(string.Format("({0}) + str({1})", f1.Text, f2.Text), f1.Context, false);
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

		object IFormula.Evaluate(object host, IDictionary<string, object> variables)
		{
			return Evaluate(host, variables);
		}

		public abstract T Evaluate(object host, IDictionary<string, object> variables = null);

		public override string ToString()
		{
			try
			{
				if (Value is string)
					return '"' + Value.ToString() + '"';
				return Value.ToString();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public abstract Formula<string> ToStringFormula(CultureInfo c = null);

		protected abstract T ComputeValue();
	}
}
