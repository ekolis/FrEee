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
using FrEee.Game.Setup;
using FrEee.Modding;
using FrEee.Utility;

namespace FrEee.WinForms.Forms
{

    public class EmpireSetupViewModel : ViewModelBase<EmpireSetupViewModel>
    {
        public class SelectableTrait : ViewModelBase<SelectableTrait>
        {

            private bool _isSelected;
            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    if (_isSelected == value) return;

                    _isSelected = value;
                    NotifyPropertyChanged(m => (IsSelected));
                    SendMessage("TraitSelectionChanged", new NotificationEventArgs());
                }
            }


            private Trait _trait;
            public Trait Trait
            {
                get { return _trait; }
                set
                {
                    if (_trait == value) return;

                    _trait = value;
                    NotifyPropertyChanged(m => (Trait));
                }
            }

            public SelectableTrait(Trait trait)
            {
                Trait = trait;
            }
        }

        /// <summary>
        /// A copy of the original empire template, in case the user cancels.
        /// </summary>
        public EmpireTemplate OriginalEmpireTemplate { get; set; }

        /// <summary>
        /// A copy of the original race, in case the user cancels.
        /// </summary>
        public Race OriginalRace { get; set; }


        public EmpireSetupViewModel()
        {
            if (this.IsInDesignMode()) return;

            // Return if we're still initializing. Should be removed one day...
            if (Mod.Current == null) return;

            Initialize();
        }

        public void Initialize()
        {
            foreach (var trait in Mod.Current.Traits)
            {
                EmpireTraits.Add(new SelectableTrait(trait));
            }

            AvailableShipsets.Clear();
            foreach (var shipset in Pictures.ListShipsets())
            {
                AvailableShipsets.Add(shipset);
            }

            AvailableCultures.Clear();
            foreach (var shipset in Pictures.ListShipsets())
            {
                AvailableShipsets.Add(shipset);
            }

            AvailableSurfaces.Clear();
            foreach (var surface in Mod.Current.StellarObjectTemplates.OfType<Planet>().Select(p => p.Surface).Distinct())
            {
                AvailableSurfaces.Add(surface);
            }

            AvailableAtmospheres.Clear();
            foreach (var atmosphere in Mod.Current.StellarObjectTemplates.OfType<Planet>().Select(p => p.Atmosphere).Distinct())
            {
                AvailableAtmospheres.Add(atmosphere);
            }

            AvailablePopulationIcons.Clear();
            foreach (var icon in Pictures.ListPopulationIcons())
            {
                AvailablePopulationIcons.Add(icon);
            }

            AvailableLeaderPortraits.Clear();
            foreach (var icon in Pictures.ListLeaderPortraits())
            {
                AvailableLeaderPortraits.Add(icon);
            }

            AvailableInsignia.Clear();
            foreach (var insignia in Pictures.ListInsignia()) AvailableInsignia.Add(insignia);


            CurrentEmpireTemplate.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(CurrentEmpireTemplate.ShipsetName)) ShipsetImage = Mod.Current.Hulls.First().GetPortrait(CurrentEmpireTemplate.ShipsetName);
            };

        }


        private EmpireTemplate _empireTemplate;
        public EmpireTemplate CurrentEmpireTemplate
        {
            get { return _empireTemplate; }
            set
            {
                if (_empireTemplate == value) return;

                _empireTemplate = value;
                NotifyPropertyChanged(m => m.CurrentEmpireTemplate);
            }
        }

        private int _pointsToSpend;
        /// <summary>
        /// The number of empire setup points available for spending.
        /// </summary>
        public int PointsToSpend
        {
            get { return _pointsToSpend; }
            set
            {
                if (_pointsToSpend == value) return;

                _pointsToSpend = value;
                NotifyPropertyChanged(m => m.PointsToSpend);
            }
        }


        private ObservableCollection<SelectableTrait> _empireTraits = new ObservableCollection<SelectableTrait>();
        public ObservableCollection<SelectableTrait> EmpireTraits
        {
            get { return _empireTraits; }
            set
            {
                if (_empireTraits == value) return;

                _empireTraits = value;
                NotifyPropertyChanged(m => m.EmpireTraits);
            }
        }


        private Image _shipsetImage;
        public Image ShipsetImage
        {
            get { return _shipsetImage; }
            set
            {
                if (_shipsetImage == value) return;

                _shipsetImage = value;
                NotifyPropertyChanged(m => m.ShipsetImage);
            }
        }

        private ObservableCollection<string> _availableShipsets = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableShipsets
        {
            get { return _availableShipsets; }
            set
            {
                if (_availableShipsets == value) return;

                _availableShipsets = value;
                NotifyPropertyChanged(m => m.AvailableShipsets);
            }
        }


        private ObservableCollection<string> _availableCultures = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableCultures
        {
            get { return _availableCultures; }
            set
            {
                if (_availableCultures == value) return;

                _availableCultures = value;
                NotifyPropertyChanged(m => m.AvailableCultures);
            }
        }


        private ObservableCollection<string> _availableSurfaces = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableSurfaces
        {
            get { return _availableSurfaces; }
            set
            {
                if (_availableSurfaces == value) return;

                _availableSurfaces = value;
                NotifyPropertyChanged(m => m.AvailableSurfaces);
            }
        }


        private ObservableCollection<string> _availablePopulationIcons = new ObservableCollection<string>();
        public ObservableCollection<string> AvailablePopulationIcons
        {
            get { return _availablePopulationIcons; }
            set
            {
                if (_availablePopulationIcons == value) return;

                _availablePopulationIcons = value;
                NotifyPropertyChanged(m => m.AvailablePopulationIcons);
            }
        }


        private ObservableCollection<string> _availableAtmospheres = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableAtmospheres
        {
            get { return _availableAtmospheres; }
            set
            {
                if (_availableAtmospheres == value) return;

                _availableAtmospheres = value;
                NotifyPropertyChanged(m => m.AvailableAtmospheres);
            }
        }


        private ObservableCollection<string> _availableLeaderPortraits = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableLeaderPortraits
        {
            get { return _availableLeaderPortraits; }
            set
            {
                if (_availableLeaderPortraits == value) return;

                _availableLeaderPortraits = value;
                NotifyPropertyChanged(m => m.AvailableLeaderPortraits);
            }
        }


        private ObservableCollection<string> _availableInsignia = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableInsignia
        {
            get { return _availableInsignia; }
            set
            {
                if (_availableInsignia == value) return;

                _availableInsignia = value;
                NotifyPropertyChanged(m => m.AvailableInsignia);
            }
        }



        // All that follows is some plumbing stuff, not really interesting.
        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
