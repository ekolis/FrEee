using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities;

/// <summary>
/// A general purpose game object.
/// </summary>
public class GameObject
{
	/// <summary>
	/// The ID of the game object.
	/// </summary>
	public long GameObjectID { get; set; }

	/// <summary>
	/// General purpose serialized data to be sorted out in future iterations of the data model.
	/// </summary>
	public string SerializedData { get; set; } = "";
}
