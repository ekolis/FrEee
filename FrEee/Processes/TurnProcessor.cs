using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Combat.Grid;
using FrEee.Objects.Combat;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Objects.VictoryConditions;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Enumerations;
using FrEee.Extensions;

namespace FrEee.Processes;

/// <summary>
/// Processes turns.
/// </summary>
public class TurnProcessor
{
	public TurnProcessor(Game game)
	{
		Game = game;
	}

	public Game Game { get; private set; }
	public Galaxy Galaxy => Game.Galaxy;
	public Mod Mod => Game.Mod;


	/// <summary>
	/// Processes the turn.
	/// </summary>
	/// <param name="safeMode">Stop processing if PLR files are missing?</param>
	/// <returns>Player empires which did not submit commands and are not defeated.</returns>
	/// <exception cref="InvalidOperationException">if the Galaxy empire is not null, or this galaxy is not the Galaxy Game.</exception>
	// TODO - make non-static so we don't have to say Game. everywhere
	public IEnumerable<Empire> ProcessTurn(bool safeMode, Status? status = null, double desiredProgress = 1d)
	{
		if (The.Empire != null)
			throw new InvalidOperationException("Can't process the turn if there is a current empire. Load the game host's view of the galaxy instead.");

		var didLastTick = false;

		double progressPerOperation;
		if (status == null)
			progressPerOperation = 0d;
		else // TODO - make a list of operation lambdas and run through them so we don't have to keep count manually & the code is cleaner
			progressPerOperation = (desiredProgress - status.Progress) / (11 + Game.Empires.Count);

		if (status != null)
			status.Message = "Initializing turn";

		foreach (var e in Game.Empires)
		{
			foreach (var m in e.Log.ToArray())
			{
				if (m is IPictorialLogMessage<Battle> bm)
				{
					// purge old battle to save space in the savegame
					var b = bm.Context;
					if (b.Timestamp < Timestamp)
						b.Dispose();
				}
				if (m.TurnNumber < Game.TurnNumber - 10)
				{
					// purge *really* old empire logs too
					e.Log.Remove(m);
				}
			}
		}

		// old seekers are from old battles
		foreach (var r in Game.Referrables.OfType<Seeker>().ToArray())
			r.Dispose();

		// reset anger deltas for new turn
		foreach (var p in Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null))
		{
			var c = p.Colony;
			c.AngerDeltas.Clear();
		}

		if (status != null)
		{
			status.Progress += progressPerOperation;
			status.Message = "Triggering events";
		}

		var dice = new PRNG(HashCodeMasher.Mash(Game.Empires.Where(e => !e.IsDefeated)) + Game.TurnNumber);
		if (RandomHelper.PerMilleChance(Game.Setup.EventFrequency * Game.Empires.Where(e => !e.IsDefeated).Count(), dice))
		{
			// trigger a new event
			var templates = Game.Mod.EventTemplates.Where(t => t.Severity <= Game.Setup.MaximumEventSeverity);
			if (templates.Any())
			{
				var template = templates.PickRandom(dice);
				var evt = template.Instantiate(Game);
				Game.PendingEvents.Add(evt);
				evt.Warn();
			}
		}

		// take care of pending events
		foreach (var evt in Game.PendingEvents.ToArray())
		{
			if (evt.TurnNumber == Game.TurnNumber)
			{
				if (evt.Target != null)
					evt.Execute();
				Game.PendingEvents.Remove(evt);
			}
		}

		// We can enable the ability cache here because space objects aren't changing state yet in any way where order of operations is relevant.
		// For instance, all construction is supposed to take place simultaneously, so there's no reason to allow one construction order to affect other objects' abilities.
		// Plus this speeds up turn processing immensely!
		Game.AbilityManager.EnableServerSideCache();

		// clear treaty clause cache (empires might have added treaties)
		Game.GivenTreatyClauseCache.Clear();
		Game.ReceivedTreatyClauseCache.Clear();

		// delete any floating space objects that are unused
		//Game.SpaceObjectCleanup();

		//Battle.Previous.Clear();
		Galaxy.Battles.Clear();
		PythonScriptEngine.ClearScope(); // no caching galaxy between turns!

		Game.GivenTreatyClauseCache.Clear();
		Game.ReceivedTreatyClauseCache.Clear();

		if (status != null)
			status.Progress += progressPerOperation;

		// load commands
		if (status != null)
			status.Message = "Loading player commands";
		var missingPlrs = Game.LoadCommands();
		if (safeMode && missingPlrs.Any())
			return missingPlrs;
		if (status != null)
			status.Progress += progressPerOperation;

