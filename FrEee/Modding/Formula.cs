using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using FrEee.Modding.Enumerations;
using FrEee.Utility;

namespace FrEee.Modding
{
	/// <summary>
	/// A script formula.
	/// </summary>
	/// <typeparam name="T">Return type.</typeparam>
	[Serializable]
	public class Formula<T> : IFormula<T>, IComparable<T>, IComparable<Formula<T>>
		where T : IConvertible, IComparable
	{
		/// <summary>
		/// For serialization.
		/// </summary>
		private Formula()
		{
		}

		public Formula(object context, string text, FormulaType fType)
		{
			Context = context;
			Text = text;
			FormulaType = fType;
		}

		/// <summary>
		/// The formula text.
		/// </summary>
		public string Text { get; set; }

		public FormulaType FormulaType { get; set; }

		public T Value
		{
			get
			{
				if (FormulaType == FormulaType.Literal || FormulaType == FormulaType.Static)
				{
					// literal and static formulas can be cached
					if (!hasCache)
					{
						if (FormulaType == FormulaType.Literal)
						{
							if (typeof(T) == typeof(string))
								cachedValue = (T)(object)Text;
							else if (typeof(T).IsEnum)
								cachedValue = (T)Enum.Parse(typeof(T), Text);
							else
								cachedValue = (T)Convert.ChangeType(Text, typeof(T), CultureInfo.InvariantCulture);
						}
						else
							cachedValue = Evaluate(Context);
						hasCache = true;
					}
					return cachedValue;
				}
				else
				{
					// dynamic formula must be executed each time
					return Evaluate(Context);
				}
			}
		}

		public T Evaluate(IDictionary<string, object> variables)
		{
			return ScriptEngine.EvaluateExpression<T>(Text, variables);
		}

		public T Evaluate(object host)
		{
			if (FormulaType == FormulaType.Literal)
				return Value;
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

		private T cachedValue;
		private bool hasCache = false;

		object IFormula.Value { get { return Value; } }

		/// <summary>
		/// Compiles the formula into a literal formula.
		/// </summary>
		/// <param name="variables"></param>
		/// <returns></returns>
		public Formula<T> Compile()
		{
			return new Formula<T>(Context, Value.ToStringInvariant(), FormulaType.Literal);
		}

		public static implicit operator Formula<T>(T obj)
		{
			return new Formula<T>(null, obj.ToStringInvariant(), FormulaType.Literal);
		}

		public static implicit operator T(Formula<T> f)
		{
			if (f == null)
				return default(T);
			return f.Value;
		}

		public object Context { get; set; }

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

		public static Formula<T> operator +(Formula<T> f1, Formula<T> f2)
		{
			string combined = "({0}) + ({1})";
			if (typeof(T) == typeof(string))
			{
				if (f1.FormulaType == FormulaType.Literal)
					combined = combined.Replace("({0})", "\"{0}\"");
				if (f2.FormulaType == FormulaType.Literal)
					combined = combined.Replace("({1})", "\"{1}\"");
			}
			var result = new Formula<T>(f1.Context ?? f2.Context, string.Format(combined, f1.Text, f2.Text), f1.FormulaType == FormulaType.Literal ? FormulaType.Static : f1.FormulaType);
			if (result.FormulaType == FormulaType.Literal && f2.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator -(Formula<T> f1, Formula<T> f2)
		{
			var result = new Formula<T>(f1.Context ?? f2.Context, string.Format("({0}) - ({1})", f1.Text, f2.Text), f1.FormulaType == FormulaType.Literal ? FormulaType.Static : f1.FormulaType);
			if (f1.FormulaType == FormulaType.Literal && f2.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator *(Formula<T> f1, Formula<T> f2)
		{
			var result = new Formula<T>(f1.Context ?? f2.Context, string.Format("({0}) * ({1})", f1.Text, f2.Text), f1.FormulaType == FormulaType.Literal ? FormulaType.Static : f1.FormulaType);
			if (f1.FormulaType == FormulaType.Literal && f2.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator /(Formula<T> f1, Formula<T> f2)
		{
			var result = new Formula<T>(f1.Context ?? f2.Context, string.Format("({0}) / ({1})", f1.Text, f2.Text), f1.FormulaType == FormulaType.Literal ? FormulaType.Static : f1.FormulaType);
			if (f1.FormulaType == FormulaType.Literal && f2.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator -(Formula<T> f)
		{
			var result = new Formula<T>(f.Context, string.Format("-({0})", f.Text), f.FormulaType == FormulaType.Literal ? FormulaType.Static : f.FormulaType);
			if (f.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator +(Formula<T> f, double scalar)
		{
			Formula<T> result;
			if (typeof(T) == typeof(string) && f.FormulaType == FormulaType.Literal)
				result = new Formula<T>(f.Context, string.Format("(\"{0}\") + str({1})", f.Text, scalar.ToStringInvariant()), FormulaType.Static).Compile();
			else
				result = new Formula<T>(f.Context, string.Format("({0}) + ({1})", f.Text, scalar.ToStringInvariant()), f.FormulaType == FormulaType.Literal ? FormulaType.Static : f.FormulaType);
			if (f.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator -(Formula<T> f, double scalar)
		{
			var result = new Formula<T>(f.Context, string.Format("({0}) - {1}", f.Text, scalar.ToStringInvariant()), f.FormulaType == FormulaType.Literal ? FormulaType.Static : f.FormulaType);
			if (f.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator *(Formula<T> f, double scalar)
		{
			var result = new Formula<T>(f.Context, string.Format("({0}) * {1}", f.Text, scalar.ToStringInvariant()), f.FormulaType == FormulaType.Literal ? FormulaType.Static : f.FormulaType);
			if (f.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<T> operator /(Formula<T> f, double scalar)
		{
			var result = new Formula<T>(f.Context, string.Format("({0}) / {1}", f.Text, scalar.ToStringInvariant()), f.FormulaType == FormulaType.Literal ? FormulaType.Static : f.FormulaType);
			if (f.FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public static Formula<string> operator +(Formula<string> f1, Formula<T> f2)
		{
			if (f1 == null && f2 == null)
				return null;
			if (f1 == null)
			{
				if (f2.FormulaType == FormulaType.Literal)
					return new Formula<string>(f2.Context, f2.Text, FormulaType.Literal);
				else
					return new Formula<string>(f2.Context, string.Format("str({0})", f2.Text), f2.FormulaType);
			}
			if (f2 == null)
				return f1.Copy(); // don't leak a reference to the original formula!
			if (f1.FormulaType == FormulaType.Literal && f2.FormulaType == FormulaType.Literal)
				return f1.Value + f2.Value;
			if (f1.FormulaType == FormulaType.Dynamic || f2.FormulaType == FormulaType.Dynamic)
			{
				if (f1.FormulaType == FormulaType.Literal)
					return new Formula<string>(f1.Context, string.Format("(\"{0}\") + str({1})", f1.Value, f2.Text), FormulaType.Dynamic);
				else
					return new Formula<string>(f1.Context, string.Format("({0}) + str({1})", f1.Text, f2.Text), FormulaType.Dynamic);
			}
			if (f1.FormulaType == FormulaType.Literal)
				return new Formula<string>(f1.Context, string.Format("(\"{0}\") + str({1})", f1.Value, f2.Text), FormulaType.Static);
			else
				return new Formula<string>(f1.Context, string.Format("({0}) + str({1})", f1.Text, f2.Text), FormulaType.Static);
		}

		public static bool operator ==(Formula<T> f1, Formula<T> f2)
		{
			if (f1.IsNull() && f2.IsNull())
				return true;
			if (f1.IsNull() || f2.IsNull())
				return false;
			return f1.Value.SafeEquals(f2.Value);
		}

		public static bool operator !=(Formula<T> f1, Formula<T> f2)
		{
			return !(f1 == f2);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Formula<T>))
				return false;
			var f = (Formula<T>)obj;
			return Text == f.Text && Context == f.Context && FormulaType == f.FormulaType;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Text, Context, FormulaType);
		}

		public Formula<string> ToStringFormula()
		{
			var result = new Formula<string>(Context, "str(" + Text + ")", FormulaType == FormulaType.Literal ? FormulaType.Static : FormulaType);
			if (FormulaType == FormulaType.Literal)
				return result.Compile();
			return result;
		}

		public int CompareTo(object obj)
		{
			if (obj is IFormula)
				return Value.CompareTo(((IFormula)obj).Value);
			return Value.CompareTo(obj);
		}

		public int CompareTo(T other)
		{
			if (other is Formula<T>)
				return Value.CompareTo(((Formula<T>)other).Value);
			return Value.CompareTo(other);
		}

		public int CompareTo(Formula<T> other)
		{
			return Value.CompareTo(other.Value);
		}
	}
}
