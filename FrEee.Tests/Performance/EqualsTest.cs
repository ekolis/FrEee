using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Performance
{
	/// <summary>
	/// 
	/// </summary>
	[TestClass]
	public class EqualsTest
	{
		/// <summary>
		/// Whiches the equals is faster.
		/// No, not really... just determines which of the two implementations of Equals is faster.
		/// </summary>
		[TestMethod]
		public void WhichEqualsIsFaster()
		{
			var dict = new SafeDictionary<string, Action>();
			var t1 = new TypeCheckAndCast(1);
			var t2 = new TypeCheckAndCast(2);
			var a1 = new AsOperator(1);
			var a2 = new AsOperator(1);
			dict["Type-check and cast"] = () => { t1.Equals(t1); t1.Equals(t2); t1.Equals(a1); };
			dict["As operator and null-check"] = () => { a1.Equals(a1); a1.Equals(a2); a1.Equals(t1); };
			var data = GottaGoFast.Run(dict);
			Assert.Inconclusive(GottaGoFast.CreateReport(data));
		}

		private abstract class BaseClass
		{
			public BaseClass(int value)
			{
				Value = value;
			}

			public int Value { get; private set; }
		}

		private class TypeCheckAndCast : BaseClass
		{
			public TypeCheckAndCast(int value) : base(value)
			{
			}

			public override bool Equals(object obj)
			{
				if (obj is TypeCheckAndCast)
				{
					var g = (TypeCheckAndCast)obj;
					return Value == g.Value;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return Value.GetHashCode();
			}
		}

		private class AsOperator : BaseClass
		{
			public AsOperator(int value) : base(value)
			{
			}

			public override bool Equals(object obj)
			{
				var g = obj as AsOperator;
				if (g == null)
					return false;
				return Value == g.Value;
			}

			public override int GetHashCode()
			{
				return Value.GetHashCode();
			}
		}
	}
}
