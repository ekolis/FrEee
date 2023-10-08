using System;

namespace FrEee.Utility;

/// <summary>
/// Prevents IDs from being assigned to objects when calling AssignIDs.
/// </summary>
public class DoNotAssignIDAttribute : Attribute
{
	public DoNotAssignIDAttribute(bool recurse = true)
	{
		Recurse = recurse;
	}

	/// <summary>
	/// Should the "don't assign ID" rule be recursive?
	/// </summary>
	public bool Recurse { get; private set; }
}
