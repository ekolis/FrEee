using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Plugins;

namespace FrEee.Gameplay.Commands.Ministers;

/// <summary>
/// Creates commands used to manage AI ministers for an empire.
/// </summary>
public interface IMinisterCommandService
	: IPlugin<IMinisterCommandService>
{
	IToggleMinistersCommand ToggleMinisters();
}