using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Marks a type as safe to pass from the client to the server.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
	sealed class ClientSafeAttribute : Attribute
	{
		
	}
}