		// AI/minister commands
		if (status != null)
			status.Message = "Playing AI turns";
		if (Game.Empires.Any(e => e.AI != null && (e.EnabledMinisters?.SelectMany(kvp => kvp.Value)?.Any() ?? false)))
		{
			// TODO - use existing player gam file if it exists instead of recreating it in memory
			var serializedgalaxy = Game.SaveToString();
			var cmds = new Dictionary<int, IList<ICommand>>();
			var notes = new Dictionary<int, DynamicDictionary>();
			foreach (var i in Game.Empires.Where(e => e.AI != null && (e.EnabledMinisters?.SelectMany(kvp => kvp.Value)?.Any() ?? false)).Select(e => Game.Empires.IndexOf(e)).ToArray())
			{
				try
				{
					Game = Game.LoadFromString(serializedgalaxy);
					The.Empire = Game.Empires[i];
					Game.Redact();
					The.Empire.AI.Act(The.Empire, Galaxy, The.Empire.EnabledMinisters);

				}
				catch (Exception e)
				{
					//log the error in the ai and move on. 
					//TODO: add in some indication the AI failed. 
					The.Empire.Log.Add(new GenericLogMessage($"AI Error when processing:{e.Message}", LogMessageType.Error));
					e.Log();
				}
				finally
				{
					//these always need to happen, otherwise the code below will throw an exception as it looks for the missing commands. 
					cmds.Add(i, The.Empire.Commands);
					notes.Add(i, The.Empire.AINotes);
				}
			}
			Game = Game.LoadFromString(serializedgalaxy);
			foreach (var i in Game.Empires.Where(e => e.AI != null && (e.EnabledMinisters?.SelectMany(kvp => kvp.Value)?.Any() ?? false)).Select(e => Game.Empires.IndexOf(e)).ToArray())
			{
				try
				{
					Game.LoadCommands(Game.Empires[i], cmds[i]);
					Game.Empires[i].AINotes = notes[i];
				}
				catch (Exception e)
				{
					//log the error and move on so we don't take down FrEee entirely. 
					//TODO: add in some indication the AI failed with commands. 
					e.Log();
				}
			}
		}
		if (status != null)
			status.Progress += progressPerOperation;

		//Game.SpaceObjectIDCheck("after loading commands");

		// advance turn number
		Game.TurnNumber++;

		//Game.SpaceObjectIDCheck("after colony maintenance");

		// resource generation
		if (status != null)
			status.Message = "Generating resources";

		// resource generation 1: colony income
		Galaxy.FindSpaceObjects<Planet>().Where(x => !x.IsMemory).Select(p => p.Colony).ExceptSingle(null).SafeForeach(q => ProcessColonyIncome(q));

		// resource generation 2: remote mining
		// TODO - multithread remote mining once I can figure out where adjustedValue should go
		var adjustedValue = new SafeDictionary<IMineableSpaceObject, ResourceQuantity>(true);
		foreach (var emp in Game.Empires)
		{
			foreach (var kvp in emp.RemoteMiners)
			{
				// consume supplies
				// unlike most other operations, miners that are out of supplies still function
				// because having to resupply miners would be a pain :P
				var miner = kvp.Key.Item1;
				if (miner is SpaceVehicle)
				{
					var sv = miner as SpaceVehicle;
					var miningComps = sv.Components.Where(c => c.Abilities().Any(a => a.Rule.StartsWith("Remote Resource Generation - ")));
					var burn = miningComps.Sum(c => c.Template.SupplyUsage);
					sv.SupplyRemaining -= burn;
					sv.NormalizeSupplies();
				}

				// adjust resource value
				foreach (var r in Resource.All)
				{
					var amount = kvp.Value[r];
					var mined = kvp.Key.Item2;
					if (amount > 0 && adjustedValue[mined][r] == 0)
					{
						// resource was mined here, but hasn't been adjusted yet
						adjustedValue[mined][r] = Game.Setup.RemoteMiningModel.GetDecay(kvp.Value[r], mined.ResourceValue[r]);
						mined.ResourceValue[r] -= adjustedValue[mined][r];
					}
				}
			}

			// give income
			emp.StoredResources += emp.RemoteMiningIncome;
		}

		// resource generation 3: raw resource generation
		foreach (var emp in Game.Empires)
			emp.StoredResources += emp.RawResourceIncome;

		if (status != null)
			status.Progress += progressPerOperation;

