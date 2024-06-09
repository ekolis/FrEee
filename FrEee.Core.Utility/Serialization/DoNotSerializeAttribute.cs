using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;
using FrEee.Serialization;

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
