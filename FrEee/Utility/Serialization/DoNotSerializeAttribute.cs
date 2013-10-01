using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility.Serialization
{
	/// <summary>
	/// Prevents the serializer from serializing a property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class DoNotSerializeAttribute : Attribute
	{

	}
}
