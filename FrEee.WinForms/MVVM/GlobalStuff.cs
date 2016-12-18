using SimpleMvvmToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.WinForms.Forms {
    /// <summary>
    /// A class to hold all the stuff the is common to all game objects, for example, filesystem paths. Many forms/windows are also of singleton nature,
    /// so a reference could be kept here.
    /// </summary>
    public class GlobalStuff : ViewModelBase<GlobalStuff> {
        /// <summary>
        /// Single instance of the class.
        /// </summary>
        public static GlobalStuff Default { get; }

        /// <summary>
        /// View model for the log form/window (the one that shows up after every move).
        /// </summary>
        public GameLogViewModel GameLogViewModel { get; set; } = new GameLogViewModel();

        public ComponentReportViewModel DefaultComponentReportViewModel { get { return new ComponentReportViewModel(); } }

        public FacilityReportViewModel DefaultFacilityReportViewModel { get { return new FacilityReportViewModel(); } }

        /// <summary>
        /// This is a (hopefully) temporary hack. We need global references to make WinForms/WPF duality work.
        /// </summary>
        public LogForm LogForm { get; set; }
        public GameForm GameForm { get; set; }

        static GlobalStuff() {
            Default = new GlobalStuff();
            Default.Initialize();
        }

        /// <summary>
        /// I don't do any actual initialization in constructor, because this a singleton class that is heavily used during design time (for binding, for example).
        /// Thus, anything but trivial calls always causes lots of very weird problems, when compiler tells there are compilation errors but actually there are none.
        /// This method is called only when the solution is the application is actually running.
        /// </summary>
        public async void Initialize() {
            // No execution logic while in design mode, thank you very much.
            if (this.IsInDesignMode()) return;

            // Reacting to doubleclicks in Log.
            RegisterToReceiveMessages("LogMessageDoubleClicked", (object sender, NotificationEventArgs e) => {
                // Relaying the event to the LogForm to handle. Eventually, navigation logic should find a better way and leave forms only for UI stuff.
                GameLogViewModel.HandleNavigationFromLogForm(Default.GameLogViewModel.SelectedMessage);
            });
        }
    }
}
