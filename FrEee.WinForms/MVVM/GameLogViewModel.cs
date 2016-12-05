using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.WinForms.Controls;
using FrEee.Game.Objects.Combat.Simple;
using SimpleMvvmToolkit;
using System.Collections.ObjectModel;

namespace FrEee.WinForms.Forms
{
    /// <summary>
    /// View model for the Log (the one that appears after each turn).
    /// </summary>
    public class GameLogViewModel : ViewModelBase<GameLogViewModel>
    {
        /// <summary>
        /// Holds all the messages that we'd like to show.
        /// </summary>
        public ObservableCollection<LogMessage> Messages { get; } = new ObservableCollection<LogMessage>();

        /// <summary>
        /// Currently selected message. Used for navigation.
        /// </summary>
        public LogMessage SelectedMessage { get; set; }

        /// <summary>
        /// Navigates user whenever he double-clicks the log. Navigation target depends on the message that was double clicked.
        /// </summary>
        /// <param name="message"></param>
        public void HandleNavigationFromLogForm(LogMessage message)
        {
            if (message is IPictorialLogMessage<IPictorial>)
            {
                var shortGameForm = GlobalStuff.Default.GameForm;


                var context = ((IPictorialLogMessage<IPictorial>)message).Context;
                if (context is ISpaceObject)
                {
                    // go to space object
                    shortGameForm.SelectSpaceObject((ISpaceObject)context);
                }
                else if (context is IUnit)
                {
                    // go to whatever contains the unit
                    var unit = (IUnit)context;
                    var container = unit.FindContainer();
                    if (container != null)
                    {
                        if (container is Sector)
                            shortGameForm.SelectSpaceObject((ISpaceObject)unit);
                        else
                            shortGameForm.SelectSpaceObject((ISpaceObject)container);
                    }
                }
                else if (context is Facility)
                {
                    // go to the planet
                    var facility = (Facility)context;
                    var container = facility.Container;
                    shortGameForm.SelectSpaceObject(container);
                }
                else if (context is Technology)
                {
                    // go to research screen
                    shortGameForm.ShowResearchForm();
                }
                else if (context is IHull<IVehicle>)
                {
                    // go to design screen and create a new design using this hull 
                    var hull = (IHull<IVehicle>)context;
                    shortGameForm.ShowVehicleDesignForm(new VehicleDesignForm(hull));
                }
                else if (context is ComponentTemplate || context is Mount)
                {
                    // go to design screen
                    shortGameForm.ShowVehicleDesignForm(new VehicleDesignForm());
                }
                else if (context is Battle)
                {
                    // show battle results
                    var form = new BattleResultsForm((Battle)context);
                    GlobalStuff.Default.LogForm.ShowChildForm(form);
                }
                else if (context is IMessage)
                {
                    // show diplomacy screen
                    var form = new DiplomacyForm((IMessage)context);
                    shortGameForm.ShowChildForm(form);
                }

                // This is some sort of a special case: closing Log form almost always, except...
                if ((context is Battle) == false) GlobalStuff.Default.LogForm.Close();

                // TODO - more types of goto-messages
            }
        }
    }
}
