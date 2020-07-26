using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Tech = FrEee.Game.Objects.Technology.Technology;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A package of items that can be gifted or traded.
	/// </summary>
	public class Package : IOwnable, IPromotable
	{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		// initialized via property
		public Package(Empire owner, Empire recipient)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			Owner = owner;
			Recipient = recipient;
			TreatyClauses = new HashSet<Clause>();
			Planets = new GalaxyReferenceSet<Planet>();
			Vehicles = new GalaxyReferenceSet<IVehicle>();
			Resources = new ResourceQuantity();
			Technology = new ModReferenceKeyedDictionary<Tech, int>();
			StarCharts = new GalaxyReferenceSet<StarSystem>();
			CommunicationChannels = new GalaxyReferenceSet<Empire>();
		}

		public GalaxyReferenceSet<Empire> CommunicationChannels { get; private set; }

		/// <summary>
		/// Errors due to the game settings restricting certain gifts/trades,
		/// or the empire in question not actually owning the objects he is promising.
		/// </summary>
		public IEnumerable<string> Errors
		{
			get
			{
				if (TreatyClauses.OfType<AllianceClause>().Count() > 1)
					yield return "A treaty cannot contain more than one alliance clause per side.";
				if (TreatyClauses.OfType<CooperativeResearchClause>().Count() > 1)
					yield return "A treaty cannot contain more than one cooperative research clause per side.";
				if (TreatyClauses.OfType<FreeTradeClause>().GroupBy(c => c.Resource).Any(g => g.Count() > 1))
					yield return "A treaty cannot contain more than free trade clause per resource per side.";
				if (TreatyClauses.OfType<ShareAbilityClause>().GroupBy(c => c.AbilityRule).Any(g => g.Count() > 1))
					yield return "A treaty cannot contain more than ability-sharing clause per ability per side.";
				if (TreatyClauses.OfType<ShareCombatLogsClause>().Count() > 1)
					yield return "A treaty cannot contain more than one combat-log-sharing clause per side.";
				if (TreatyClauses.OfType<ShareDesignsClause>().Count() > 1)
					yield return "A treaty cannot contain more than one design-sharing clause per side.";
				if (TreatyClauses.OfType<ShareVisionClause>().Count() > 1)
					yield return "A treaty cannot contain more than one vision-sharing clause per side.";
				if (TreatyClauses.OfType<TributeClause>().GroupBy(c => c.Resource).Any(g => g.Count() > 1))
					yield return "A treaty cannot contain more than tribute clause per resource per side.";
				foreach (var p in Planets.Where(p => p.Owner != Owner))
					yield return "The " + Owner + " does not own " + p + ".";
				foreach (var v in Vehicles.Where(v => v.Owner != Owner))
					yield return "The " + Owner + " does not own " + v + ".";
				foreach (var u in Vehicles.OfType<IUnit>().Where(u => u.Container is ISpaceObject))
					yield return u + " cannot be traded because it is in the cargo of " + u.Container + ".";
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

				// TODO - game setup restrictions on gifts/trades/treaties
			}
		}

		public bool IsEmpty => !TreatyClauses.Any() && !Planets.Any() && !Vehicles.Any() && !Resources.Any(r => r.Value != 0) && !Technology.Any() && !StarCharts.Any() && !CommunicationChannels.Any();

		/// <summary>
		/// Is this a valid package?
		/// </summary>
		public bool IsValid => !Errors.Any();

		[DoNotSerialize]
		public Empire Owner { get => owner; set => owner = value; }

		public GalaxyReferenceSet<Planet> Planets { get; private set; }

		[DoNotSerialize]
		public Empire Recipient { get => recipient; set => recipient = value; }

		public ResourceQuantity Resources { get; private set; }
		public GalaxyReferenceSet<StarSystem> StarCharts { get; private set; }
		public ModReferenceKeyedDictionary<Tech, int> Technology { get; private set; }
		public ISet<Clause> TreatyClauses { get; private set; }
		public GalaxyReferenceSet<IVehicle> Vehicles { get; private set; }

		private GalaxyReference<Empire> owner { get; set; }
		private GalaxyReference<Empire> recipient { get; set; }

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				foreach (var clause in TreatyClauses)
					clause.ReplaceClientIDs(idmap, done);
			}
		}

		public override string ToString()
		{
			var items = new List<string?>();
			if (TreatyClauses.Any())
			{
				if (TreatyClauses.Count == 1)
					items.Add(TreatyClauses.Single().ToString());
				else
					items.Add(TreatyClauses.Count + " treaty clauses");
			}
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
			foreach (var c in TreatyClauses)
				Galaxy.Current.GetReferrable(c).IsInEffect = true;
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

			// TODO - get max treaty clause happiness change and only apply that?
			if (TreatyClauses.OfType<AllianceClause>().Any())
			{
				var cs = TreatyClauses.OfType<AllianceClause>();
				if (cs.Any(c => c.AllianceLevel >= AllianceLevel.DefensivePact))
					Recipient.TriggerHappinessChange(hm => hm.TreatyMilitaryAlliance);
				else if (cs.Any(c => c.AllianceLevel >= AllianceLevel.NonAggression))
					Recipient.TriggerHappinessChange(hm => hm.TreatyNonAggression);
				else if (cs.Any(c => c.AllianceLevel >= AllianceLevel.NeutralZone))
					Recipient.TriggerHappinessChange(hm => hm.TreatyNonIntercourse);
			}
			if (TreatyClauses.OfType<CooperativeResearchClause>().Any())
			{
				Recipient.TriggerHappinessChange(hm => hm.TreatyTradeAndResearch);
			}
			if (TreatyClauses.OfType<FreeTradeClause>().Any())
			{
				Recipient.TriggerHappinessChange(hm => hm.TreatyTrade);
			}
			if (TreatyClauses.OfType<TributeClause>().Any())
			{
				Recipient.TriggerHappinessChange(hm => hm.TreatyProtectorateDominant);
				Owner.TriggerHappinessChange(hm => hm.TreatyProtectorateSubordinate);
			}
			if (TreatyClauses.OfType<ShareAbilityClause>().Any()
				|| TreatyClauses.OfType<ShareCombatLogsClause>().Any()
				|| TreatyClauses.OfType<ShareDesignsClause>().Any()
				|| TreatyClauses.OfType<ShareVisionClause>().Any())
			{
				Recipient.TriggerHappinessChange(hm => hm.TreatyPartnership);
			}
		}
	}
}
