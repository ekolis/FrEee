using System;
using System.Drawing;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using System.Collections.Generic;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System.IO;
using FrEee.Modding;
using FrEee.Game.Objects.LogMessages;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire attempting to rule the galaxy.
	/// </summary>
	[Serializable]
	public class Empire : INamed, IOrderable<Empire, IEmpireOrder>
	{
		/// <summary>
		/// The current empire being controlled by the player.
		/// </summary>
		public static Empire Current
		{
			get
			{
				if (Galaxy.Current == null)
					return null;
				return Galaxy.Current.CurrentEmpire;
			}
		}

		public Empire()
		{
			StoredResources = new Resources();

			// TODO - make starting resources moddable
			StoredResources.Add("Minerals", 50000);
			StoredResources.Add("Organics", 50000);
			StoredResources.Add("Radioactives", 50000);

			Commands = new List<ICommand>();
			KnownDesigns = new List<IDesign>();
		}

		/// <summary>
		/// The name of the empire.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The title of the emperor.
		/// </summary>
		public string EmperorTitle { get; set; }

		/// <summary>
		/// The name of the emperor.
		/// </summary>
		public string EmperorName { get; set; }

		/// <summary>
		/// The folder (under Pictures/Races) where the empire's shipset is located.
		/// </summary>
		public string ShipsetPath { get; set; }

		/// <summary>
		/// The empire's flag.
		/// </summary>
		public Image Flag
		{
			get
			{
				if (Mod.Current.RootPath != null)
				{
					return
						Pictures.GetCachedImage(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", ShipsetPath, "Flag")) ??
						Pictures.GetCachedImage(Path.Combine("Pictures", "Races", ShipsetPath, "Flag")) ??
						Pictures.GetGenericImage(typeof(Empire));
				}
				else
				{
					return
						Pictures.GetCachedImage(Path.Combine("Pictures", "Races", ShipsetPath, "Flag")) ??
						Pictures.GetGenericImage(typeof(Empire));
				}
			}
		}

		/// <summary>
		/// The color used to represent this empire's star systems on the galaxy map.
		/// </summary>
		public Color Color { get; set; }

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// The resources stored by the empire.
		/// </summary>
		public Resources StoredResources { get; set; }

		/// <summary>
		/// Commands issued by the player this turn.
		/// </summary>
		public IList<ICommand> Commands { get; private set; }

		/// <summary>
		/// The empire's resource income.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <returns></returns>
		public Resources Income
		{
			get
			{
				// TODO - take into account maintenance costs
				return ColonizedPlanets.Select(p => p.Income).Aggregate((r1, r2) => r1 + r2);
			}
		}

		/// <summary>
		/// Finds star systems explored by the empire.
		/// </summary>
		public IEnumerable<StarSystem> ExploredStarSystems
		{
			get { return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).Where(sys => sys.ExploredByEmpires.Contains(this)); }
		}

		/// <summary>
		/// Planets colonized by the empire.
		/// </summary>
		public IEnumerable<Planet> ColonizedPlanets
		{
			get
			{
				return Galaxy.Current.StarSystemLocations.Select(ssl => ssl.Item).SelectMany(ss => ss.FindSpaceObjects<Planet>(p => p.Owner == this).Flatten());
			}
		}

		/// <summary>
		/// Designs known by this empire.
		/// </summary>
		public ICollection<IDesign> KnownDesigns { get; private set; }

		public IList<IEmpireOrder> Orders
		{
			get;
			private set;
		}

		/// <summary>
		/// Not the empire index (1 for player 1, etc.), just the index in the list of orderable objects.
		/// </summary>
		public int ID
		{
			get;
			set;
		}

		/// <summary>
		/// The empire owns itself, of course.
		/// </summary>
		public Empire Owner
		{
			get { return this; }
		}

		public void ExecuteOrders()
		{
			foreach (var order in Orders)
				order.Execute(this);
		}

		/// <summary>
		/// Empire history log.
		/// </summary>
		public IList<LogMessage> Log { get; set; }
	}
}
