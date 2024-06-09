using System;
using FrEee.Utility;
using FrEee.Utility;

namespace FrEee.Utility;

/// <summary>
/// Provides a custom name to use that is the singular definitive name, not an alias.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
public class CanonicalNameAttribute : NameAttribute
{
	public CanonicalNameAttribute(string name)
		: base(name)
	{
	}
}

/// <summary>
/// Provides a custom name to use on a class, field, or property.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
public class NameAttribute : Attribute
{
	public NameAttribute(string name)
	{
		Name = name;
	}

	/// <summary>
	/// The name to assign.
	/// </summary>
	public string Name { get; private set; }
}
