using System;
using FrEee.Utility;

namespace FrEee.Serialization
{
	/// <summary>
	/// Prevents a property from being serialized, or copied when the containing object is copied.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class DoNotSerializeAttribute : DoNotCopyAttribute
	{
		public DoNotSerializeAttribute(bool allowSafeCopy = true)
			: base(allowSafeCopy)
		{
		}
	}
}
