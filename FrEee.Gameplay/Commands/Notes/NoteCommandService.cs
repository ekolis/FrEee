﻿using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.Gameplay.Commands.Notes;
public class NoteCommandService
	: INoteCommandService
{
	public ICommand ClearPlayerNote(IReferrable target)
	{
		return new ClearPlayerNoteCommand(target);
	}

	public ICommand ClearPrivateName(Empire empire, INameable target)
	{
		return new ClearPrivateNameCommand(empire, target);
	}

	public ICommand SetPlayerInfo(Empire empire, PlayerInfo playerInfo)
	{
		return new SetPlayerInfoCommand(empire, playerInfo);
	}

	public ICommand SetPlayerNote(IReferrable target, string note)
	{
		return new SetPlayerNoteCommand(target, note);
	}

	public ICommand SetPrivateName(Empire empire, INameable target, string name)
	{
		return new SetPrivateNameCommand(empire, target, name);
	}

	public ICommand SetPublicName(INameable target, string name)
	{
		return new SetPublicNameCommand(target, name);
	}
}
