using FrEee.Wpf.Views;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace FrEee.Wpf.ViewModels
{
    [Export]
    public class MainMenuViewModel : FrEeeViewModelBase
    {
        private DelegateCommand _modsCommand;
        public DelegateCommand ModsCommand
        {
            get
            {
                return _modsCommand ?? (_modsCommand = new DelegateCommand(() =>
                {
                    // Temporary, need something more elegant :)
                    ServiceLocator.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainMenuRegion, ViewNames.SelectModView);
                }));
            }
        }

        private DelegateCommand _newCommand;
        public DelegateCommand NewCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    // Temporary, need something more elegant :)
                    ServiceLocator.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainMenuRegion, ViewNames.GameSetupView);
                }));
            }
        }

        private DelegateCommand _quickstartCommand;
        public DelegateCommand QuickstartCommand
        {
            get
            {
                return _quickstartCommand ?? (_quickstartCommand = new DelegateCommand(() =>
                {
                    // Temporary, need something more elegant :)
                    ServiceLocator.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainRegion, ViewNames.GameView);
                }));
            }
        }

        private DelegateCommand _resumeCommand;
        public DelegateCommand ResumeCommand
        {
            get { return _resumeCommand ?? (_resumeCommand = new DelegateCommand(() => MessageBox.Show("Not Implemented"))); }
        }

        private DelegateCommand _loadCommand;
        public DelegateCommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new DelegateCommand(() => MessageBox.Show("Not Implemented"))); }
        }

        private DelegateCommand _scenarioCommand;
        public DelegateCommand ScenarioCommand
        {
            get { return _scenarioCommand ?? (_scenarioCommand = new DelegateCommand(() => MessageBox.Show("Not Implemented"))); }
        }

        private DelegateCommand _creditsCommand;
        public DelegateCommand CreditsCommand
        {
            get
            {
                return _creditsCommand ?? (_creditsCommand = new DelegateCommand(() =>
                {
                    // Temporary, need something more elegant :)
                    ServiceLocator.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainMenuRegion, ViewNames.CreditsView);
                }));
            }
        }

        private DelegateCommand _quitCommand;
        public DelegateCommand QuitCommand
        {
            get { return _quitCommand ?? (_quitCommand = new DelegateCommand(() => Application.Current.Shutdown())); }
        }
    }
}
