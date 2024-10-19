using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;

namespace FrEee.Gameplay.Commands.Notes;

/// <summary>
/// Builds various types of commands used for managing notes.
public interface INoteCommandFactory
{
	ICommand ClearPlayerNote(IReferrable target);

	ICommand ClearPrivateName(Empire empire, INameable target);

	ICommand SetPlayerInfo(Empire empire, PlayerInfo playerInfo);

	ICommand SetPlayerNote(IReferrable target, string note);

	ICommand SetPrivateName(Empire empire, INameable target, string name);

	ICommand SetPublicName(INameable target, string name);
}
