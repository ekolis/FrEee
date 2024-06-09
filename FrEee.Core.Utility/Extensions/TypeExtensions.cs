using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Extensions;

namespace FrEee.Extensions
{
	public static class TypeExtensions
	{
		public static object Instantiate(this Type type, params object[] args)
		{
			if (type.GetConstructors().Where(c => c.GetParameters().Length == (args == null ? 0 : args.Length)).Any())
				return Activator.CreateInstance(type, args) ?? throw new NullReferenceException($"Couldn't create instance of type {type}.");
			else
				return FormatterServices.GetSafeUninitializedObject(type);
		}

		public static T Instantiate<T>(params object[] args)
		{
			return (T)typeof(T).Instantiate(args);
		}

		/// <summary>
		/// Equals method that doesn't throw an exception when objects are null.
		/// Null is not equal to anything else, except other nulls.
		/// </summary>
		/// <param name="o1"></param>
		/// <param name="o2"></param>
		/// <returns></returns>
		public static bool SafeEquals(this object o1, object o2)
		{
			if (o1 == null && o2 == null)
				return true;
			if (o1 == null || o2 == null)
				return false;
			return o1.Equals(o2);
		}

		public static bool SafeSequenceEqual<T>(this IEnumerable<T> e1, IEnumerable<T> e2)
		{
			if (e1.SafeEquals(null) && e2.SafeEquals(null))
				return true;
			if (e1.SafeEquals(null) || e2.SafeEquals(null))
				return false;
			return e1.SequenceEqual(e2);
		}
	}
}
