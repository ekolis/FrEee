using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Serialization;

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public sealed class SerializationPriorityAttribute : Attribute
{
	public SerializationPriorityAttribute(int priority)
	{
		Priority = priority;
	}

	public int Priority { get; private set; }
}

/// <summary>
/// Forces serialization of a property even when it has a default value, e.g. null or zero.
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public sealed class ForceSerializationWhenDefaultValueAttribute : Attribute
{
	public ForceSerializationWhenDefaultValueAttribute()
	{
	}
}