		// empire stuff
		// TODO - multithread this, we'll need to get rid of the (1 of 4) or whatever after "Maintaining empires" :(
		foreach (var emp in Game.Empires)
		{
			if (status != null)
				status.Message = "Maintaining empires (" + (Game.Empires.IndexOf(emp) + 1) + " of " + Game.Empires.Count + ")";

			// pay maintenance on on ships/bases
			// TODO - allow mod to specify maintenance on units/facilities too?
			foreach (var v in emp.OwnedSpaceObjects.OfType<SpaceVehicle>().Where(x => !x.IsMemory))
				emp.StoredResources -= v.MaintenanceCost;

			// if not enough funds, lose ships/bases (weighted by maintenance cost)
			// TODO - if mods allow ground-unit/facility maintenance, lose those too?
			// TODO - destroy space units in cargo as well if they pay maintenance?
			// TODO - check if SE4 "saves up" deficits between turns to destroy ships slower than one per turn
			var deficit = -emp.StoredResources.Values.Where(r => r < 0).Sum();
			var lostShips = deficit / Game.Mod.Settings.MaintenanceDeficitToDestroyOneShip;
			for (var i = 0; i < lostShips; i++)
			{
				var ship = emp.OwnedSpaceObjects.OfType<SpaceVehicle>().PickWeighted(x => x.MaintenanceCost.Sum(y => y.Value));
				if (ship != null)
				{
					emp.Log.Add(ship.CreateLogMessage(ship + " fell into disrepair and was scuttled due to lack of funding for maintenance.", LogMessageType.Warning));
					ship.Dispose();
				}
			}

			// execute commands
			foreach (var cmd in emp.Commands.Where(cmd => cmd != null))
			{
				if (cmd.Issuer == emp)
				{
					if (cmd.Executor == null)
						cmd.Issuer.Log.Add(cmd.Issuer.CreateLogMessage("Attempted to issue " + cmd.GetType() + " to a nonexistent object with ID=" + cmd.ExecutorID + ". This is probably a game bug.", LogMessageType.Error));
					else if (cmd.Issuer != cmd.Executor.Owner && cmd.Issuer != cmd.Executor)
						cmd.Issuer.Log.Add(cmd.Issuer.CreateLogMessage("We cannot issue commands to " + cmd.Executor + " because it does not belong to us!", LogMessageType.Error));
					else
						cmd.Execute();
				}
				else if (cmd.Issuer == null)
				{
					Console.Error.WriteLine($"Command {cmd} was issued by nobody!");
				}
				else
				{
					// no hacking!
					cmd.Issuer.Log.Add(new GenericLogMessage(cmd.Issuer.Name + " cannot issue a command to an object belonging to " + emp + "!"));
				}
			}

			// perform treaty actions
			foreach (var clause in emp.GivenTreatyClauses.Flatten())
				clause.PerformAction();

			// don't let stored resources actually fall below zero
			foreach (var r in emp.StoredResources.Keys.Where(r => emp.StoredResources[r] < 0).ToArray())
				emp.StoredResources[r] = 0;

			// do research
			var Spending = emp.ResearchSpending;
			var Queue = emp.ResearchQueue;
			// spend research from spending % priorities
			var spendable = emp.NetIncome[Resource.Research] + emp.BonusResearch;
			foreach (var tech in Spending.Keys.ToArray())
				emp.Research(tech, Spending[tech] * spendable / 100);

			// spend research from queues
			var leftovers = (100 - Spending.Sum(kvp => kvp.Value)) * spendable / 100;
			while (Queue.Any() && leftovers > 0)
			{
				// first tech in queue
				var tech = Queue.First();
				var toSpend = Math.Min(leftovers, tech.GetNextLevelCost(emp) - emp.AccumulatedResearch[tech]);
				emp.Research(tech, toSpend);
				leftovers -= toSpend;
			}

			// no items queued?
			if (!Queue.Any() && leftovers > 0)
			{
				if (Spending.Any(kvp => kvp.Value > 0))
				{
					// pick tech with highest % focus
					emp.Research(Spending.Where(kvp => kvp.Value == Spending.Max(kvp2 => kvp2.Value)).First().Key, leftovers);
				}
				else
				{
					// no techs queued or prioritized, pick a random tech
					var tech = emp.AvailableTechnologies.PickRandom();
					if (tech != null)
						emp.Research(emp.AvailableTechnologies.PickRandom(), leftovers);
				}
			}

			// clear bonus research for this turn
			emp.BonusResearch = 0;

			if (status != null)
				status.Progress += progressPerOperation;

			//Game.SpaceObjectIDCheck("after empire maintenance for " + emp);
		}

		// validate fleets and share supplies
		foreach (var f in Galaxy.FindSpaceObjects<Fleet>().ToArray())
		{
			f.Validate();
			f.ShareSupplies();
		}

