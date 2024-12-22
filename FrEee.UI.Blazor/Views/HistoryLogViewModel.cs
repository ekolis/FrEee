using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding.Templates;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.GameState;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Processes.Combat;
using FrEee.Vehicles.Types;
using FrEee.Vehicles;
using FrEee.Extensions;
using FrEee.Utility;

namespace FrEee.UI.Blazor.Views;

public class HistoryLogViewModel
	: ViewModelBase
{
	public IEnumerable<LogMessage> Messages { get; set; } = [];

	public ImageDisplayViewModel CreateImageDisplayViewModel(Image image)
	{
		return new ImageDisplayViewModel
		{
			Image = image
		};
	}

	public void Navigate(LogMessage message)
	{
		if (message is IPictorialLogMessage<IPictorial> pm)
		{
			var context = pm.Context;
			if (context is ISpaceObject sobj)
			{
				// go to space object
				DIRoot.Gui.Focus(sobj);
			}
			else if (context is IUnit unit)
			{
				// go to whatever contains the unit
				var container = unit.FindContainer();
				if (container != null)
				{
					if (container is Sector)
					{
						DIRoot.Gui.Focus((ISpaceObject)unit);
					}
					else
					{
						DIRoot.Gui.Focus((ISpaceObject)container);
					}
				}
			}
			else if (context is Facility facility)
			{
				// go to the planet
				var container = facility.Container;
				DIRoot.Gui.Focus(container);
			}
			else if (context is Technology tech)
			{
				// go to research screen
				DIRoot.Gui.Focus(tech);
			}
			else if (context is IHull hull)
			{
				// go to design screen and create a new design using this hull
				DIRoot.Gui.Focus(hull);
			}
			else if (context is ComponentTemplate || context is Mount)
			{
				// go to design screen
				DIRoot.Gui.Show(Screen.VehicleDesign);
			}
			else if (context is IBattle battle)
			{
				// show battle results
				DIRoot.Gui.Focus(battle);
			}
			else if (context is IMessage msg)
			{
				// show diplomacy screen
				DIRoot.Gui.Focus(msg);
			}
			else if (context is StarSystem sys)
			{
				// navigate game form to that system
				DIRoot.Gui.Focus(sys);
			}

			// TODO - more types of goto-messages
		}
	}

	public bool CanNavigate(LogMessage message)
	{
		if (message is PictorialLogMessage<IPictorial> pm)
		{
			return pm.Context is ISpaceObject
				|| pm.Context is IUnit
				|| pm.Context is Facility
				|| pm.Context is Technology
				|| pm.Context is IHull
				|| pm.Context is ComponentTemplate
				|| pm.Context is Mount
				|| pm.Context is IBattle
				|| pm.Context is IMessage
				|| pm.Context is StarSystem;
		}
		return false;
	}
}
