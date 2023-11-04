using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;

namespace FrEee.Serialization.Attributes
{
	/// <summary>
	/// Attribute which causes a property to be converted to its in-game ID when serializing.
	/// The property type must implement <see cref="IReferrable"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class GameReferenceAttribute : Attribute
	{
	}
}
