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

using NewtMath.f16;

using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
	public class Battle_Space : INamed, ILocated, IPictorial, IFoggable
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
            else
            {
                this.Sector = location;
                Initialize(location.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(location.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)));
            }
		}
       
        /// <summary>
		/// Battlespace constructor for unit tests (doesn't need a sector; will create a fake sector).
		/// </summary>
		/// <param name="combatants"></param>
		/// <param name="isreplay"></param>
		public Battle_Space(IEnumerable<ICombatant> combatants)
		{
            this.Sector = new Sector(new StarSystem(0), new System.Drawing.Point());
			Initialize(combatants);
            
		}

		private void Initialize(IEnumerable<ICombatant> combatants)
		{
				
			double stardate = Galaxy.Current.Timestamp;
			int starday = (int)(Galaxy.Current.CurrentTick * 10);

			int moduloID = (int)(Sector.StarSystem.ID % 100000);
			this.battleseed = (int)(moduloID / stardate * 10);
            
			EmpiresArray = combatants.Select(c => c.Owner).Where(emp => emp != null).Distinct().ToArray();
			Empires = new Dictionary<Empire, CombatEmpire> { };

			StartCombatants = new Dictionary<long, ICombatant>();
			ActualCombatants = combatants.ToDictionary(c => c.ID);
			//WorkingCombatants = new HashSet<ICombatant>();
			CombatNodes = new HashSet<CombatNode>();
            StartNodes = new HashSet<CombatNode>();
			FreshNodes = new HashSet<CombatNode>();
			DeadNodes = new HashSet<CombatNode>();

			foreach (ICombatant obj in combatants)
			{
                ICombatant copy = obj.CopyAndAssignNewID();//obj.Copy();
				copy.IsMemory = true;

				if (obj is SpaceVehicle)
				{
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
                    StartCombatants.Add(obj.ID, scopy);
					CombatVehicle comObj = new CombatVehicle(scopy, (SpaceVehicle)obj, battleseed, obj.ID);
					StartNodes.Add(comObj);
				}
				else if (obj is Planet)
				{
					Planet pcopy = (Planet)copy;

					// copy over the facilities individually so they can take damage without affecting the starting state
					if (pcopy.Colony != null)
					{
						pcopy.Colony.Facilities.Clear();
						foreach (var f in ((Planet)obj).Colony.Facilities)
						{
							var fcopy = f.Copy();
							pcopy.Colony.Facilities.Add(fcopy);
						}
					}

					if (pcopy.Owner != obj.Owner)
						pcopy.Owner.Dispose(); // don't need extra empires!
					if (pcopy.Colony != null)
						pcopy.Colony.Owner = obj.Owner;
                    StartCombatants.Add(obj.ID, pcopy);
					CombatPlanet comObj = new CombatPlanet(pcopy, (Planet)obj, battleseed, obj.ID);
					StartNodes.Add(comObj);
				}
				else
				{
					Console.Error.WriteLine("Unknown ICombatant type found in " + this + ": " + obj.GetType());
				}
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
		}


        #region fields & properties
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
		public static ICollection<Battle_Space> Previous { get { return Galaxy.Current.Battles; } }

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
		public IDictionary<long, ICombatant> StartCombatants { get; private set; }

		/// <summary>
		/// The REAL combatants objects.
		/// </summary>
		public IDictionary<long, ICombatant> ActualCombatants { get; private set; }

		/// <summary>
		/// All combat nodes in this battle, including ships, fighters, seekers, projectiles, etc.
		/// </summary>
		public ISet<CombatNode> CombatNodes { get; private set; }

        /// <summary>
        /// these are objects at the beginning of the combat.
        /// </summary>
        public ISet<CombatNode> StartNodes { get; private set; }

        /// <summary>
        /// objects go here when they're created during combat, so the replay can create the graphic ojects)
        /// Objects Not Launched or Created during combat should not go here, (ie not objects at combat start)
        /// </summary>
        public ISet<CombatNode> FreshNodes { get; set; }

        /// <summary>
        /// objects go here to die. (this is so replay can clean up the graphic objects)
        /// ONLY OBJECTS CREATED *DURING* combat should go here. (or Replaysetup() won't have all the objects. humn, how' we going to diferentiate for launchable cargo, ie fighters?)
        /// Maybe we need StartNodes as well, and have Replaysetup() go from that. and it won't matter.
        /// </summary>
        public ISet<CombatNode> DeadNodes { get; set; }

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
		/// Combat nodes that represent vehicles.
		/// This includes ships, bases, and units.
		/// </summary>
        public IEnumerable<CombatVehicle> CombatVehicles
        {
            get
            {
                return CombatNodes.OfType<CombatVehicle>();
            }
        }

		/// <summary>
		/// Combat nodes that represent controllable objects.
		/// This includes vehicles and planets.
		/// </summary>
		public IEnumerable<CombatControlledObject> ControlledCombatObjects
		{
			get
			{
				return CombatNodes.OfType<CombatControlledObject>();
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
		/// physics gets calculated every tick. 
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

        private int tOC = 0;
        private int tempObjCounter
        {
            get
            {
#if DEBUG
                Console.WriteLine("TempObjCounter :" + tOC);
#endif
                return tOC++;            
            }
        }

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

        #endregion

        private void FirstSetup()
		{
#if DEBUG
            Console.WriteLine("Beginning Processing Setup");
            Console.WriteLine("Adding Empires");
#endif
			foreach (Empire empire in EmpiresArray.Where(e => !Empires.ContainsKey(e)))
			{
				Empires.Add(empire, new CombatEmpire());
#if DEBUG
                Console.Write(".");
#endif
			}
#if DEBUG
            Console.WriteLine("Done");
#endif
#if DEBUG
            Console.WriteLine("Creating IFF lists");
#endif
            foreach (CombatObject comObj in StartNodes)
			{
                if (comObj is CombatControlledObject)
                {
#if DEBUG
                    Console.WriteLine("Getting Empire for this ship");
#endif
                    Empire thisemp = ((CombatControlledObject)comObj).StartCombatant.Owner;
#if DEBUG
                    Console.WriteLine("Done");
#endif
                    Empires[thisemp].ownships.Add(comObj);
#if DEBUG
                    Console.Write(".");
#endif
                }
			}
#if DEBUG
            Console.WriteLine("Done Initial Setup");
#endif
            CombatNodes = StartNodes;
		}

		private void ReplaySetup()
		{
#if DEBUG
            Console.WriteLine("Beginning Replay Setup");

            Console.WriteLine("Creating IFF lists");
#endif
            foreach (CombatEmpire emp in Empires.Values)
            {
                
            }
            foreach (CombatObject comObj in StartNodes)
			{
                comObj.renewtoStart();
				if (comObj is CombatControlledObject)
                {
#if DEBUG
                    Console.WriteLine("Getting Empire for this ship");
#endif
					Empire thisemp = ((CombatControlledObject)comObj).StartCombatant.Owner;
#if DEBUG
                    Console.WriteLine("Done");
#endif
                    Empires[thisemp].ownships.Add(comObj);
#if DEBUG
                    Console.Write(".");
#endif
                }
#if DEBUG
                Console.Write(".");
#endif
			}
#if DEBUG
            Console.WriteLine("Done");
#endif
            CombatNodes = StartNodes;
		}

		public void SetUpPieces()
		{
#if DEBUG
            Console.WriteLine("Setting up combat Objects");
#endif
            tOC = 0; //reset temp object counter.
			Fix16 startrange = (Fix16)1500; //TODO check longest range weapon. startrange should be half this. (half? shouldn't it be a bit MORE than max range?) - no, since this is a radius.
			PointXd[] startpoints = new PointXd[EmpiresArray.Count()];

			Compass angle = new Compass((Fix16)360 / (Fix16)EmpiresArray.Count(), false);

			for (int i = 0; i <= EmpiresArray.Count() - 1; i++)
			{
				Fix16 angleoffset = angle.Radians * (Fix16)i;
				startpoints[i] = new PointXd(Trig.sides_ab(startrange, angleoffset));
			}

			if (!IsReplay)
				FirstSetup();
			else
				ReplaySetup();
			//setup the game peices
            foreach (var ccobj in ControlledCombatObjects)
			{
				foreach (KeyValuePair<Empire, CombatEmpire> empire in Empires)
				{
					var ship = ccobj.WorkingObject;
					if (ship is ICombatant)
					{
						var c = (ICombatant)ship;
						if (c.IsHostileTo(empire.Key))
							empire.Value.hostile.Add(ccobj);
						else if (c.Owner != empire.Key)
							empire.Value.friendly.Add(ccobj);
					}
				}

				int empindex = EmpiresArray.IndexOf(ccobj.StartCombatant.Owner);
                
                ccobj.empire = Empires[ccobj.StartCombatant.Owner];
                ccobj.strategy = new StragegyObject_Default();

				ccobj.cmbt_loc = new PointXd(startpoints[empindex]); //todo add offeset from this for each ship put in a formation (atm this is just all ships in one position) ie + PointXd(x,y,z)
				//thiscomobj.cmbt_face = new PointXd(0, 0, 0); // todo have the ships face the other fleet if persuing or towards the sector they were heading if not persuing. 
				ccobj.cmbt_head = new Compass(ccobj.cmbt_loc, new PointXd(0, 0, 0));
				ccobj.cmbt_att = new Compass(0);
				Fix16 speed = (Fix16)0;
				if (ccobj.WorkingObject is Vehicle)
					speed = ((Fix16)((Vehicle)ccobj.WorkingObject).Speed) / (Fix16)2;
				//thiscomobj.cmbt_vel = Trig.sides_ab(speed, (Trig.angleto(thiscomobj.cmbt_loc, thiscomobj.cmbt_face)));
				ccobj.cmbt_vel = Trig.sides_ab(speed, ccobj.cmbt_head.Radians);

				ccobj.newDice(battleseed);

			}
            foreach (var ccobj in ControlledCombatObjects)
				commandAI(ccobj, 0);

#if DEBUG
            Console.WriteLine("Done setting up combat Objects");
            Console.WriteLine("CNodes :" + CombatNodes.Count());
            Console.WriteLine("FNodes :" + FreshNodes.Count());
            Console.WriteLine("DNodes :" + DeadNodes.Count());        
#endif
		}

		public void Start()
		{
			//start combat
			Current.Add(this);

			SetUpPieces();
		}

		public void End(int tick)
		{
			//end combat
            ReplayLog.Events.Add(new CombatEndBattleEvent(tick));
			Current.Remove(this);
			Previous.Add(this);

			// delete leftover seekers that were en route when combat ended
			foreach (var seeker in CombatNodes.OfType<CombatSeeker>().ToArray())
				CombatNodes.Remove(seeker);
            IsReplay = true;
		}

		/// <summary>
		/// Processes a tick of combat (processing only)
		/// </summary>
		/// <param name="tick">The tick number</param>
		/// <param name="cmdfreqCounter">Counter to keep track of when the ship AI can issue comamnds.</param>
		/// <returns>True if the battle should continue; false if it should end.</returns>
		public bool ProcessTick(ref int tick, ref int cmdfreqCounter)
		{
#if DEBUG
            Console.WriteLine("Processing: ");
            Console.WriteLine("Tick: " + tick.ToString());
#endif
			//unleash the dogs of war!
			foreach (var comObj in CombatObjects)
				comObj.debuginfo = ""; //cleardebuginfo txt.

			foreach (var comObj in CombatObjects.ToArray())
            {				
#if DEBUG
                Console.WriteLine("comObj.helm " + comObj.strID);
#endif
                comObj.helm(); //heading and thrust
            }

			foreach (var comObj in CombatObjects.ToArray())
				firecontrol(tick, comObj); //fire ready weapons.

			foreach (var comObj in CombatObjects)
				SimNewtonianPhysics(comObj); //physicsmove objects.

			if (cmdfreqCounter >= Battle_Space.CommandFrequencyTicks)
			{
                foreach (CombatControlledObject ccobj in ControlledCombatObjects)
				{
					commandAI(ccobj, tick);
				}
				cmdfreqCounter = 0;
			}

            foreach (CombatNode comNod in FreshNodes.ToArray())
            {                
                CombatNodes.Add(comNod);
                FreshNodes.Remove(comNod);
#if DEBUG
                Console.WriteLine("adding " + comNod.strID + " to combatNodes from fresh");
#endif
            }
            foreach (CombatNode comNod in DeadNodes.ToArray())
            {
                CombatNodes.Remove(comNod);
                DeadNodes.Remove(comNod);
                if (comNod is CombatObject)
                {
                    
                    foreach (CombatEmpire emp in Empires.Values)
                    {
                        emp.removeComObj((CombatObject)comNod);
                    }
                }
#if DEBUG
                Console.WriteLine("disposing obj from deadNodes");
#endif
                
            }

			bool ships_persuing = true; // TODO - check if ships are actually pursuing
			bool ships_inrange = true; //ships are in skipdrive interdiction range of enemy ships TODO - check if ships are in range
            //TODO: check for alive missiles and bullets.
			bool hostiles = ControlledCombatObjects.Any(o => !o.WorkingCombatant.IsDestroyed && ControlledCombatObjects.Any(o2 => !o2.WorkingCombatant.IsDestroyed && o.WorkingCombatant.IsHostileTo(o2.WorkingCombatant.Owner)));

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

			End(tick);
		}


		public PointXd SimNewtonianPhysics(CombatNode comObj)
		{
			if (comObj is CombatObject)
			{
				CombatObject comObjo = (CombatObject)comObj;
                comObjo.cmbt_accel = (NMath.accelVector(comObjo.cmbt_mass, comObjo.cmbt_thrust));
                comObjo.cmbt_vel += comObjo.cmbt_accel / TicksPerSecond;
#if DEBUG                
                Console.WriteLine(comObj.strID + " Acc " + comObjo.cmbt_accel);
#endif
			}

            comObj.cmbt_loc += comObj.cmbt_vel / TicksPerSecond;
#if DEBUG
            Console.WriteLine(comObj.strID + " Loc " + comObj.cmbt_loc);

            Console.WriteLine(comObj.strID + " Vel " + comObj.cmbt_vel);
#endif
			return comObj.cmbt_loc;
		}


        /// <summary>
        /// this is to make animation/ship movement smoother, and should not affect the physics in any way.
        /// </summary>
        /// <param name="comObj"></param>
        /// <param name="fractionalTick"></param>
        /// <returns></returns>
		public PointXd InterpolatePosition(CombatNode comObj, double fractionalTick)
		{
			return comObj.cmbt_loc + comObj.cmbt_vel * (Fix16)fractionalTick;
		}

        public void commandAI(CombatControlledObject ccobj, int battletick)
        {
            //do AI decision stuff.
            //pick a primary target to persue, use AI script from somewhere.  this could also be a formate point. and could be a vector rather than a static point. 
            if (ccobj.WorkingObject != null)
            {

                int hp1 = ccobj.WorkingCombatant.Hitpoints;
                int hp2 = ccobj.WorkingObject.Hitpoints;
                int hp3 = ccobj.WorkingObject.HullHitpoints;
                int hp4 = ccobj.StartCombatant.Hitpoints;
                int hp5 = ccobj.StartCombatant.HullHitpoints;
#if DEBUG
                Console.WriteLine(ccobj.WorkingCombatant.Name);
                Console.WriteLine("hp = " + hp1 + ", " + hp2 + ", " + hp3 + ", " + hp4 + ", " + hp5);
                
                Vehicle startV = (Vehicle)ccobj.StartCombatant;
                foreach (Component comp in startV.Components)
                {
                    Console.WriteLine(comp.Name + " container " + comp.Container);
                }
#endif
                string comAI = "";
                //CombatObject tgtObj;
                if (Empires[ccobj.WorkingObject.Owner].hostile.Any())
                {
                    /*old
                    tgtObj = Empires[ccobj.WorkingObject.Owner].hostile[0];
                    combatWaypoint wpt = new combatWaypoint(tgtObj);
                    ccobj.waypointTarget = wpt;
                    //pick a primary target to fire apon from a list of enemy within weapon range
                    ccobj.weaponTarget = new List<CombatObject>();
                    ccobj.weaponTarget.Add(Empires[ccobj.WorkingObject.Owner].hostile[0]);
                     * */
                    ccobj.calcWaypoint();
                    ccobj.calcWpnTarget();
                }
                else 
                {
#if DEBUG
                    Console.WriteLine(ccobj.WorkingCombatant.Name + " Has no hostile Targets");
#endif
                }
                if (IsReplay && battletick < 1000)
                {
                    List<CombatEvent> evnts = ReplayLog.EventsForObjectAtTick(ccobj, battletick).ToList<CombatEvent>();
                    var locevnts = evnts.OfType<CombatLocationEvent>().Where(e => e.Object == ccobj && e.Tick == battletick);
                    comAI = "Location ";
                    if (locevnts.Any() && locevnts.All(le => le.Location == ccobj.cmbt_loc))
                        comAI += "Does match \r\n";
                    else
                        comAI += "Not matched \r\n";
                }
                else if (!IsReplay && battletick < 1000)
                {
                    CombatLocationEvent locevnt = new CombatLocationEvent(battletick, ccobj, ccobj.cmbt_loc);
                    ReplayLog.Events.Add(locevnt);
                }

                ccobj.debuginfo += comAI;
            }
        }

        private void missilefirecontrol(int battletick, CombatSeeker comSek)
        {
            //Fix16 locdistance = Trig.distance(comSek.cmbt_loc, comSek.weaponTarget[0].cmbt_loc);
            //PointXd vectortowaypoint = comSek.cmbt_loc - comSek.waypointTarget.cmbt_loc;
            //Fix16 locdistance2 = vectortowaypoint.Length;
            PointXd vectortowaypoint = comSek.cmbt_loc - comSek.weaponTarget[0].cmbt_loc;
            Fix16 locdistance = vectortowaypoint.Length;
#if DEBUG
            //Console.WriteLine("firecontrol for: " + comSek.strID);
            //Console.WriteLine(comSek.strID + " Targeting " + comSek.weaponTarget[0].strID);
            Console.WriteLine(comSek.strID + " Range " + locdistance + "to " + comSek.weaponTarget[0].strID);
            //Console.WriteLine("Rangettgt " + locdistance2);
            //Console.WriteLine("Rangettgt " + locdistance3);
#endif
            
            if (locdistance <= comSek.cmbt_vel.Length)//erm, I think? (if we're as close as we're going to get in one tick) could screw up at high velocities.
            {
#if DEBUG
                Console.WriteLine("ProxDetonation!");
#endif
                if (!IsReplay)
                {
                    CombatTakeFireEvent evnt = comSek.seekertargethit;
                    evnt.IsHit = true;
                    evnt.Tick = battletick;
                }
                Component launcher = comSek.launcher.weapon;
                CombatObject target = comSek.weaponTarget[0];
                if (target is CombatControlledObject) //TODO handle seekers and other objects as seeker targets.
                {
					CombatControlledObject ccTarget = (CombatControlledObject)target;
                    var target_icomobj = ccTarget.WorkingObject;
                    var shot = new Combat.Shot(launcher, target_icomobj, 0);
                    //defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
                    int damage = shot.Damage;
                    combatDamage(battletick, target, comSek.launcher, damage, comSek.getDice());
                    if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
                        target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
                    if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
                        target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;

                }

                DeadNodes.Add(comSek);
                CombatNodes.Remove(comSek);                
            }
            else if (battletick > comSek.deathTick)
            {
#if DEBUG
                Console.WriteLine("Out of Juice!");
#endif
                DeadNodes.Add(comSek);
                CombatNodes.Remove(comSek);
            }
        }

		/// <summary>
		/// TODO - move fire control to CombatObject as an abstract method
		/// </summary>
		/// <param name="tic_countr"></param>
		/// <param name="comObj"></param>
        public void firecontrol(int tic_countr, CombatObject comObj)
        {

            if (comObj is CombatSeeker)
            {
				//is a seeker 
                missilefirecontrol(tic_countr, (CombatSeeker)comObj);
            }
            else if (comObj is CombatControlledObject)
            {

				//is a ship, base, unit, or planet
				CombatControlledObject ccobj = (CombatControlledObject)comObj;
#if DEBUG
            Console.WriteLine("firecontrol for: " + ccobj.WorkingCombatant.Name);
#endif

                List<CombatWeapon> allweapons = ccobj.Weapons.ToList();

                for (int i = 0; i < ccobj.strategy.numberOfTargetStrategies(); i++)
                {
                    CombatObject targetObject = ccobj.strategy.targetforgroup(ccobj, i);
                    List<int> wpnindexesthistarget = ccobj.strategy.weaponslists[i].Keys.ToList();
                    List<CombatWeapon> wpnsforthistarget = new List<CombatWeapon>();
#if DEBUG
                    Console.WriteLine("Target: " + targetObject.strID);
#endif
                    foreach (int wpndex in wpnindexesthistarget)
                    {
                        if ( wpndex < allweapons.Count)//handling a case where a strategy might have more wpns than this ship does. 
                            wpnsforthistarget.Add(allweapons[wpndex]);
                    }

                    foreach (CombatWeapon wpn in wpnsforthistarget)
                    {
#if DEBUG
                        Console.WriteLine("Weapon: " + wpn.weapon.Name);
#endif
                        //var wpn = ccobj.Weapons.ToList()[i];
                        ICombatant ship = (ICombatant)ccobj.WorkingObject;

                        if (comObj.weaponTarget.Count() > 0 && //if there ARE targets
                            wpn.CanTarget(targetObject.WorkingObject) && //if we CAN target 
                            tic_countr >= wpn.nextReload) //if the weapon is ready to fire.
                        {
                            if (wpn.isinRange(comObj, targetObject))//weaponTarget[i] should match the 
                            {
                                //this function figures out if there's a hit, deals the damage, and creates an event.
#if DEBUG
                                Console.WriteLine("Fire Weapon!");

                                if(wpn.weapon.Container == null)
                                    Console.WriteLine(wpn.weapon.Name + " Has Null Container!!!");
                                else
                                    Console.WriteLine(wpn.weapon.Name + " Has Container.");
#endif
                                //first create the event for the target ship
                                CombatTakeFireEvent targets_event = FireWeapon(tic_countr, comObj, wpn, targetObject);
                                //then create teh event for this ship firing on the target
                                CombatFireOnTargetEvent attack_event = new CombatFireOnTargetEvent(tic_countr, comObj, comObj.cmbt_loc, wpn, targets_event);
                                targets_event.fireOnEvent = attack_event;

                                if (!IsReplay)
                                {
                                    ReplayLog.Events.Add(targets_event);
                                    ReplayLog.Events.Add(attack_event);
                                }

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

        int to_hit(Sector sector, CombatObject attacker, CombatWeapon weapon, CombatObject target)
        {

            //// TODO - check range too
            //var tohit =
            //    Mod.Current.Settings.WeaponAccuracyPointBlank // default weapon accuracy at point blank range
            //    + weapon.weapon.Template.WeaponAccuracy // weapon's intrinsic accuracy modifier
            //    + weapon.weapon.Container.Accuracy // firing ship's accuracy modifier
            //    - target_icomobj.Evasion // target's evasion modifier
            //    - Sector.GetAbilityValue(target.WorkingObject.Owner, "Sector - Sensor Interference").ToInt() // sector evasion modifier
            //    + Sector.GetAbilityValue(attacker.WorkingObject.Owner, "Combat Modifier - Sector").ToInt() // generic combat bonuses
            //    - Sector.GetAbilityValue(target.WorkingObject.Owner, "Combat Modifier - Sector").ToInt()
            //    + Sector.StarSystem.GetAbilityValue(attacker.WorkingObject.Owner, "Combat Modifier - System").ToInt()
            //    - Sector.StarSystem.GetAbilityValue(target.WorkingObject.Owner, "Combat Modifier - System").ToInt()
            //    + attacker.WorkingObject.Owner.GetAbilityValue("Combat Modifier - Empire").ToInt()
            //    - target.WorkingObject.Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
            //// TODO - moddable min/max hit chances with per-weapon overrides
            Vehicle vehicle = (Vehicle)attacker.WorkingObject;
            int wpn_Accu_blankrng = Mod.Current.Settings.WeaponAccuracyPointBlank;
            int wpn_AccuMod = weapon.weapon.Template.WeaponAccuracy; // weapon's intrinsic accuracy modifier
            int atkr_AccuMod = vehicle.Accuracy;//weapon.weapon.Container.Accuracy; // firing ship's accuracy modifier
            int tgt_EvdMod = target.WorkingObject.Evasion; // target's evasion modifier
            int tgt_SectEvdMod = Sector.GetAbilityValue(target.WorkingObject.Owner, "Sector - Sensor Interference").ToInt(); // sector evasion modifier
            int atkr_SectComMod = Sector.GetAbilityValue(attacker.WorkingObject.Owner, "Combat Modifier - Sector").ToInt(); // generic combat bonuses
			int tgt_SectComMod =  Sector.GetAbilityValue(target.WorkingObject.Owner, "Combat Modifier - Sector").ToInt();
            int atkr_SysComMod = Sector.StarSystem.GetAbilityValue(attacker.WorkingObject.Owner, "Combat Modifier - System").ToInt();
			int tgt_SysComMod = Sector.StarSystem.GetAbilityValue(target.WorkingObject.Owner, "Combat Modifier - System").ToInt();
		    int atkr_EmpComMod = attacker.WorkingObject.Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
            int tgt_EmpComMod = target.WorkingObject.Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();

            int total =
                wpn_Accu_blankrng
                + wpn_AccuMod
                + atkr_AccuMod
                - tgt_EvdMod
                - tgt_SectEvdMod
                + atkr_SectComMod
                - tgt_SectComMod
                + atkr_SysComMod
                - tgt_SysComMod
                + atkr_EmpComMod
                - tgt_EmpComMod;

            return total;

        }

		public CombatTakeFireEvent FireWeapon(int battletick, CombatObject attacker, CombatWeapon weapon, CombatObject target)
		{
			var wpninfo = weapon.weapon.Template.ComponentTemplate.WeaponInfo;
			Fix16 rangeForDamageCalcs = (Fix16)0;
			Fix16 rangetotarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
			int targettic = battletick;

			//reset the weapon nextReload.
			weapon.nextReload = battletick + (int)(weapon.reloadRate * TicksPerSecond); // TODO - round up, so weapons that fire more than 10 times per second don't fire at infinite rate
			
			var target_icomobj = target.WorkingObject;
			//Vehicle defenderV = (Vehicle)target_icomobj;
            Vehicle attkVeh = (Vehicle)attacker.WorkingObject;
			if (!weapon.CanTarget(target_icomobj))
				return null;
            int tohit = this.to_hit(Sector, attacker, weapon, target);

			if (tohit > 99)
				tohit = 99;
			if (tohit < 1)
				tohit = 1;
            if (attkVeh.HasAbility("Weapons Always Hit"))//weapon.weapon.Container.HasAbility("Weapons Always Hit"))
				tohit = 100;

			//bool hit = RandomHelper.Range(0, 99) < tohit;
			PRNG dice = attacker.getDice();
			bool hit = dice.Range(0, 99) < tohit;

			CombatTakeFireEvent target_event = null;

            if (weapon.weaponType == "Seeker")
            {
                
                //create seeker and node.
                CombatSeeker seeker = new CombatSeeker(attacker, weapon, -tempObjCounter);
                seeker.waypointTarget = new combatWaypoint(target);
                seeker.weaponTarget = new List<CombatObject>() { target};
                seeker.deathTick = battletick * TicksPerSecond + weapon.maxRange_time;
                seeker.cmbt_head = attacker.cmbt_head;
                seeker.cmbt_att = attacker.cmbt_att;
                FreshNodes.Add(seeker);
#if DEBUG
                Console.WriteLine("Seeker " + seeker.strID + " added to FreshNodes");
#endif
				foreach (var emp in Empires.Values)
				{
					if (emp.ownships.Contains(attacker))
						emp.ownships.Add(seeker);
					if (emp.friendly.Contains(attacker))
						emp.friendly.Add(seeker);
					if (emp.neutral.Contains(attacker))
						emp.neutral.Add(seeker);
					if (emp.hostile.Contains(attacker))
						emp.hostile.Add(seeker);
				}

                if (IsReplay)
                {
                    //read the event
                    target_event = ReplayLog.EventsForObjectAtTick(target, targettic).OfType<CombatTakeFireEvent>().ToList<CombatTakeFireEvent>()[0];
                    target_event.BulletNode = seeker;
                }
                else
                {
                    //*write* the event
                    target_event = new CombatTakeFireEvent(battletick, target, target.cmbt_loc, false);
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

                    //because bullets don't need to be created during processing
                    Fix16 rThis_distance = (target_event.Location - target_event.fireOnEvent.Location).Length;
                    PointXd bulletVector = Trig.intermediatePoint(attacker.cmbt_loc, target_event.Location, rThis_distance);
                    if (!target_event.IsHit) //jitter it!
                    {
                        // TODO - take into account firing ship's accuracy and target's evasion
                        int accuracy = target_event.fireOnEvent.Weapon.weapon.Template.WeaponAccuracy;
                        int jitterAmount = 0;
                        if (accuracy < 50)
                            jitterAmount = (int)System.Math.Pow(50 - accuracy, 2) / 50;
                        if (jitterAmount < 5)
                            jitterAmount = 5;
                        if (jitterAmount > 30)
                            jitterAmount = 30;
                        //do *NOT* use ship prng here!!!! (since this is not done during normal processing, it'll cause differences, use any rand)
                        Compass jitter = new Compass(RandomHelper.Range(-jitterAmount, jitterAmount), false);
                        Compass bulletCompass = bulletVector.Compass;
                        Compass offsetCompass = bulletCompass + jitter;
                        bulletVector = offsetCompass.Point(bulletVector.Length);
                    }
                    CombatNode bullet = new CombatNode(attacker.cmbt_loc, bulletVector, -tempObjCounter, "BLT");
                    target_event.BulletNode = bullet;
                    FreshNodes.Add(bullet);
                    if (target_event.IsHit)
                    {
                        bullet.deathTick = target_event.Tick;
                    }
                    else
                    {
                        bullet.deathTick = battletick + target_event.fireOnEvent.Weapon.maxRange;
                    }
				}
				else
				{
					//*write* the event
					target_event = new CombatTakeFireEvent(targettic, target, target.cmbt_loc, hit);
                    int nothing = -tempObjCounter; //increase it just so processing has the same number of tempObjects created as replay will. 
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
				combatDamage(battletick, target, weapon, damage, attacker.getDice());
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
            Fix16 boltSpeed = weapon.boltClosingSpeed(attacker, target); // m/s
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

			var targetV = target.WorkingObject;
			if (targetV.IsDestroyed)
				return; //damage; // she canna take any more!

			targetV.TakeDamage(damageType, damage, attackersdice);

			if (targetV.IsDestroyed)
			{
#if DEBUG
                Console.WriteLine(target.strID + " is destroyed!");
#endif
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
		}



		public System.Drawing.Image Icon
		{
			get { return StartCombatants.Values.OfType<ISpaceObject>().Largest().Icon; }
		}

		public System.Drawing.Image Portrait
		{
			get { return StartCombatants.Values.OfType<ISpaceObject>().Largest().Portrait; }
		}

		public ICombatant FindStartCombatant(ICombatant c)
		{
			return StartCombatants[c.ID];
		}

		public ICombatant FindWorkingCombatant(ICombatant c)
		{
			foreach (var n in CombatNodes.OfType<CombatControlledObject>())
			{
				if (n.StartCombatant == c)
					return n.WorkingCombatant;
				if (n.WorkingCombatant == c)
					return c;
			}
			throw new ArgumentException("Can't find working combatant for " + c + ".");
		}

		public ICombatant FindActualCombatant(ICombatant c)
		{
			return ActualCombatants[c.ID];
		}

		/// <summary>
		/// Battles are visible to anyone who has a combatant in them; otherwise they're unknown.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			// TODO - "share combat logs" treaties?
			return ActualCombatants.Values.Any(c => c.Owner == emp) ? Visibility.Visible : Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);
			if (vis < Visibility.Fogged)
				Dispose();
			else
			{
				// start combatants are memories but need to be redacted anyway
				foreach (var memory in StartCombatants.Values)
					memory.Redact(emp);
			}
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp
		{
			get;
			set;
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			Current.Remove(this);
			Previous.Remove(this);
			IsDisposed = true;
		}

		public Empire Owner
		{
			get { return null; }
		}
	}
}
