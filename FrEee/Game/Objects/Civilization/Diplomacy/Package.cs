using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tech = FrEee.Game.Objects.Technology.Technology;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A package of items that can be gifted or traded.
	/// </summary>
	public class Package : IOwnable
	{
		public Package(Empire owner)
		{
			Owner = owner;
			Planets = new ReferenceSet<Planet>();
			Vehicles = new ReferenceSet<IVehicle>();
			Resources = new ResourceQuantity();
			Technology = new ReferenceKeyedDictionary<Tech, int>();
		}

		public ReferenceSet<Planet> Planets { get; private set; }

		public ReferenceSet<IVehicle> Vehicles { get; private set; }

		public ResourceQuantity Resources { get; private set; }

		public ReferenceKeyedDictionary<Tech, int> Technology { get; private set; }

		public ReferenceSet<StarSystem> StarCharts { get; private set; }

		public ReferenceSet<Empire> CommunicationChannels { get; private set; }

		/// <summary>
		/// Is this a valid package?
		/// </summary>
		public bool IsValid
		{
			get
			{
				return !Errors.Any();
			}
		}

		/// <summary>
		/// Errors due to the game settings restricting certain gifts/trades,
		/// or the empire in question not actually owning the objects he is promising.
		/// </summary>
		public IEnumerable<string> Errors
		{
			get
			{
				foreach (var p in Planets.Where(p => p.Owner != Owner))
					yield return "The " + Owner + " does not own " + p + ".";
				foreach (var v in Vehicles.Where(v => v.Owner != Owner))
					yield return "The " + Owner + " does not own " + v + ".";
				if (Resources.Any(kvp => kvp.Value < 0))
					yield return "You cannot transfer a negative quantity of resources.";
				if (Resources.Any(kvp => Owner.StoredResources[kvp.Key] < kvp.Value))
					yield return "The " + Owner + " does not have the specified quantity of resources.";
				foreach (var kvp in Technology.Where(kvp => Owner.ResearchedTechnologies[kvp.Key] < kvp.Value))
					yield return "The " + Owner + " has not researched " + kvp.Key + " to level " + kvp.Value + ".";
				foreach (var sys in StarCharts.Where(sys => !Owner.ExploredStarSystems.Contains(sys)))
					yield return "The " + Owner + " has not explored " + sys + ".";
				foreach (var emp in CommunicationChannels.Where(emp => !Owner.EncounteredEmpires.Contains(emp)))
					yield return "The " + Owner + " has not encountered the " + emp + ".";

				// TODO - game setup restrictions on gifts/trades
			}
		}

		public Empire Owner
		{
			get;
			set;
		}

		public bool IsEmpty
		{
			get
			{
				return !Planets.Any() && !Vehicles.Any() && !Resources.Any(r => r.Value != 0) && !Technology.Any() && !StarCharts.Any() && !CommunicationChannels.Any();
			}
		}

		public override string ToString()
		{
			var items = new List<string>();
			if (Planets.Any())
			{
				if (Planets.Count == 1)
					items.Add(Planets.Single().ToString());
				else
					items.Add(Planets.Count + " planets");
			}
			if (Vehicles.Any())
			{
				if (Vehicles.Count == 1)
					items.Add(Vehicles.Single().ToString());
				else
					items.Add(Vehicles.Count + " vehicles");
			}
			if (Resources.Any(kvp => kvp.Value != 0))
				items.Add(Resources.ToString());
			if (Technology.Any())
			{
				if (Technology.Count == 1)
				{
					var kvp = Technology.Single();
					items.Add(kvp.Key + " to level " + kvp.Value);
				}
				else
					items.Add(Technology.Count + " technologies");
			}
			if (StarCharts.Any())
			{
				if (StarCharts.Count == 1)
					items.Add("the star chart for " + StarCharts.Single().ToString());
				else
					items.Add(StarCharts.Count + " star charts");
			}
			if (CommunicationChannels.Any())
			{
				if (CommunicationChannels.Count == 1)
					items.Add("comm channels to " + CommunicationChannels.Single().ToString());
				else
					items.Add(CommunicationChannels.Count + " comm channels");
			}
			if (!items.Any())
				return "nothing";
			return string.Join(", ", items.ToArray());
		}

		/// <summary>
		/// Transfers this package to a target empire.
		/// </summary>
		/// <param name="target"></param>
		public void Transfer(Empire target)
		{
			var errors = Errors.ToArray();
			if (errors.Any())
				throw new Exception("Attempting to transfer an invalid package (" + this + "): " + errors.First());
			foreach (var p in Planets)
				p.Colony.Owner = target;
			foreach (var v in Vehicles)
				v.Owner = target;
			Owner.StoredResources -= Resources;
			target.StoredResources += Resources;
			foreach (var kvp in Technology)
			{
				if (target.ResearchedTechnologies[kvp.Key] < kvp.Value)
				{
					// research the tech and reset progress to next level
					target.ResearchedTechnologies[kvp.Key] = kvp.Value;
					var progress = target.ResearchProgress.SingleOrDefault(p => p.Item == kvp.Key);
					if (progress != null)
						progress.Value = 0;
				}
			}
			foreach (var sys in StarCharts)
			{
				foreach (var kvp in Owner.Memory)
				{
					// copy memory of system and everything in it
					if (kvp.Value == sys || kvp.Value is ISpaceObject && ((ISpaceObject)kvp.Value).StarSystem == sys)
					{
						var copy = kvp.Value.CopyAndAssignNewID();
						target.Memory.Add(kvp.Key, copy);
					}
				}
			}
			foreach (var emp in CommunicationChannels)
				target.EncounteredEmpires.Add(emp); // not two way, you'll have to gift your own comms channels to the target to let them talk to you!
		}
	}
}
