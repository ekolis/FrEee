using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Messages;

namespace FrEee.Gameplay.Commands.Ministers;

/// <summary>
/// Builds commands used to manage AI ministers for an empire.
/// </summary>
public interface IMinisterCommandFactory
{
	IToggleMinistersCommand ToggleMinisters();
}