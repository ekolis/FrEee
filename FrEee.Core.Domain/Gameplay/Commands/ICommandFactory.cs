using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Gameplay.Commands;

/// <summary>
/// Builds various types of <see cref="ICommand"> used by the game.
/// </summary>
public interface ICommandFactory
{
    // TODO: split these out into separate factories for each category and add parameters
    ICommand ClearPlayerNote();

    ICommand ClearPrivateName();

    ICommand CreateDesign();

    ICommand CreateFleet();

    ICommand CreateWaypoint();

    ICommand DeleteMessage();

    ICommand DeleteWaypoint();

    ICommand DisbandFleet();

    ICommand HotkeyWaypoint();

    ICommand JoinFleet();

    ICommand LeaveFleet();

    ICommand MinisterToggle();

    ICommand Research();

    ICommand SendMessage();

    ICommand SetObsoleteFlag();

    ICommand SetPlayerInfo();

    ICommand SetPlayerNote();

    ICommand SetPrivateName();

    ICommand SetPublicName();
}