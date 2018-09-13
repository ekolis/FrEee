﻿using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FrEee.Modding
{
	/// <summary>
	/// A simple formula which is just a literal value. Does not require a script.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LiteralFormula<T> : Formula<T>, IEquatable<LiteralFormula<T>>
		where T : IConvertible, IComparable
	{
		public LiteralFormula(string text)
					: base(text)
		{
		}

		/// <summary>
		/// For serialization.
		/// </summary>
		private LiteralFormula()
			: base()
		{
		}

		[DoNotSerialize(false)]
		public override object Context
		{
			get
			{
				return null;
			}
			set
			{
				// do nothing
			}
		}

		public override bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		public override bool IsLiteral
		{
			get
			{
				return true;
			}
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

		public static implicit operator LiteralFormula<T>(T obj)
		{
			return new LiteralFormula<T>(obj.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator T(LiteralFormula<T> f)
		{
			return f.Value;
		}

		public static bool operator !=(LiteralFormula<T> f1, Formula<T> f2)
		{
			return !(f1 == f2);
		}

		public static bool operator ==(LiteralFormula<T> f1, Formula<T> f2)
		{
			if (f1.IsNull() && f2.IsNull())
				return true;
			if (f1.IsNull() || f2.IsNull())
				return false;
			return f1.Value.SafeEquals(f2.Value);
		}

		public override bool Equals(object obj)
		{
			var x = obj as LiteralFormula<T>;
			if (ReferenceEquals(x, null))
				return false;
			return Equals(x);
		}

		public bool Equals(LiteralFormula<T> other)
		{
			return Text == other.Text;
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

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Text);
		}

		public override Formula<string> ToStringFormula(CultureInfo c = null)
		{
			return new LiteralFormula<string>(Value.ToString(c ?? CultureInfo.InvariantCulture));
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
	}
}