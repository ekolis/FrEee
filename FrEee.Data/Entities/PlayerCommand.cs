using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.References;

namespace FrEee.Data.Entities;

/// <summary>
/// A command issued by a player to a game object.
/// </summary>
public class PlayerCommand(GameIdentifier<PlayerCommand> id)
	: GameObject<PlayerCommand>(id)
{
}