		// construction queues
		if (status != null)
			status.Message = "Constructing objects";
		Game.Referrables.OfType<ConstructionQueue>().Where(q => !q.IsMemory && q.Container.Sector != null).SafeForeach(q => q.ExecuteOrders());
		if (status != null)
			status.Progress += progressPerOperation;

		//Game.SpaceObjectIDCheck("after construction");

		// replenish shields
		if (status != null)
			status.Message = "Replenishing shields";
		Galaxy.FindSpaceObjects<ICombatSpaceObject>().SafeForeach(o => o.ReplenishShields());
		if (status != null)
			status.Progress += progressPerOperation;

		//Game.SpaceObjectIDCheck("after shield replenishment");

		// ship movement
		if (status != null)
			status.Message = "Moving ships";
		var tick = 0d;
		Galaxy.FindSpaceObjects<IMobileSpaceObject>().SafeForeach(CommonExtensions.RefillMovement);
		Game.AbilityManager.DisableServerSideCache(); // ships moving about and fighting can affect abilities!
		while (!didLastTick)
		{
			// can at least cache abilities for the duration of a tick
			// seeing as actions within a tick are supposed to be simultaneous
			// the order of execution is arbitrary
			Game.AbilityManager.EnableServerSideCache();

			ComputeNextTickSize();

			// Don't let ships in fleets move separate from their fleets!
			MoveShips();
			tick += NextTickSize;
			if (tick >= 1d)
			{
				tick = 1d;
				NextTickSize = 0d;
				MoveShips();
				didLastTick = true;
			}
			foreach (var f in Game.Referrables.OfType<IFoggable>().Where(f => !f.IsMemory))
				f.Timestamp = Timestamp;
			if (status != null && NextTickSize != double.PositiveInfinity)
				status.Progress += progressPerOperation * NextTickSize;

			//Game.SpaceObjectIDCheck("after ship movement at T=" + Game.Timestamp);

			Game.AbilityManager.DisableServerSideCache();
		}

		if (status != null)
			status.Progress += progressPerOperation;

		//Game.SpaceObjectIDCheck("after shield replenishment");

		// ship movement
		if (status != null)
			status.Message = "Resolving ground battles";

		// resolve ground battles
		foreach (var p in Galaxy.FindSpaceObjects<Planet>(p => p.Cargo != null && p.Cargo.Units.Any(u => u.IsHostileTo(p.Owner) || p.IsHostileTo(u.Owner)))
		)
		{
			var battle = new GroundBattle(p);
			battle.Resolve();
			Galaxy.Battles.Add(battle);
			foreach (var emp in battle.Empires)
				emp.Log.Add(battle.CreateLogMessage(battle.NameFor(emp), LogMessageType.Battle));
		}

		Game.AbilityManager.EnableServerSideCache();

		// validate fleets again (ships might have been destroyed, consumed supplies, etc...)
		foreach (var f in Game.Referrables.OfType<Fleet>().ToArray())
		{
			f.Validate();
			f.ShareSupplies();
		}

		// TODO - more turn stuff? or do we have everything?

		// colony maintenance
		if (status != null)
			status.Message = "Maintaining colonies";
		if (Game.TurnNumber.IsDivisibleBy(Mod.Settings.ReproductionFrequency.DefaultTo(1)))
			Galaxy.FindSpaceObjects<Planet>(p => p.HasColony).SafeForeach(ProcessPopulationGrowth);
		if (Game.TurnNumber.IsDivisibleBy(Mod.Settings.ValueChangeFrequency.DefaultTo(1)))
			Galaxy.FindSpaceObjects<Planet>(p => p.HasColony).SafeForeach(q => ProcessResourceValueChange(q));
		if (status != null)
			status.Progress += progressPerOperation;

		if (status != null)
			status.Message = "Cleaning up";

