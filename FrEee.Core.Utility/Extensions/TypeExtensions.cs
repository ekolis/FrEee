using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using FrEee.Utility;

namespace FrEee.Extensions;

public static class TypeExtensions
{
	private static SafeDictionary<MemberInfo, IEnumerable<Attribute>> attributeCache = new SafeDictionary<MemberInfo, IEnumerable<Attribute>>();

	private static SafeDictionary<Type, IEnumerable<Type>> interfaceCache = new SafeDictionary<Type, IEnumerable<Type>>();

	private static SafeDictionary<Type, IEnumerable<MemberInfo>> memberCache = new SafeDictionary<Type, IEnumerable<MemberInfo>>();

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

	/// <summary>
	/// Checks for attributes in a class or its interfaces.
	/// </summary>
	/// <param name="mi"></param>
	/// <param name="attributeType"></param>
	/// <returns></returns>
	public static bool HasAttribute<T>(this MemberInfo mi)
	{
		return mi.HasAttribute(typeof(T));
	}

	/// <summary>
	/// Checks for attributes in a class or its interfaces.
	/// </summary>
	/// <param name="mi"></param>
	/// <param name="attributeType"></param>
	/// <returns></returns>
	public static bool HasAttribute(this MemberInfo mi, Type attributeType, bool checkInterfaces = true)
	{
		if (attributeCache[mi] == null)
			attributeCache[mi] = Attribute.GetCustomAttributes(mi).ToArray();
		if (attributeCache[mi].Where(a => attributeType.IsAssignableFrom(a.GetType())).Any())
			return true;
		var dt = mi is Type ? mi as Type : mi.DeclaringType;
		if (checkInterfaces)
		{
			if (interfaceCache[dt] == null)
				interfaceCache[dt] = dt.GetInterfaces();
			foreach (var i in interfaceCache[dt])
			{
				if (memberCache[i] == null)
					memberCache[i] = i.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToArray(); // TODO - refactor into method
				if (memberCache[i].Any(m => m.Name == mi.Name && m.MemberType == mi.MemberType && m.HasAttribute(attributeType, false))) // no need to check interfaces of interfaces, they're already listed by GetInterfaces
					return true;
			}
		}
		return false;
	}

	public static bool HasProperty(this ExpandoObject obj, string propertyName)
	{
		return obj.GetType().GetProperty(propertyName) != null;
	}

	/// <summary>
	/// Checks for attributes in a class or its interfaces.
	/// </summary>
	/// <param name="mi"></param>
	/// <param name="attributeType"></param>
	/// <returns></returns>
	public static IEnumerable<T> GetAttributes<T>(this MemberInfo mi) where T : Attribute
	{
		if (attributeCache[mi] == null)
			attributeCache[mi] = Attribute.GetCustomAttributes(mi).ToArray();
		var atts = attributeCache[mi].OfType<T>();
		foreach (var att in atts)
			yield return att;
		if (interfaceCache[mi.DeclaringType] == null)
			interfaceCache[mi.DeclaringType] = mi.DeclaringType.GetInterfaces();
		foreach (var i in interfaceCache[mi.DeclaringType])
		{
			if (memberCache[i] == null)
				memberCache[i] = i.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToArray(); // TODO - refactor into method
			var mi2 = memberCache[i].SingleOrDefault(x => x.MemberType == mi.MemberType && x.Name == mi.Name);
			if (mi2 != null)
			{
				foreach (var att2 in mi2.GetAttributes<T>())
					yield return att2;
			}
		}
	}

	/// <summary>
	/// Gets all names for a property, class, etc. including custom names and the actual item name.
	/// </summary>
	/// <param name="m"></param>
	/// <returns></returns>
	public static IEnumerable<string> GetNames(this MemberInfo m)
	{
		return m.GetAttributes<NameAttribute>().Select(a => a.Name).UnionSingle(m.Name);
	}

	/// <summary>
	/// Gets the canonical name for a property, class, etc.
	/// This is taken from the [CanonicalName] attribute if present, otherwise the name of the item itself.
	/// </summary>
	/// <param name="m"></param>
	/// <returns></returns>
	public static string GetCanonicalName(this MemberInfo m)
	{
		// TODO - use most derived class's attribute?
		var name = m.GetAttributes<CanonicalNameAttribute>().Select(a => a.Name).SingleOrDefault();
		if (name == null)
			return m.Name;
		return name;
	}

	/// <summary>
	/// Gets a property value from an object using reflection.
	/// If the property does not exist, returns null.
	/// </summary>
	/// <param name="o"></param>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	public static object GetPropertyValue(this object o, string propertyName)
	{
		if (o == null)
			return null;
		var prop = o.GetType().GetProperty(propertyName);
		if (prop == null)
			return null;
		return prop.GetValue(o, new object[0]);
	}
}
