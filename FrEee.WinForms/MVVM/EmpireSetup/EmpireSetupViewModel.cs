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

namespace FrEee.WinForms.Forms {

    public class EmpireSetupViewModel : ViewModelBase<EmpireSetupViewModel> {

        public EmpireSetupViewModel() {
            if (this.IsInDesignMode()) return;

            foreach (var trait in Mod.Current.Traits) {
                EmpireTraits.Add(trait);
            }
        }


        private EmpireTemplate _empireTemplate;
        public EmpireTemplate EmpireTemplate
        {
            get { return _empireTemplate; }
            set
            {
                if (_empireTemplate == value) return;

                _empireTemplate = value;
                NotifyPropertyChanged(m => m.EmpireTemplate);
            }
        }

        private int _pointsToSpend;
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


        private ObservableCollection<Trait> _empireTraits = new ObservableCollection<Trait>();
        public ObservableCollection<Trait> EmpireTraits
        {
            get { return _empireTraits; }
            set
            {
                if (_empireTraits == value) return;

                _empireTraits = value;
                NotifyPropertyChanged(m => m.EmpireTraits);
            }
        }

        // All that follows is some plumbing stuff, not really interesting.
        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void NotifyPropertyChanged(string propertyName) {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