		// deal with population in cargo again, in case colonies took damage and lost some population
		// TODO - multithread population cargo maintenance
		foreach (var p in Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null))
		{
			var pop = p.Colony.Population;
			var ratio = (double)pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
			if (ratio < 1)
			{
				var cargo = p.Cargo;
				if (cargo != null)
				{
					// bring population out of cold storage
					// do this by removing and adding the population
					// this will work since population is removed from cargo storage first but added to population storage first
					foreach (var kvp in cargo.Population.ToArray())
					{
						var amount = kvp.Value;
						amount -= p.RemovePopulation(kvp.Key, kvp.Value);
						p.AddPopulation(kvp.Key, kvp.Value);
					}
				}
			}
		}

		// replenish shields again, so the players see the full shield amounts in the GUI
		Galaxy.FindSpaceObjects<ICombatSpaceObject>().SafeForeach(o => o.ReplenishShields());

		// modify colony anger
		foreach (var ship in Galaxy.FindSpaceObjects<MajorSpaceVehicle>().Where(x => !x.IsDestroyed))
		{
			foreach (var emp in Game.Empires.Where(e => e.CanSee(ship)))
			{
				if (emp == ship.Owner)
				{
					emp.TriggerHappinessChange(ship.StarSystem, hm => hm.OurShipInSystem);
					emp.TriggerHappinessChange(ship.Sector, hm => hm.OurShipInSector);
				}
				else if (emp.IsEnemyOf(ship.Owner, ship.StarSystem))
				{
					emp.TriggerHappinessChange(ship.StarSystem, hm => hm.EnemyShipInSystem);
					emp.TriggerHappinessChange(ship.Sector, hm => hm.EnemyShipInSector);
				}
			}
		}
		Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null).Select(p => p.Colony).ParallelSafeForeach(c =>
		{
			if (c.Cargo.Units.Any(u => u.IsHostileTo(c.Owner)))
				c.TriggerHappinessChange(hm => hm.EnemyTroopsOnPlanet);
			c.TriggerHappinessChange(hm => hm.OurTroopOnPlanet * c.Cargo.Units.OfType<Troop>().Count());
		});
		Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null).Select(p => p.Colony).ParallelSafeForeach(c =>
		{
			foreach (var race in c.Population.Keys)
			{
				if (!race.HasAbility("Population Emotionless") && race.HappinessModel != null)
				{
					var delta = c.AngerDeltas[race];
					delta += race == c.Owner.PrimaryRace ? race.HappinessModel.NaturalTurnAngerChangeOurRace : race.HappinessModel.NaturalTurnAngerChangeOtherRaces;
					delta -= c.StarSystem.GetEmpireAbilityValue(c.Owner, "Change Population Happiness - System").ToInt() * 10;
					if (delta > race.HappinessModel.MaxPositiveTurnAngerChange)
						delta = race.HappinessModel.MaxPositiveTurnAngerChange;
					if (delta < race.HappinessModel.MaxNegativeTurnAngerChange)
						delta = race.HappinessModel.MaxNegativeTurnAngerChange;
					c.Anger[race] += delta;
					if (c.Anger[race] > Mod.Settings.MaxAnger)
						c.Anger[race] = Mod.Settings.MaxAnger;
					if (c.Anger[race] < Mod.Settings.MinAnger)
						c.Anger[race] = Mod.Settings.MinAnger;

					// TODO - display reason for growing happy/unhappy
					if (delta >= 100)
						c.Owner.RecordLog(c.Container, $"The {race} population of {c.Container} is growing very unhappy.", LogMessageType.Generic);
					else if (delta <= -100)
						c.Owner.RecordLog(c.Container, $"The {race} population of {c.Container} is growing very happy.", LogMessageType.Generic);
				}
			}
		});

		// repair facilities
		// planets with no population won't repair facilities
		Galaxy.FindSpaceObjects<Planet>().Select(p => p.Colony).Where(c => c != null && c.Population.Any(q => q.Value > 0)).SelectMany(c => c.Facilities).SafeForeach(f => f.Repair());

		// repair units
		// planets with no population won't repair units
		Galaxy.FindSpaceObjects<IMobileSpaceObject>().OfType<IUnit>().SafeForeach(u => u.Repair());
		Galaxy.FindSpaceObjects<ISpaceObject>().OfType<ICargoContainer>().Where(
			p => p.Cargo != null
			&& (!(p is Planet) || (((Planet)p).Colony?.Population.Any(q => q.Value > 0) ?? false)))
			.SelectMany(p => p.Cargo.Units).SafeForeach(u => u.Repair());

		// repair ships/bases
		// TODO - repair priorities
		foreach (var emp in Game.Empires)
		{
			// component repair is per sector per turn per empire, so we need to track it that way
			var usedPts = new SafeDictionary<Sector, int>();
			foreach (var v in Galaxy.FindSpaceObjects<IMobileSpaceObject>().Where(v => v.Owner == emp && v.Sector != null && (v is Ship || v is Base || v is Fleet)))
			{
				var pts = v.Sector.GetEmpireAbilityValue(emp, "Component Repair").ToInt() - usedPts[v.Sector];
				usedPts[v.Sector] += pts - v.Repair(pts).Value;
			}
		}

		// repairs affect abilities
		Game.AbilityManager.DisableServerSideCache();
		Game.AbilityManager.EnableServerSideCache();

		// get supplies from reactors, solar panels, etc.
		Galaxy.FindSpaceObjects<IMobileSpaceObject>().SafeForeach(v =>
		{
			v.SupplyRemaining += v.GetAbilityValue("Supply Generation Per Turn").ToInt();
			if (v.StarSystem != null)
				v.SupplyRemaining += v.GetAbilityValue("Solar Supply Generation").ToInt() * v.StarSystem.FindSpaceObjects<Star>().Count();
			v.NormalizeSupplies();
		});

		// resupply space vehicles one last time (after weapons fire and repair which could affect supply remaining/storage)
		Galaxy.FindSpaceObjects<ISpaceObject>().Where(s => s.HasAbility("Supply Generation")).SafeForeach(sobj =>
		{
			var emp = sobj.Owner;
			var sector = sobj.Sector;
			foreach (var v in sector.SpaceObjects.OfType<IMobileSpaceObject>().Where(v => v.Owner == emp))
				v.SupplyRemaining = v.SupplyStorage;
		});
		// TODO - multithread this... somehow...
		foreach (var emp in Game.Empires)
		{
			foreach (var sys in Galaxy.StarSystemLocations.Select(l => l.Item).Where(s => s.HasAbility("Supply Generation - System", emp) || s.HasAbility("Supply Generation - System")))
			{
				foreach (var v in sys.FindSpaceObjects<IMobileSpaceObject>().Where(v => v.Owner == emp))
					v.SupplyRemaining = v.SupplyStorage;
			}
		}

		Game.Empires.ParallelSafeForeach(emp =>
		{
			emp.StoredResources = ResourceQuantity.Min(emp.StoredResources, emp.ResourceStorage);// resource spoilage
			emp.Commands.Clear(); // clear empire commands
			emp.Scores[Game.TurnNumber] = emp.ComputeScore(null); // update score
		});

		// clear completed orders
		Game.Referrables.OfType<IPathfindingOrder>().Where(o => o.KnownTarget == null).ParallelSafeForeach(o => o.IsComplete = true);
		Game.Referrables.OfType<IOrder>().Where(o => o.IsComplete).SafeForeach(o => o.Dispose());

		// update known designs
		Game.Empires.ParallelSafeForeach(emp =>
		{
			foreach (var design in Game.Referrables.OfType<IDesign>())
			{
				if (design.CheckVisibility(emp) >= Visibility.Scanned && !emp.KnownDesigns.Contains(design))
					emp.KnownDesigns.Add(design);
			}
		});

		// clear obsolete sensor ghosts
		// TODO - multithread this somehow
		foreach (var emp in Game.Empires)
		{
			foreach (var kvp in emp.Memory.ToArray())
			{
				if (kvp.Value.IsObsoleteMemory(emp))
				{
					emp.Memory.Remove(kvp);
					kvp.Value.Dispose();
				}
			}
		}

		// validate fleets and share supplies
		foreach (var f in Galaxy.FindSpaceObjects<Fleet>().ToArray())
		{
			f.Validate();
			f.ShareSupplies();
		}

		// check for victory/defeat
		foreach (var vc in Game.Setup.VictoryConditions)
		{
			if (vc is TotalEliminationVictoryCondition || Game.TurnNumber > Game.Setup.VictoryDelay)
			{
				// find winners
				var winners = new List<Empire>();
				foreach (var emp in Game.Empires)
				{
					if (vc.GetProgress(emp) >= 1d)
					{
						// empire won!
						emp.IsWinner = true;
						emp.Log.Add(emp.CreateLogMessage(vc.GetVictoryMessage(emp), LogMessageType.Victory));
						winners.Add(emp);
					}
				}

				// if there were any winners, everyone else lost :(
				if (winners.Any())
				{
					foreach (var emp in Game.Empires.Except(winners))
					{
						// empire lost because someone else won
						emp.IsLoser = true;
						emp.Log.Add(emp.CreateLogMessage(vc.GetDefeatMessage(emp, winners), LogMessageType.Victory));
					}
				}
			}
		}

		if (status != null)
			status.Progress += progressPerOperation;

		// dispose of invalid waypoints e.g. space object got destroyed
		foreach (var w in Game.Referrables.OfType<Waypoint>().ToArray())
		{
			if (w.Sector == null)
				w.Dispose();
		}

		foreach (var x in Galaxy.FindSpaceObjects<ISpaceObject>().Owned().ToArray())
			x.UpdateEmpireMemories();

		// save off tech levels for computing tech uniqueness next turn
		Game.SaveTechLevelsForUniqueness();

		//Game.SpaceObjectIDCheck("after cleanup");

		// end of turn scripts
		if (status != null)
			status.Message = "Executing scripts";
		PythonScriptEngine.RunScript<object>(Mod.EndTurnScript);
		if (status != null)
			status.Progress += progressPerOperation;

		// delete any floating space objects that are unused
		//Game.SpaceObjectCleanup();

		//Game.SpaceObjectIDCheck("at end of turn");

		Game.StringValue = null;

		return missingPlrs;
	}

	/// <summary>
	/// Only public for unit tests. You should probably call ProcessTurn instead.
	/// </summary>
	/// <param name="p"></param>
	private void ProcessColonyIncome(Colony c)
	{
		var p = c.Container;
		var sys = p.StarSystem;
		var income = p.GrossIncome();

		// log messages
		if (income < p.GrossIncomeIgnoringSpaceport)
		{
			var ratio = p.Colony.MerchantsRatio;
			if (ratio == 0)
				p.Owner.Log.Add(p.CreateLogMessage(p + " earned no income due to lack of a spaceport.", LogMessageType.Warning));
			else if (ratio < 1)
				p.Owner.Log.Add(p.CreateLogMessage(p + " earned only " + Math.Floor(ratio * 100) + "% of normal income due to lack of a spaceport.", LogMessageType.Warning));
		}

		// give owner his income
		lock (p.Owner.StoredResources) p.Owner.StoredResources += income;

		// adjust resource value
		// TODO - have a "is mineable" or "has value" property on Resource class
		var incomeWithoutValue = new ResourceQuantity();
		if (p.ResourceValue[Resource.Minerals] != 0)
			incomeWithoutValue += income[Resource.Minerals] / p.ResourceValue[Resource.Minerals] * Resource.Minerals;
		if (p.ResourceValue[Resource.Organics] != 0)
			incomeWithoutValue += income[Resource.Organics] / p.ResourceValue[Resource.Organics] * Resource.Organics;
		if (p.ResourceValue[Resource.Radioactives] != 0)
			incomeWithoutValue += income[Resource.Radioactives] / p.ResourceValue[Resource.Radioactives] * Resource.Radioactives;
		incomeWithoutValue += income[Resource.Research] * Resource.Research;
		incomeWithoutValue += income[Resource.Intelligence] * Resource.Research;
		foreach (var kvp in incomeWithoutValue)
		{
			p.ResourceValue[kvp.Key] -= Game.Setup.StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
		}
	}

	/// <summary>
	/// Only public for unit tests. You should probably call ProcessTurn instead.
	/// </summary>
	/// <param name="p"></param>
	private static void ProcessPopulationGrowth(Planet p)
	{
		var pop = p.Colony.Population;
		var wasFull = p.PopulationStorageFree == 0;
		var deltas = p.PopulationChangePerTurnPerRace;
		foreach (var race in pop.Keys.ToArray())
		{
			pop[race] += deltas[race];
		}

		// deal with overpopulation
		var ratio = (double)pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
		if (ratio > 1)
		{
			foreach (var race in pop.Keys.ToArray())
			{
				// TODO - should planetary population spill over into cargo?
				// this might be annoying for homeworlds, as their cargo space would fill up quickly...
				// especially in Proportions Mod with its 1000kT/1M population!
				pop[race] = (long)(pop[race] / ratio);
			}
			if (!wasFull && p.Owner != null)
				p.Owner.RecordLog(p, "{0} has completely filled up with population. Building colonizers or transports is advised.".F(p), LogMessageType.Generic);
		}

		// deal with population in cargo
		ratio = (double)pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
		if (ratio < 1)
		{
			var cargo = p.Cargo;
			if (cargo != null)
			{
				// bring population out of cold storage
				// do this by removing and adding the population
				// this will work since population is removed from cargo storage first but added to population storage first
				foreach (var kvp in cargo.Population.ToArray())
				{
					var amount = kvp.Value;
					amount -= p.RemovePopulation(kvp.Key, kvp.Value);
					p.AddPopulation(kvp.Key, kvp.Value);
				}
			}
		}
	}

	/// <summary>
	/// Only public for unit tests. You should probably call ProcessTurn instead.
	/// </summary>
	/// <param name="p"></param>
	private void ProcessResourceValueChange(Planet p)
	{
		foreach (var r in Resource.All.Where(r => r.HasValue))
		{
			var wasFull = p.ResourceValue[r] == Game.Setup.MaxPlanetValue;
			var wasEmpty = p.ResourceValue[r] == Game.Setup.MinPlanetValue;
			var modifier =
				p.GetAbilityValue("Planet - Change {0} Value".F(r.Name)).ToInt()
				+ p.GetAbilityValue("Sector - Change {0} Value".F(r.Name)).ToInt()
				+ p.GetAbilityValue("System - Change {0} Value".F(r.Name)).ToInt()
				+ p.GetAbilityValue("Empire - Change {0} Value".F(r.Name)).ToInt();
			p.ResourceValue[r] += modifier;
			p.ResourceValue[r] = p.ResourceValue[r].LimitToRange(Game.Setup.MinPlanetValue, Game.Setup.MaxPlanetValue);
			if (!wasFull && p.ResourceValue[r] == Game.Setup.MaxPlanetValue && p.Owner != null)
				p.Owner.RecordLog(p, "{0}'s {1} have been completely replenished. Its value is at the absolute maximum.".F(p, r), LogMessageType.Generic);
			if (!wasEmpty && p.ResourceValue[r] == Game.Setup.MinPlanetValue && p.Owner != null)
				p.Owner.RecordLog(p, "{0} has been stripped dry of {1}. Its value is at the bare minimum.".F(p, r), LogMessageType.Generic);
		}
	}

	/// <summary>
	/// Only public for unit tests. You should probably call ProcessTurn instead.
	/// </summary>
	public void MoveShips()
	{
		var vlist = Galaxy.FindSpaceObjects<IMobileSpaceObject>().Where(sobj => sobj.Container == null && !sobj.IsMemory).Shuffle();
		foreach (var v in vlist)
		{
			// mark system explored if not already
			var sys = v.StarSystem;
			if (sys == null)
				continue; // space object is dead, or not done being built

			if (CurrentTick == 0d && !v.Orders.OfType<IMovementOrder>().Any())
				v.DealWithMines();

			bool didStuff = v.ExecuteOrders();
			sys.MarkAsExploredBy(v.Owner);

			// update memory sight after movement
			if (didStuff)
			{
				v.UpdateEmpireMemories();
				if (v.StarSystem != null && v.Owner != null)
				{
					foreach (var sobj in v.StarSystem.FindSpaceObjects<ISpaceObject>().Where(sobj => sobj != v && !sobj.IsMemory && v.Owner.CanSee(sobj)).ToArray())
						v.Owner.UpdateMemory(sobj);
				}

				// replenish shields after moving (who knows, we might be out of supplies, or about to hit a minefield)
				v.ReplenishShields();
			}

			// check for battles
			var sector = v.Sector;
			if (v.Owner != null && // unowned objects can't pick fights
				sector != null && // can't fight nowhere
				sector.SpaceObjects.OfType<ICombatant>().Any(
					sobj =>
						(sobj.IsHostileTo(v.Owner) && sobj.Weapons.Any() || v.IsHostileTo(sobj.Owner) && v.Weapons.Any()) // any enemies?
						&& (sobj.Owner.CanSee(v) || v.Owner.CanSee(sobj)) // enemies are visible?
						&& (!lastBattleTimestamps.ContainsKey(sector) || lastBattleTimestamps[sector] < Timestamp - (v.StrategicSpeed == 0 ? 1d : 1d / v.StrategicSpeed)))) // have we fought here too recently?
			{
				// resolve the battle
				var battle = new SpaceBattle(sector);
				battle.Resolve();
				Galaxy.Battles.Add(battle);
				foreach (var emp in battle.Empires)
					emp.Log.Add(battle.CreateLogMessage(battle.NameFor(emp), LogMessageType.Battle));
				lastBattleTimestamps[sector] = Timestamp;
			}
		}
	}

	private IDictionary<Sector, double> lastBattleTimestamps = new SafeDictionary<Sector, double>();

	/// <summary>
	/// The current tick in turn processing. 0 = start of turn, 1 = end of turn.
	/// </summary>
	public double CurrentTick { get; set; }

	/// <summary>
	/// Current time equals turn number plus tick minus 1.
	/// </summary>
	public double Timestamp => Game.TurnNumber + CurrentTick - 1;

	/// <summary>
	/// The next tick size, for ship movement.
	/// </summary>
	public double NextTickSize { get; internal set; }

	public void ComputeNextTickSize()
	{
		var objs = Galaxy.FindSpaceObjects<IMobileSpaceObject>().Where(obj => obj.Orders.Any());
		objs = objs.Where(obj => !obj.IsMemory);
		if (objs.Where(v => v.TimeToNextMove > 0).Any() && CurrentTick < 1.0)
		{
			// HACK - why are objects getting zero time to next move?!
			var nextTickSize = objs.Where(v => v.TimeToNextMove > 0).Min(v => v.TimeToNextMove);
			NextTickSize = Math.Min(1.0 - CurrentTick, nextTickSize);
		}
		else if (objs.Any())
		{
			NextTickSize = objs.Min(v => v.TimePerMove);
		}
		else
			NextTickSize = double.PositiveInfinity;
	}
}
