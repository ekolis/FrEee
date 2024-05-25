using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Entities;

/// <summary>
/// A command issued by a player to a game object.
/// </summary>
public class PlayerCommand
{
	/// <summary>
	/// The ID of the command.
	/// </summary>
	public long PlayerCommandID { get; set; }

	/// <summary>
	/// General purpose serialized data to be sorted out in future iterations of the data model.
	/// </summary>
	public string SerializedData { get; set; } = "";
}
