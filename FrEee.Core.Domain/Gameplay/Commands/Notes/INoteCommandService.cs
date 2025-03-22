using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Plugins;

namespace FrEee.Gameplay.Commands.Notes;

/// <summary>
/// Creates various types of commands used for managing notes.
/// </summary>
public interface INoteCommandService
	: IPlugin<INoteCommandService>
{
	ICommand ClearPlayerNote(IReferrable target);

	ICommand ClearPrivateName(Empire empire, INameable target);

	ICommand SetPlayerInfo(Empire empire, PlayerInfo playerInfo);

	ICommand SetPlayerNote(IReferrable target, string note);

	ICommand SetPrivateName(Empire empire, INameable target, string name);

	ICommand SetPublicName(INameable target, string name);
}
