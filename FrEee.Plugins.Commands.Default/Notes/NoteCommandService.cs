using System;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;

namespace FrEee.Plugins.Commands.Default.Notes;
public class NoteCommandService
	: INoteCommandService
{
	public string Package { get; } = IPlugin.DefaultPackage;
	public string Name { get; } = "NoteCommandService";
	public Version Version { get; } = IPlugin.DefaultVersion;

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
