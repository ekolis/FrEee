using System;

namespace FrEee.Utility
{
	/// <summary>
	/// Prevents an property or class's value from being copied when the containing object is copied.
	/// Instead, the original value will be used, or the known copy if the value has already been copied.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class DoNotCopyAttribute : Attribute
	{
		public DoNotCopyAttribute(bool allowSafeCopy = true)
		{
			AllowSafeCopy = allowSafeCopy;
		}

		/// <summary>
		/// Is "safe" copying (using the original property value) allowed?
		/// If false, even this will not be attempted, and the property will be completely ignored.
		/// Setting to false is useful for properties whose setters throw NotSupportedException.
		/// </summary>
		public bool AllowSafeCopy { get; private set; }
	}
}
