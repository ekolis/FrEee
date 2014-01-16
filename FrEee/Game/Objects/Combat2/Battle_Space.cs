using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
	public class Battle_Space : INamed, ILocated, IPictorial
	{
		/// <summary>
		/// Standard battlespace constructor.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="isreplay"></param>
		public Battle_Space(Sector location)
		{
			if (location == null)
				throw new ArgumentNullException("location", "Battles require a sector location.");
			Initialize(location, location.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(location.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)));
		}

		/// <summary>
		/// Battlespace constructor for unit tests (doesn't need a sector; will create a fake sector).
		/// </summary>
		/// <param name="combatants"></param>
		/// <param name="isreplay"></param>
		public Battle_Space(IEnumerable<ICombatant> combatants)
		{
			Initialize(null, combatants);
		}

		private void Initialize(Sector sector, IEnumerable<ICombatant> combatants)
		{
			if (sector != null)
				Sector = sector;
			else
				Sector = new Sector(new StarSystem(0), new System.Drawing.Point());


			double stardate = Galaxy.Current.Timestamp;
			int starday = (int)(Galaxy.Current.CurrentTick * 10);

			int moduloID = (int)(Sector.StarSystem.ID % 100000);
			this.battleseed = (int)(moduloID / stardate * 10);

			EmpiresArray = combatants.Select(c => c.Owner).Where(emp => emp != null).Distinct().ToArray();
			Empires = new Dictionary<Empire, CombatEmpire> { };

			StartCombatants = new HashSet<ICombatant>();
			ActualCombatants = new HashSet<ICombatant>(combatants);
			//WorkingCombatants = new HashSet<ICombatant>();
			CombatNodes = new HashSet<CombatNode>();

			foreach (ICombatant obj in combatants)
			{
				// TODO - deal with planets in combat
				ICombatant copy = obj.Copy();
				copy.IsMemory = true;
				SpaceVehicle scopy = (SpaceVehicle)copy;

				// copy over the components individually so they can take damage without affecting the starting state
				scopy.Components.Clear();
				foreach (var comp in ((SpaceVehicle)obj).Components)
				{
					var ccopy = comp.Copy();
					ccopy.Container = scopy;
					scopy.Components.Add(ccopy);					
				}

				if (scopy.Owner != obj.Owner)
					scopy.Owner.Dispose(); // don't need extra empires!
				scopy.Owner = obj.Owner;
				StartCombatants.Add(scopy);
				CombatObject comObj = new CombatObject((SpaceVehicle)scopy, (SpaceVehicle)obj, battleseed);
				CombatNodes.Add(comObj);

			}

			Fleets = new List<Fleet> { };

			foreach (var fleet in Sector.SpaceObjects.OfType<Fleet>())
			{
				Fleets.Add(fleet);
			}
			ReplayLog = new CombatReplayLog();


		}

		static Battle_Space()
		{
			Current = new HashSet<Battle_Space>();
			Previous = new HashSet<Battle_Space>();
		}

		/// <summary>
		/// whether or not this is a processing(false) or a replay(true)
		/// </summary>
		public bool IsReplay { get; set; }

		/// <summary>
		/// seed for this battle.
		/// </summary>
		private int battleseed { get; set; }

		/// <summary>
		/// Any battles that are currently ongoing.
		/// This is a collection so we can multithread battle resolution if so desired.
		/// </summary>
		public static ICollection<Battle_Space> Current { get; private set; }

		/// <summary>
		/// Any battles that have completed this turn.
		/// </summary>
		public static ICollection<Battle_Space> Previous { get; private set; }

		/// <summary>
		/// The sector in which this battle took place.
		/// </summary>
		public Sector Sector { get; private set; }

		/// <summary>
		/// The star system in which this battle took place.
		/// </summary>
		public StarSystem StarSystem { get { return Sector.StarSystem; } }

		/// <summary>
		/// The empires engagaed in battle.
		/// </summary>
		public IEnumerable<Empire> EmpiresArray { get; private set; }
		public Dictionary<Empire, CombatEmpire> Empires { get; private set; }

		/// <summary>
		/// The combatants at the start of this battle.
		/// </summary>
		public ISet<ICombatant> StartCombatants { get; private set; }

		/// <summary>
		/// The working list of combatants in this battle.
		/// </summary>
		//public ISet<ICombatant> WorkingCombatants { get; private set; }

		/// <summary>
		/// The REAL combatants objects.
		/// </summary>
		public ISet<ICombatant> ActualCombatants { get; private set; }

		/// <summary>
		/// All combat nodes in this battle, including ships, fighters, seekers, projectiles, etc.
		/// </summary>
		public ISet<CombatNode> CombatNodes { get; private set; }

		/// <summary>
		/// Combat nodes that have an AI attached to them.
		/// This includes ships, bases, units, and seekers.
		/// </summary>
		public IEnumerable<CombatObject> CombatObjects
		{
			get
			{
				return CombatNodes.OfType<CombatObject>();
			}
		}

		/// <summary>
		/// the Fleets in this battle
		/// </summary>
		public ICollection<Fleet> Fleets { get; private set; }

		public CombatReplayLog ReplayLog { get; private set; }

		public Sector sectoratStart { get; private set; }

		/// <summary>
		/// Combat sim temporal resolution.
		/// One SE4 combat round is taken to be one second.
		/// </summary>
		public const int TicksPerSecond = 10;

		/// <summary>
		/// Physics tick length in seconds.
		/// One SE4 combat round is taken to be one second.
		/// </summary>
		public static readonly Fix16 TickLength = (Fix16)1 / (Fix16)TicksPerSecond;

		/// <summary>
		/// Number of seconds between commands. (accelerate, target, etc.)
		/// </summary>
		public static readonly Fix16 CommandFrequencySeconds = 1;

		/// <summary>
		/// Number of ticks between commands. (accelerate, target, etc.)
		/// </summary>
		public static readonly int CommandFrequencyTicks = CommandFrequencySeconds * TicksPerSecond;

		/// <summary>
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		/// 
		public string Name
		{
			get
			{
				if (Sector.SpaceObjects.OfType<StellarObject>().Any())
					return "Battle at " + Sector.SpaceObjects.OfType<StellarObject>().Largest();
				var coords = Sector.Coordinates;
				return "Battle at " + Sector.StarSystem + " sector (" + coords.X + ", " + coords.Y + ")";
			}
		}

		private void FirstSetup()
		{
			foreach (Empire empire in EmpiresArray.Where(e => !Empires.ContainsKey(e)))
			{
				Empires.Add(empire, new CombatEmpire());
			}
			foreach (CombatObject comObj in CombatObjects)
			{
				Empires[comObj.icomobj_WorkingCopy.Owner].ownships.Add(comObj);
			}

		}
		private void ReplaySetup()
		{
			foreach (CombatObject shipObj in CombatObjects)
			{
				shipObj.renewtoStart();
				Empires[shipObj.icomobj_StartCopy.Owner].ownships.Add(shipObj);
			}
		}

		public void SetUpPieces()
		{
			Fix16 startrange = (Fix16)1500; //TODO check longest range weapon. startrange should be half this. (half? shouldn't it be a bit MORE than max range?) - no, since this is a radius.
			Point3d[] startpoints = new Point3d[EmpiresArray.Count()];

			Compass angle = new Compass((Fix16)360 / (Fix16)EmpiresArray.Count(), false);

			for (int i = 0; i <= EmpiresArray.Count() - 1; i++)
			{
				Fix16 angleoffset = angle.Radians * (Fix16)i;
				startpoints[i] = new Point3d(Trig.sides_ab(startrange, angleoffset));
			}

			if (!IsReplay)
				FirstSetup();
			else
				ReplaySetup();
			//setup the game peices
			foreach (CombatObject comObj in CombatObjects)
			{
				foreach (KeyValuePair<Empire, CombatEmpire> empire in Empires)
				{
					var ship = comObj.icomobj_WorkingCopy;
					if (ship.IsHostileTo(empire.Key))
						empire.Value.hostile.Add(comObj);
					else if (ship.Owner != empire.Key)
						empire.Value.friendly.Add(comObj);
				}

				int empindex = EmpiresArray.IndexOf(comObj.icomobj_WorkingCopy.Owner);
				comObj.cmbt_loc = new Point3d(startpoints[empindex]); //todo add offeset from this for each ship put in a formation (atm this is just all ships in one position) ie + point3d(x,y,z)
				//thiscomobj.cmbt_face = new Point3d(0, 0, 0); // todo have the ships face the other fleet if persuing or towards the sector they were heading if not persuing. 
				comObj.cmbt_head = new Compass(comObj.cmbt_loc, new Point3d(0, 0, 0));
				comObj.cmbt_att = new Compass(0);
				Fix16 speed = (Fix16)0;
				if (comObj.icomobj_WorkingCopy is Vehicle)
					speed = ((Fix16)((Vehicle)comObj.icomobj_WorkingCopy).Speed) / (Fix16)2;
				//thiscomobj.cmbt_vel = Trig.sides_ab(speed, (Trig.angleto(thiscomobj.cmbt_loc, thiscomobj.cmbt_face)));
				comObj.cmbt_vel = Trig.sides_ab(speed, comObj.cmbt_head.Radians);

				comObj.newDice(battleseed);

			}
			foreach (CombatObject comObj in CombatObjects)
				commandAI(comObj, 0);
		}

		public void Start()
		{
			//start combat
			Current.Add(this);

			SetUpPieces();
		}

		public void End()
		{
			//end combat
			Current.Remove(this);
			Previous.Add(this);
		}

		/// <summary>
		/// Processes a tick of combat
		/// </summary>
		/// <param name="tick">The tick number</param>
		/// <param name="cmdfreqCounter">Counter to keep track of when the ship AI can issue comamnds.</param>
		/// <returns>True if the battle should continue; false if it should end.</returns>
		public bool ProcessTick(ref int tick, ref int cmdfreqCounter)
		{
			//unleash the dogs of war!
			foreach (var comObj in CombatObjects)
				comObj.debuginfo = ""; //cleardebuginfo txt.

			foreach (var comObj in CombatObjects)
				comObj.helm(); //heading and thrust

			foreach (var comObj in CombatObjects.ToArray())
				firecontrol(tick, comObj); //fire ready weapons.

			foreach (var comObj in CombatObjects)
				SimNewtonianPhysics(comObj); //physicsmove objects.

			if (cmdfreqCounter >= Battle_Space.CommandFrequencyTicks)
			{
				foreach (CombatObject comObj in CombatObjects)
				{
					commandAI(comObj, tick);
				}
				cmdfreqCounter = 0;
			}

			bool ships_persuing = true; // TODO - check if ships are actually pursuing
			bool ships_inrange = true; //ships are in skipdrive interdiction range of enemy ships TODO - check if ships are in range
			bool hostiles = CombatObjects.Any(o => !o.icomobj_WorkingCopy.IsDestroyed && CombatObjects.Any(o2 => !o2.icomobj_WorkingCopy.IsDestroyed && o.icomobj_WorkingCopy.IsHostileTo(o2.icomobj_WorkingCopy.Owner)));

			bool cont;
			if (!ships_persuing && !ships_inrange)
				cont = false;
			else if (!hostiles)
				cont = false;
			else if (tick > 10000) // TODO - put max battle tick in Settings.txt or something
				cont = false;
			else
				cont = true;
			cmdfreqCounter++;
			tick++;
			return cont;
		}

		public void Resolve()
		{
			Start();


			int tick = 0, cmdFreqCounter = 0;
			while (ProcessTick(ref tick, ref cmdFreqCounter))
			{
				// keep on truckin'
			}

			End();
		}


		public Point3d SimNewtonianPhysics(CombatNode comObj)
		{
			if (comObj is CombatObject)
			{
				CombatObject comObjo = (CombatObject)comObj;
				comObjo.cmbt_accel = (GravMath.accelVector(comObjo.cmbt_mass, comObjo.cmbt_thrust));
				comObj.cmbt_vel += comObjo.cmbt_accel;
			}

			comObj.cmbt_loc += comObj.cmbt_vel * (Fix16)TickLength;

			return comObj.cmbt_loc;
		}


		public Point3d InterpolatePosition(CombatNode comObj, double fractionalTick)
		{
			return comObj.cmbt_loc + comObj.cmbt_vel * (Fix16)fractionalTick;
		}

		public void commandAI(CombatObject comObj, int tick)
		{
			//do AI decision stuff.
			//pick a primary target to persue, use AI script from somewhere.  this could also be a formate point. and could be a vector rather than a static point. 
			string comAI = "";
			CombatObject tgtObj;
			if (Empires[comObj.icomobj_WorkingCopy.Owner].hostile.Any())
			{
				tgtObj = Empires[comObj.icomobj_WorkingCopy.Owner].hostile[0];
				combatWaypoint wpt = new combatWaypoint(tgtObj);
				comObj.waypointTarget = wpt;
				//pick a primary target to fire apon from a list of enemy within weapon range
				comObj.weaponTarget = new List<CombatObject>();
				comObj.weaponTarget.Add(Empires[comObj.icomobj_WorkingCopy.Owner].hostile[0]);
			}
			if (IsReplay && tick < 1000)
			{
				List<CombatEvent> evnts = ReplayLog.EventsForObjectAtTick(comObj, tick).ToList<CombatEvent>();
				CombatLocationEvent locevnt = evnts.OfType<CombatLocationEvent>().SingleOrDefault(e => e.Location == comObj.cmbt_loc);
				comAI = "Location ";
				if (locevnt != null)
					comAI += "Does match \r\n";
				else
					comAI += "Not matched \r\n";
			}
			else if (!IsReplay && tick < 1000)
			{
				CombatLocationEvent locevnt = new CombatLocationEvent(tick, comObj, comObj.cmbt_loc);
				ReplayLog.Events.Add(locevnt);
			}

			comObj.debuginfo += comAI;
		}

        private void missilefirecontrol(int tic_countr, CombatObject comObj)
        { 
            Fix16 locdistance = (comObj.cmbt_loc - comObj.weaponTarget[0].cmbt_loc).Length;
            if (locdistance <= comObj.cmbt_vel.Length)//erm, I think?
            {
                CombatTakeFireEvent evnt = comObj.seekertargethit;
                evnt.IsHit = true;
                evnt.Tick = tic_countr;

                CombatWeapon launcherwpn = comObj.weaponList[0];
                Component launcher = launcherwpn.weapon;
                CombatObject target = comObj.weaponTarget[0];
                ICombatant target_icomobj = target.icomobj_WorkingCopy;
                var shot = new Combat.Shot(launcher, target_icomobj, 0);
                //defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
                int damage = shot.Damage;
                combatDamage(tic_countr, target, launcherwpn, damage, comObj.getDice());
                if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
                    target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
                if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
                    target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;
            }
        }

        public void firecontrol(int tic_countr, CombatObject comObj)
        {
            if (comObj.icomobj_WorkingCopy == null)
            {//is a seeker 
                missilefirecontrol(tic_countr, comObj);
            }
            else //is a ship.
            {
                foreach (var weapon in comObj.weaponList)
                {
                    Vehicle ship = (Vehicle)comObj.icomobj_WorkingCopy;
                    //ship.Weapons
                    CombatWeapon wpn = (CombatWeapon)weapon;

                    if (comObj.weaponTarget.Count() > 0 && //if there ARE targets
                        wpn.CanTarget(comObj.weaponTarget[0].icomobj_WorkingCopy) && //if we CAN target 
                        tic_countr >= wpn.nextReload) //if the weapon is ready to fire.
                    {
                        if (wpn.isinRange(comObj, comObj.weaponTarget[0]))
                        {
                            //this function figures out if there's a hit, deals the damage, and creates an event.

                            //first create the event for the target ship
                            CombatTakeFireEvent targets_event = FireWeapon(tic_countr, comObj, wpn, comObj.weaponTarget[0]);
                            //then create teh event for this ship firing on the target
                            CombatFireOnTargetEvent attack_event = new CombatFireOnTargetEvent(tic_countr, comObj, comObj.cmbt_loc, weapon, targets_event);
                            targets_event.fireOnEvent = attack_event;

                            if (!IsReplay)
                            {
                                ReplayLog.Events.Add(targets_event);
                                ReplayLog.Events.Add(attack_event);
                            }

                        }
                    }
                }
                //update any events where this ship has taken fire, and set the location. 
                if (!IsReplay)
                {
                    foreach (CombatEvent comevnt in ReplayLog.EventsForObjectAtTick(comObj, tic_countr))
                    {
                        if (comevnt.GetType() == typeof(CombatTakeFireEvent))
                        {
                            CombatTakeFireEvent takefire = (CombatTakeFireEvent)comevnt;
                            takefire.setLocation(comObj.cmbt_loc);
                        }
                    }
                }
            }
        }

		public CombatTakeFireEvent FireWeapon(int tick, CombatObject attacker, CombatWeapon weapon, CombatObject target)
		{
			var wpninfo = weapon.weapon.Template.ComponentTemplate.WeaponInfo;
			Fix16 rangeForDamageCalcs = (Fix16)0;
			Fix16 rangetotarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
			int targettic = tick;

			//reset the weapon nextReload.
			weapon.nextReload = tick + (int)(weapon.reloadRate * TicksPerSecond); // TODO - round up, so weapons that fire more than 10 times per second don't fire at infinite rate
			
			ICombatant target_icomobj = target.icomobj_WorkingCopy;
			//Vehicle defenderV = (Vehicle)target_icomobj;

			if (!weapon.CanTarget(target_icomobj))
				return null;

			// TODO - check range too
			var tohit = Mod.Current.Settings.WeaponAccuracyPointBlank + weapon.weapon.Template.WeaponAccuracy + weapon.weapon.Container.Accuracy - target_icomobj.Evasion;
			// TODO - moddable min/max hit chances with per-weapon overrides
			if (tohit > 99)
				tohit = 99;
			if (tohit < 1)
				tohit = 1;

			//bool hit = RandomHelper.Range(0, 99) < tohit;
			PRNG dice = attacker.getDice();
			bool hit = dice.Range(0, 99) < tohit;

			CombatTakeFireEvent target_event = null;

            if (weapon.weaponType == "Seeker")
            {
                //Seeker2 iseeker = new Seeker2(attacker.icomobj_WorkingCopy.Owner, weapon.weapon, target.icomobj_WorkingCopy);

                CombatObject seeker = new CombatObject(attacker, weapon, dice.Next(100000));
                seeker.waypointTarget = new combatWaypoint(target);
                seeker.weaponTarget = new List<CombatObject>() { target};
                CombatNodes.Add(seeker);
                if (IsReplay)
                {
                    //read the event
                    target_event = ReplayLog.EventsForObjectAtTick(target, targettic).OfType<CombatTakeFireEvent>().ToList<CombatTakeFireEvent>()[0];
                }
                else
                {
                    //*write* the event
                    target_event = new CombatTakeFireEvent(tick, target, target.cmbt_loc, false);
                    target_event.BulletNode = seeker;
                    seeker.seekertargethit = target_event;
                }
            }
			//for bolt calc, need again for adding to list.
			else if (weapon.weaponType == "Bolt")
			{
                rangeForDamageCalcs = rangeForDamageCalcs_bolt(attacker, weapon, target);
                Fix16 boltTTT = weapon.boltTimeToTarget(attacker, target);
				//set target tick for the future.
				targettic += (int)boltTTT;
				if (IsReplay)
				{
					//read the event
					target_event = ReplayLog.EventsForObjectAtTick(target, targettic).OfType<CombatTakeFireEvent>().ToList<CombatTakeFireEvent>()[0];
				}
				else
				{
					//*write* the event
					target_event = new CombatTakeFireEvent(targettic, target, target.cmbt_loc, hit);
				}

			}
			else //not bolt
			{
				if (IsReplay)
				{ //read the replay... nothing to do if a beam. 
				}
				else
				{ //write the event.
					rangeForDamageCalcs = rangetotarget / (Fix16)1000;
					target_event = new CombatTakeFireEvent(targettic, target, target.cmbt_loc, hit);
				}
			}

			rangeForDamageCalcs = Fix16.Max((Fix16)1, rangeForDamageCalcs); //don't be less than 1.

			if (hit && !target_icomobj.IsDestroyed)
			{
				var shot = new Combat.Shot(weapon.weapon, target_icomobj, (int)rangeForDamageCalcs);
				//defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
				int damage = shot.Damage;
				combatDamage(tick, target, weapon, damage, attacker.getDice());
				if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
					target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
				if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
					target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;
				//if (defender.IsDestroyed)
				//battle.LogTargetDeath(defender);
			}
			return target_event;
		}


        public static Fix16 rangeForDamageCalcs_bolt(CombatObject attacker, CombatWeapon weapon, CombatObject target)
        {
            Fix16 boltTTT = weapon.boltTimeToTarget(attacker, target);
            Fix16 boltSpeed = weapon.boltClosingSpeed(attacker, target);
            Fix16 rThis_distance = boltSpeed * boltTTT;

            Fix16 rMax_distance = boltSpeed * weapon.maxRange; //s * t = d
            Fix16 rMin_distance = boltSpeed * weapon.minRange; //s * t = d

            Fix16 rMax_distance_standstill = weapon.boltSpeed * weapon.maxRange;
            Fix16 rMin_distance_standstill = weapon.boltSpeed * weapon.minRange;

            Fix16 scaler = rMax_distance_standstill / rMax_distance;

            Fix16 rangeForDamageCalcs = rThis_distance * scaler * (Fix16)0.001;
            return rangeForDamageCalcs;
        }

		private void combatDamage(int tick, CombatObject target, CombatWeapon weapon, int damage, PRNG attackersdice)
		{

			Combat.DamageType damageType = weapon.weapon.Template.ComponentTemplate.WeaponInfo.DamageType;
			Vehicle targetV = (Vehicle)target.icomobj_WorkingCopy;
			if (targetV.IsDestroyed)
				return; //damage; // she canna take any more!

			// TODO - worry about damage types, and make sure we have components that are not immune to the damage type so we don't get stuck in an infinite loop

            damage = target.handleShieldDamage(damage);

            target.handleComponentDamage(damage, damageType, attackersdice);

			if (targetV.IsDestroyed)
			{
				//battle.LogTargetDeath(this);
				targetV.Dispose();
                target.deathTick = tick;
                if (!IsReplay)
                {
                    CombatDestructionEvent deathEvent = new CombatDestructionEvent(tick, target, target.cmbt_loc);
                    ReplayLog.Events.Add(deathEvent);
                }
				foreach (KeyValuePair<Empire, CombatEmpire> empireKVP in Empires)
				{
					CombatEmpire empire = empireKVP.Value;
					if (empire.ownships.Contains(target))
						empire.ownships.Remove(target);
					else if (empire.hostile.Contains(target))
						empire.hostile.Remove(target);
					else if (empire.friendly.Contains(target))
						empire.friendly.Remove(target);
				}
			}

			// update memory sight
			targetV.UpdateEmpireMemories();
		}



		public System.Drawing.Image Icon
		{
			get { return StartCombatants.OfType<ISpaceObject>().Largest().Icon; }
		}

		public System.Drawing.Image Portrait
		{
			get { return StartCombatants.OfType<ISpaceObject>().Largest().Portrait; }
		}

		public ICombatant FindStartCombatant(ICombatant c)
		{
			return StartCombatants.SingleOrDefault(c2 => c2.ID == c.ID);
		}

		/// <summary>
		/// I *think* the id will work here now... the ID for the *comObjs* *Should* stay the same. 
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public ICombatant FindWorkingCombatant(ICombatant c)
		{
			//return WorkingCombatants.SingleOrDefault(c2 => c2.ID == c.ID);
			return CombatObjects.SingleOrDefault(c2 => c2.ID == c.ID).icomobj_WorkingCopy;
		}

		public ICombatant FindActualCombatant(ICombatant c)
		{
			return ActualCombatants.SingleOrDefault(c2 => c2.ID == c.ID);
		}
	}
}
