using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.Civilization;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Objects.VictoryConditions;
using FrEee.Modding;
using FrEee.Extensions;
using FrEee.Utility;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Modding.Scripts;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;
using FrEee.Processes;
using FrEee.Plugins.Default.Processes.Combat.Grid;
using System.ComponentModel.Composition;
using FrEee.Processes.Construction;

namespace FrEee.Plugins.Default.Processes;

/// <summary>
/// Stock implementation of <see cref="ITurnProcessor"/>.
/// </summary>
[Export(typeof(IPlugin))]
public class TurnProcessor
	: Plugin<ITurnProcessor>, ITurnProcessor
{
	public override string Name { get; } = "TurnProcessor";

	public override ITurnProcessor Implementation => this;

	public IEnumerable<Empire> ProcessTurn(Game game, bool safeMode, Status status = null, double desiredProgress = 1d)
	{
		//galaxy.SpaceObjectIDCheck("at start of turn");

		if (Empire.Current != null)
			throw new InvalidOperationException("Can't process the turn if there is a current empire. Load the game host's view of the galaxy instead.");

		var didLastTick = false;

		double progressPerOperation;
		if (status == null)
			progressPerOperation = 0d;
		else // TODO - make a list of operation lambdas and run through them so we don't have to keep count manually & the code is cleaner
			progressPerOperation = (desiredProgress - status.Progress) / (11 + game.Empires.Count);

		if (status != null)
			status.Message = "Initializing turn";

		foreach (var e in game.Empires)
		{
			foreach (var m in e.Log.ToArray())
			{
				if (m is IPictorialLogMessage<Battle> bm)
				{
					// purge old battle to save space in the savegame
					var b = bm.Context;
					if (b.Timestamp < game.Timestamp)
						b.Dispose();
				}
				if (m.TurnNumber < game.TurnNumber - 10)
				{
					// purge *really* old empire logs too
					e.Log.Remove(m);
				}
			}
		}

		// old seekers are from old battles
		foreach (var r in game.Referrables.OfType<Seeker>().ToArray())
			r.Dispose();

		// reset anger deltas for new turn
		foreach (var p in game.Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null))
		{
			var c = p.Colony;
			c.AngerDeltas.Clear();
		}

		if (status != null)
		{
			status.Progress += progressPerOperation;
			status.Message = "Triggering events";
		}

		var dice = new PRNG(HashCodeMasher.Mash(game.Empires.Where(e => !e.IsDefeated)) + game.TurnNumber);
		if (RandomHelper.PerMilleChance(game.EventFrequency * game.Empires.Where(e => !e.IsDefeated).Count(), dice))
		{
			// trigger a new event
			var templates = Mod.Current.EventTemplates.Where(t => t.Severity <= game.Setup.MaximumEventSeverity);
			if (templates.Any())
			{
				var template = templates.PickRandom(dice);
				var evt = template.Instantiate();
				game.PendingEvents.Add(evt);
				evt.Warn();
			}
		}

		// take care of pending events
		foreach (var evt in game.PendingEvents.ToArray())
		{
			if (evt.TurnNumber == game.TurnNumber)
			{
				if (evt.Target != null)
					evt.Execute();
				game.PendingEvents.Remove(evt);
			}
		}

		// We can enable the ability cache here because space objects aren't changing state yet in any way where order of operations is relevant.
		// For instance, all construction is supposed to take place simultaneously, so there's no reason to allow one construction order to affect other objects' abilities.
		// Plus this speeds up turn processing immensely!
		game.EnableAbilityCache();

		// clear treaty clause cache (empires might have added treaties)
		game.ResetTreatyClauseCaches();

		// delete any floating space objects that are unused
		//galaxy.SpaceObjectCleanup();

		//Battle.Previous.Clear();
		game.Battles.Clear();
		PythonScriptEngine.ClearScope(); // no caching galaxy between turns!

		game.ResetTreatyClauseCaches();

		if (status != null)
			status.Progress += progressPerOperation;

		// load commands
		if (status != null)
			status.Message = "Loading player commands";
		var missingPlrs = game.LoadCommands();
		if (safeMode && missingPlrs.Any())
			return missingPlrs;
		if (status != null)
			status.Progress += progressPerOperation;

		// AI/minister commands
		if (status != null)
			status.Message = "Playing AI turns";
		if (game.Empires.Any(e => e.AI != null && (e.EnabledMinisters?.SelectMany(kvp => kvp.Value)?.Any() ?? false)))
		{
			// TODO - use existing player gam file if it exists instead of recreating it in memory
			var serializedgalaxy = game.SaveToString();
			var cmds = new Dictionary<int, IList<ICommand>>();
			var notes = new Dictionary<int, DynamicDictionary>();
			foreach (var i in game.Empires.Where(e => e.AI != null && (e.EnabledMinisters?.SelectMany(kvp => kvp.Value)?.Any() ?? false)).Select(e => game.Empires.IndexOf(e)).ToArray())
			{
				try
				{
					Game.LoadFromString(serializedgalaxy);
					game = Game.Current;
					game.CurrentEmpire = game.Empires[i];
					game.Redact();
					game.CurrentEmpire.AI.Act(game.CurrentEmpire, game, game.CurrentEmpire.EnabledMinisters);

				}
				catch (Exception e)
				{
					//log the error in the ai and move on. 
					//TODO: add in some indication the AI failed. 
					game.CurrentEmpire.Log.Add(new GenericLogMessage($"AI Error when processing:{e.Message}", LogMessageType.Error));
					e.Log();
				}
				finally
				{
					//these always need to happen, otherwise the code below will throw an exception as it looks for the missing commands. 
					cmds.Add(i, game.CurrentEmpire.Commands);
					notes.Add(i, game.CurrentEmpire.AINotes);
				}
			}
			Game.LoadFromString(serializedgalaxy);
			game = Game.Current;
			foreach (var i in game.Empires.Where(e => e.AI != null && (e.EnabledMinisters?.SelectMany(kvp => kvp.Value)?.Any() ?? false)).Select(e => game.Empires.IndexOf(e)).ToArray())
			{
				try
				{
					game.LoadCommands(game.Empires[i], cmds[i]);
					game.Empires[i].AINotes = notes[i];
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

		//galaxy.SpaceObjectIDCheck("after loading commands");

		// advance turn number
		game.TurnNumber++;

		//galaxy.SpaceObjectIDCheck("after colony maintenance");

		// resource generation
		if (status != null)
			status.Message = "Generating resources";

		// resource generation 1: colony income
		game.Galaxy.FindSpaceObjects<Planet>().Where(x => !x.IsMemory).Select(p => p.Colony).ExceptSingle(null).SafeForeach(q => ProcessColonyIncome(game, q));

		// resource generation 2: remote mining
		// TODO - multithread remote mining once I can figure out where adjustedValue should go
		var adjustedValue = new SafeDictionary<IMineableSpaceObject, ResourceQuantity>(true);
		foreach (var emp in game.Empires)
		{
			foreach (var kvp in emp.RemoteMiners)
			{
				// consume supplies
				// unlike most other operations, miners that are out of supplies still function
				// because having to resupply miners would be a pain :P
				var miner = kvp.Key.Item1;
				if (miner is ISpaceVehicle sv)
				{
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
						adjustedValue[mined][r] = game.Setup.RemoteMiningModel.GetDecay(kvp.Value[r], mined.ResourceValue[r]);
						mined.ResourceValue[r] -= adjustedValue[mined][r];
					}
				}
			}

			// give income
			emp.StoredResources += emp.RemoteMiningIncome;
		}

		// resource generation 3: raw resource generation
		foreach (var emp in game.Empires)
			emp.StoredResources += emp.RawResourceIncome;

		if (status != null)
			status.Progress += progressPerOperation;

		//galaxy.SpaceObjectIDCheck("after resource generation");

		// empire stuff
		// TODO - multithread this, we'll need to get rid of the (1 of 4) or whatever after "Maintaining empires" :(
		foreach (var emp in game.Empires)
		{
			if (status != null)
				status.Message = "Maintaining empires (" + (game.Empires.IndexOf(emp) + 1) + " of " + game.Empires.Count + ")";

			// pay maintenance on on ships/bases
			// TODO - allow mod to specify maintenance on units/facilities too?
			foreach (var v in emp.OwnedSpaceObjects.OfType<ISpaceVehicle>().Where(x => !x.IsMemory))
			{
				emp.StoredResources -= v.MaintenanceCost;
			}

			// if not enough funds, lose ships/bases (weighted by maintenance cost)
			// TODO - if mods allow ground-unit/facility maintenance, lose those too?
			// TODO - destroy space units in cargo as well if they pay maintenance?
			// TODO - check if SE4 "saves up" deficits between turns to destroy ships slower than one per turn
			var deficit = -emp.StoredResources.Values.Where(r => r < 0).Sum();
			var lostShips = deficit / Mod.Current.Settings.MaintenanceDeficitToDestroyOneShip;
			for (int i = 0; i < lostShips; i++)
			{
				var ship = emp.OwnedSpaceObjects.OfType<ISpaceVehicle>().PickWeighted(x => x.MaintenanceCost.Sum(y => y.Value));
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

			//galaxy.SpaceObjectIDCheck("after empire maintenance for " + emp);
		}

		// validate fleets and share supplies
		foreach (var f in game.Galaxy.FindSpaceObjects<Fleet>().ToArray())
		{
			f.Validate();
			f.ShareSupplies();
		}

		// construction queues
		if (status != null)
			status.Message = "Constructing objects";
		game.Referrables.OfType<IConstructionQueue>().Where(q => !q.IsMemory && q.Container.Sector != null).SafeForeach(q => q.ExecuteOrders());
		if (status != null)
			status.Progress += progressPerOperation;

		//galaxy.SpaceObjectIDCheck("after construction");

		// replenish shields
		if (status != null)
			status.Message = "Replenishing shields";
		game.Galaxy.FindSpaceObjects<ICombatSpaceObject>().SafeForeach(o => o.ReplenishShields());
		if (status != null)
			status.Progress += progressPerOperation;

		//galaxy.SpaceObjectIDCheck("after shield replenishment");

		// ship movement
		if (status != null)
			status.Message = "Moving ships";
		var tick = 0d;
		game.Galaxy.FindSpaceObjects<IMobileSpaceObject>().SafeForeach(CommonExtensions.RefillMovement);
		game.DisableAbilityCache(); // ships moving about and fighting can affect abilities!
		while (!didLastTick)
		{
			// can at least cache abilities for the duration of a tick
			// seeing as actions within a tick are supposed to be simultaneous
			// the order of execution is arbitrary
			game.EnableAbilityCache();

			game.ComputeNextTickSize();

			// Don't let ships in fleets move separate from their fleets!
			game.MoveShips();
			tick += game.NextTickSize;
			if (tick >= 1d)
			{
				tick = 1d;
				game.NextTickSize = 0d;
				game.MoveShips();
				didLastTick = true;
			}
			foreach (var f in game.Referrables.OfType<IFoggable>().Where(f => !f.IsMemory))
				f.Timestamp = game.Timestamp;
			if (status != null && game.NextTickSize != double.PositiveInfinity)
				status.Progress += progressPerOperation * game.NextTickSize;

			//galaxy.SpaceObjectIDCheck("after ship movement at T=" + galaxy.Timestamp);

			game.DisableAbilityCache();
		}

		if (status != null)
			status.Progress += progressPerOperation;

		//galaxy.SpaceObjectIDCheck("after shield replenishment");

		// ship movement
		if (status != null)
			status.Message = "Resolving ground battles";

		// resolve ground battles
		foreach (var p in game.Galaxy.FindSpaceObjects<Planet>(p => p.Cargo != null && p.Cargo.Units.Any(u => u.IsHostileTo(p.Owner) || p.IsHostileTo(u.Owner)))
		)
		{
			var battle = Services.Battles.CreateGroundBattle(p);
			battle.Resolve();
			game.Battles.Add(battle);
			foreach (var emp in battle.Empires)
				emp.Log.Add(battle.CreateLogMessage(battle.NameFor(emp), LogMessageType.Battle));
		}

		game.EnableAbilityCache();

		// validate fleets again (ships might have been destroyed, consumed supplies, etc...)
		foreach (var f in game.Referrables.OfType<Fleet>().ToArray())
		{
			f.Validate();
			f.ShareSupplies();
		}

		// TODO - more turn stuff? or do we have everything?

		// colony maintenance
		if (status != null)
			status.Message = "Maintaining colonies";
		if (game.TurnNumber.IsDivisibleBy(Mod.Current.Settings.ReproductionFrequency.DefaultTo(1)))
			game.Galaxy.FindSpaceObjects<Planet>(p => p.HasColony).SafeForeach(ProcessPopulationGrowth);
		if (game.TurnNumber.IsDivisibleBy(Mod.Current.Settings.ValueChangeFrequency.DefaultTo(1)))
			game.Galaxy.FindSpaceObjects<Planet>(p => p.HasColony).SafeForeach(q => ProcessResourceValueChange(game, q));
		if (status != null)
			status.Progress += progressPerOperation;

		if (status != null)
			status.Message = "Cleaning up";

		// deal with population in cargo again, in case colonies took damage and lost some population
		// TODO - multithread population cargo maintenance
		foreach (var p in game.Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null))
		{
			var pop = p.Colony.Population;
			var ratio = pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
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
		game.Galaxy.FindSpaceObjects<ICombatSpaceObject>().SafeForeach(o => o.ReplenishShields());

		// modify colony anger
		foreach (var ship in game.Galaxy.FindSpaceObjects<IMajorSpaceVehicle>().Where(x => !x.IsDestroyed))
		{
			foreach (var emp in game.Empires.Where(e => e.CanSee(ship)))
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
		game.Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null).Select(p => p.Colony).ParallelSafeForeach(c =>
		{
			if (c.Cargo.Units.Any(u => u.IsHostileTo(c.Owner)))
				c.TriggerHappinessChange(hm => hm.EnemyTroopsOnPlanet);
			c.TriggerHappinessChange(hm => hm.OurTroopOnPlanet * c.Cargo.Units.Where(q => q.CanInvadeAndPoliceColonies).Count());
		});
		game.Galaxy.FindSpaceObjects<Planet>().Where(p => p.Colony != null).Select(p => p.Colony).ParallelSafeForeach(c =>
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
					if (c.Anger[race] > Mod.Current.Settings.MaxAnger)
						c.Anger[race] = Mod.Current.Settings.MaxAnger;
					if (c.Anger[race] < Mod.Current.Settings.MinAnger)
						c.Anger[race] = Mod.Current.Settings.MinAnger;

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
		game.Galaxy.FindSpaceObjects<Planet>().Select(p => p.Colony).Where(c => c != null && c.Population.Any(q => q.Value > 0)).SelectMany(c => c.Facilities).SafeForeach(f => f.Repair());

		// repair units
		// planets with no population won't repair units
		game.Galaxy.FindSpaceObjects<IMobileSpaceObject>().OfType<IUnit>().SafeForeach(u => u.Repair());
		game.Galaxy.FindSpaceObjects<ISpaceObject>().OfType<ICargoContainer>().Where(
			p => p.Cargo != null
			&& (!(p is Planet) || (((Planet)p).Colony?.Population.Any(q => q.Value > 0) ?? false)))
			.SelectMany(p => p.Cargo.Units).SafeForeach(u => u.Repair());

		// repair ships/bases
		// TODO - repair priorities
		foreach (var emp in game.Empires)
		{
			// component repair is per sector per turn per empire, so we need to track it that way
			var usedPts = new SafeDictionary<Sector, int>();
			foreach (var v in game.Galaxy.FindSpaceObjects<IMobileSpaceObject>().Where(v => v.Owner == emp && v.Sector != null && (v is IMajorSpaceVehicle || v is Fleet)))
			{
				var pts = v.Sector.GetEmpireAbilityValue(emp, "Component Repair").ToInt() - usedPts[v.Sector];
				usedPts[v.Sector] += pts - v.Repair(pts).Value;
			}
		}

		// repairs affect abilities
		game.DisableAbilityCache();
		game.EnableAbilityCache();

		// get supplies from reactors, solar panels, etc.
		game.Galaxy.FindSpaceObjects<IMobileSpaceObject>().SafeForeach(v =>
		{
			v.SupplyRemaining += v.GetAbilityValue("Supply Generation Per Turn").ToInt();
			if (v.StarSystem != null)
				v.SupplyRemaining += v.GetAbilityValue("Solar Supply Generation").ToInt() * v.StarSystem.FindSpaceObjects<Star>().Count();
			v.NormalizeSupplies();
		});

		// resupply space vehicles one last time (after weapons fire and repair which could affect supply remaining/storage)
		game.Galaxy.FindSpaceObjects<ISpaceObject>().Where(s => s.HasAbility("Supply Generation")).SafeForeach(sobj =>
		{
			var emp = sobj.Owner;
			var sector = sobj.Sector;
			foreach (var v in sector.SpaceObjects.OfType<IMobileSpaceObject>().Where(v => v.Owner == emp))
				v.SupplyRemaining = v.SupplyStorage;
		});
		// TODO - multithread this... somehow...
		foreach (var emp in game.Empires)
		{
			foreach (var sys in game.Galaxy.StarSystemLocations.Select(l => l.Item).Where(s => s.HasAbility("Supply Generation - System", emp) || s.HasAbility("Supply Generation - System")))
			{
				foreach (var v in sys.FindSpaceObjects<IMobileSpaceObject>().Where(v => v.Owner == emp))
					v.SupplyRemaining = v.SupplyStorage;
			}
		}

		game.Empires.ParallelSafeForeach(emp =>
		{
			emp.StoredResources = ResourceQuantity.Min(emp.StoredResources, emp.ResourceStorage);// resource spoilage
			emp.Commands.Clear(); // clear empire commands
			emp.Scores[game.TurnNumber] = emp.ComputeScore(null); // update score
		});

		// clear completed orders
		game.Referrables.OfType<IPathfindingOrder>().Where(o => o.KnownTarget == null).ParallelSafeForeach(o => o.IsComplete = true);
		game.Referrables.OfType<IOrder>().Where(o => o.IsComplete).SafeForeach(o => o.Dispose());

		// update known designs
		game.Empires.ParallelSafeForeach(emp =>
		{
			foreach (var design in game.Referrables.OfType<IDesign>())
			{
				if (design.CheckVisibility(emp) >= Visibility.Scanned && !emp.KnownDesigns.Contains(design))
					emp.KnownDesigns.Add(design);
			}
		});

		// clear obsolete sensor ghosts
		// TODO - multithread this somehow
		foreach (var emp in game.Empires)
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
		foreach (var f in game.Galaxy.FindSpaceObjects<Fleet>().ToArray())
		{
			f.Validate();
			f.ShareSupplies();
		}

		// check for victory/defeat
		foreach (var vc in game.Setup.VictoryConditions)
		{
			if (vc is TotalEliminationVictoryCondition || game.TurnNumber > game.Setup.VictoryDelay)
			{
				// find winners
				var winners = new List<Empire>();
				foreach (var emp in game.Empires)
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
					foreach (var emp in game.Empires.Except(winners))
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
		foreach (var w in game.Referrables.OfType<Waypoint>().ToArray())
		{
			if (w.Sector == null)
				w.Dispose();
		}

		foreach (var x in game.Galaxy.FindSpaceObjects<ISpaceObject>().Owned().ToArray())
			x.UpdateEmpireMemories();

		// save off tech levels for computing tech uniqueness next turn
		game.SaveTechLevelsForUniqueness();

		//galaxy.SpaceObjectIDCheck("after cleanup");

		// end of turn scripts
		if (status != null)
			status.Message = "Executing scripts";
		PythonScriptEngine.RunScript<object>(Mod.Current.EndTurnScript);
		if (status != null)
			status.Progress += progressPerOperation;

		// delete any floating space objects that are unused
		//galaxy.SpaceObjectCleanup();

		//galaxy.SpaceObjectIDCheck("at end of turn");

		game.StringValue = null;

		return missingPlrs;
	}

	private static void ProcessColonyIncome(Game galaxy, Colony c)
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
			p.ResourceValue[kvp.Key] -= galaxy.Setup.StandardMiningModel.GetDecay(kvp.Value, p.ResourceValue[kvp.Key]);
		}
	}

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
		var ratio = pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
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
		ratio = pop.Sum(kvp => kvp.Value) / (double)p.MaxPopulation;
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

	private static void ProcessResourceValueChange(Game galaxy, Planet p)
	{
		foreach (var r in Resource.All.Where(r => r.HasValue))
		{
			bool wasFull = p.ResourceValue[r] == galaxy.Setup.MaxPlanetValue;
			bool wasEmpty = p.ResourceValue[r] == galaxy.Setup.MinPlanetValue;
			var modifier =
				p.GetAbilityValue("Planet - Change {0} Value".F(r.Name)).ToInt()
				+ p.GetAbilityValue("Sector - Change {0} Value".F(r.Name)).ToInt()
				+ p.GetAbilityValue("System - Change {0} Value".F(r.Name)).ToInt()
				+ p.GetAbilityValue("Empire - Change {0} Value".F(r.Name)).ToInt();
			p.ResourceValue[r] += modifier;
			p.ResourceValue[r] = p.ResourceValue[r].LimitToRange(galaxy.Setup.MinPlanetValue, galaxy.Setup.MaxPlanetValue);
			if (!wasFull && p.ResourceValue[r] == galaxy.Setup.MaxPlanetValue && p.Owner != null)
				p.Owner.RecordLog(p, "{0}'s {1} have been completely replenished. Its value is at the absolute maximum.".F(p, r), LogMessageType.Generic);
			if (!wasEmpty && p.ResourceValue[r] == galaxy.Setup.MinPlanetValue && p.Owner != null)
				p.Owner.RecordLog(p, "{0} has been stripped dry of {1}. Its value is at the bare minimum.".F(p, r), LogMessageType.Generic);
		}
	}
}
