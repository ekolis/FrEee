using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;

namespace FrEee.Serialization
{
	/// <summary>
	/// Attribute which causes a property to be converted to its mod ID when serializing.
	/// The property type must implement <see cref="IModObject"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class ModReferenceAttribute : Attribute
	{
	}
}
