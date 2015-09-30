using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Modding
{
	/// <summary>
	/// A simple formula which is just a literal value. Does not require a script.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LiteralFormula<T> : Formula<T>, IEquatable<LiteralFormula<T>>
		where T : IConvertible, IComparable
	{
		/// <summary>
		/// For serialization.
		/// </summary>
		private LiteralFormula()
			: base()
		{
		}

		public LiteralFormula(string text)
			: base(text)
		{
		}

		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		public override bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		public override object Context
		{
			get
			{
				return null;
			}
		}

		protected override T ComputeValue()
		{
			if (typeof(T) == typeof(string))
				return (T)(object)Text;
			else if (typeof(T).IsEnum)
				return Text.ParseEnum<T>();
			else
				return (T)Convert.ChangeType(Text, typeof(T), CultureInfo.InvariantCulture);
		}

		public override T Value
		{
			get
			{
				if (value == null)
					value = new Lazy<T>(ComputeValue); // HACK - why is it not being set when we load?
				return value.Value;
			}
		}

		public override T Evaluate(IDictionary<string, object> variables)
		{
			// no need to call a script
			return Value;
		}

		public override T Evaluate(object host)
		{
			// no need to set variables or call a script
			return Value;
		}

		public override Formula<string> ToStringFormula(CultureInfo c = null)
		{
			return new LiteralFormula<string>(Value.ToString(c ?? CultureInfo.InvariantCulture));
		}

		public static implicit operator LiteralFormula<T>(T obj)
		{
			return new LiteralFormula<T>(obj.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator T(LiteralFormula<T> f)
		{
			return f.Value;
		}

		public static bool operator ==(LiteralFormula<T> f1, Formula<T> f2)
		{
			if (f1.IsNull() && f2.IsNull())
				return true;
			if (f1.IsNull() || f2.IsNull())
				return false;
			return f1.Value.SafeEquals(f2.Value);
		}

		public static bool operator !=(LiteralFormula<T> f1, Formula<T> f2)
		{
			return !(f1 == f2);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is LiteralFormula<T>))
				return false;
			var f = (LiteralFormula<T>)obj;
			return Equals(f);
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Text);
		}

		public bool Equals(LiteralFormula<T> other)
		{
			return Text == other.Text;
		}
	}
}
