using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.AI;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace FrEee.Game.Objects.Civilization
{
    /// <summary>
    /// A race of beings.
    /// </summary>
    [Serializable]
    public class Race : INamed, IAbilityObject, IPictorial, IReferrable, INotifyPropertyChanged
    {
        public Race()
        {
            TraitNames = new List<string>();
            Aptitudes = new SafeDictionary<string, int>();
        }

        private string _name;
        /// <summary>
		/// The name of this race. Also used for picture names.
		/// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;

                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string _populationIconName;
        /// <summary>
        /// The population icon name for this race.
        /// </summary>
        public string PopulationIconName
        {
            get { return _populationIconName; }
            set
            {
                if (_populationIconName == value) return;

                _populationIconName = value;
                NotifyPropertyChanged(nameof(PopulationIconName));
            }
        }


        private string _nativeAtmosphere;
        /// <summary>
        /// The atmosphere which this race breathes.
        /// </summary>
        public string NativeAtmosphere
        {
            get { return _nativeAtmosphere; }
            set
            {
                if (_nativeAtmosphere == value) return;

                _nativeAtmosphere = value;
                NotifyPropertyChanged(nameof(NativeAtmosphere));
            }
        }


        private string _nativeSurface;

        /// <summary>
        /// The native planet surface type of this race.
        /// </summary>
        public string NativeSurface
        {
            get { return _nativeSurface; }
            set
            {
                if (_nativeSurface == value) return;

                _nativeSurface = value;
                NotifyPropertyChanged(nameof(NativeSurface));
            }
        }

        private HappinessModel _happinessModel;
        /// <summary>
        /// The race's happiness model.
        /// </summary>
        [DoNotSerialize]
        public HappinessModel HappinessModel
        {
            get { return _happinessModel; }
            set
            {
                if (_happinessModel == value) return;

                _happinessModel = value;
                NotifyPropertyChanged(nameof(HappinessModel));
            }
        }

        private ModReference<HappinessModel> happinessModel { get; set; }

        /// <summary>
        /// The names of the race's traits.
        /// </summary>
        public IList<string> TraitNames { get; private set; }

        /// <summary>
        /// The traits of the race.
        /// </summary>
        public IEnumerable<Trait> Traits { get { return Mod.Current.Traits.Join(TraitNames, t => t.Name, n => n, (t, n) => t); } }

        public IEnumerable<Ability> Abilities
        {
            get { return Traits.SelectMany(t => t.Abilities); }
        }

        public IEnumerable<Ability> UnstackedAbilities
        {
            get { return Abilities; }
        }

        /// <summary>
        /// The population icon.
        /// </summary>
        public Image Icon
        {
            get { return Pictures.GetIcon(this); }
        }

        /// <summary>
        /// The population portrait.
        /// </summary>
        public Image Portrait
        {
            get { return Pictures.GetPortrait(this); }
        }

        public IEnumerable<string> IconPaths
        {
            get
            {
                foreach (var x in GetImagePaths(PopulationIconName, "Pop_Portrait"))
                    yield return x;

                // fall back on leader portrait if icon not found
                foreach (var x in GetImagePaths(PopulationIconName, "Race_Portrait"))
                    yield return x;

            }
        }

        public IEnumerable<string> PortraitPaths
        {
            get
            {
                return IconPaths;
            }
        }

        private IEnumerable<string> GetImagePaths(string imagename, string imagetype)
        {
            if (Mod.Current?.RootPath != null)
            {
                yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", imagename, imagetype);
                yield return Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", imagename, Name + "_" + imagetype);
            }
            yield return Path.Combine("Pictures", "Races", imagename, imagetype);
            yield return Path.Combine("Pictures", "Races", imagename, Name + "_" + imagetype);
        }

        /// <summary>
        /// Aptitudes of this race.
        /// </summary>
        // TODO - convert to NamedDictionary
        public IDictionary<string, int> Aptitudes { get; private set; }

        /// <summary>
        /// Races have no owner.
        /// </summary>
        public Empire Owner
        {
            get { return null; }
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;
            Galaxy.Current.UnassignID(this);
        }

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<string> Warnings
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    yield return "You must specify a name for your race.";
                if (string.IsNullOrWhiteSpace(PopulationIconName))
                    yield return "You must specify a population icon for your race.";
                if (string.IsNullOrWhiteSpace(NativeAtmosphere))
                    yield return "You must specify a native atmosphere for your race.";
                if (string.IsNullOrWhiteSpace(NativeSurface))
                    yield return "You must specify a native planet surface for your race.";
                if (!Mod.Current.StellarObjectTemplates.OfType<Planet>().Any(p => p.Atmosphere == NativeAtmosphere && p.Surface == NativeSurface && !p.Size.IsConstructed))
                    yield return NativeSurface + " / " + NativeAtmosphere + " is not a valid surface / atmosphere combination for the current mod.";
                if (HappinessModel == null)
                    yield return "You must specify a happiness model for your race.";
                foreach (var kvp in Aptitudes)
                {
                    var apt = Aptitude.All.FindByName(kvp.Key);
                    if (apt == null)
                        yield return "\"" + kvp.Key + "\" is not a valid racial aptitude.";
                    else if (kvp.Value > apt.MaxPercent)
                        yield return "Aptitude value for " + kvp.Key + " is too high.";
                    else if (kvp.Value < apt.MinPercent)
                        yield return "Aptitude value for " + kvp.Key + " is too low.";
                }
            }
        }

        public static Race Load(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open);
            var race = Serializer.Deserialize<Race>(fs);
            fs.Close(); fs.Dispose();
            return race;
        }

        public void Save(string filename)
        {
            var fs = new FileStream(filename, FileMode.Create);
            Serializer.Serialize(this, fs);
            fs.Close(); fs.Dispose();
        }

        /// <summary>
        /// Races are known to everyone, though they really should be hidden until first contact...
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public Visibility CheckVisibility(Empire emp)
        {
            // TODO - hide races until first contact
            return Visibility.Scanned;
        }

        public long ID { get; set; }

        public AbilityTargets AbilityTarget
        {
            get { return AbilityTargets.Race; }
        }

        public IEnumerable<Ability> IntrinsicAbilities
        {
            get { yield break; }
        }

        public IEnumerable<IAbilityObject> Children
        {
            get { return Traits; }
        }

        public IEnumerable<IAbilityObject> Parents
        {
            get
            {
                // TODO - return empire?
                yield break;
            }
        }

        public bool IsDisposed { get; set; }

        /// <summary>
        /// Resource income percentages based on racial aptitudes.
        /// </summary>
        public ResourceQuantity IncomePercentages
        {
            get
            {
                var result = new ResourceQuantity();
                foreach (var r in Resource.All)
                {
                    var factor = 1d;
                    if (r.Aptitude != null)
                        factor *= Aptitudes[r.Aptitude.Name] / 100d;
                    result += (int)(100 * factor) * r;
                }
                return result;
            }
        }

        // Implementing pipes for the INotifyPropertyChanged.
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
