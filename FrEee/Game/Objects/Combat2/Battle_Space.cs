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


namespace FrEee.Game.Objects.Combat2
{
	public class Battle_Space : INamed, ILocated, IPictorial
	{
		public Battle_Space(Sector location, bool isreplay = false)
		{
			if (location == null)
				throw new Exception("Battles require a sector location.");
			Sector = location;
			//Log = new List<LogMessage>();
			EmpiresArray = (Sector.SpaceObjects.OfType<ICombatSpaceObject>().Select(sobj => sobj.Owner).Where(emp => emp != null).Distinct().ToArray());
			Empires = new Dictionary<Empire, CombatEmpire> { };
			Combatants = new HashSet<ICombatant>(Sector.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(Sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.CombatObjects)));
			CombatObjects = new HashSet<CombatObject>();
			Fleets = new List<Fleet> { };

			foreach (var fleet in Sector.SpaceObjects.OfType<Fleet>())
			{
				Fleets.Add(fleet);
			}
			this.IsReplay = isreplay;
			if (!isreplay)
				ReplayLog = new CombatReplayLog();
            
            
            double stardate = Galaxy.Current.Timestamp;
            int starday = (int)(Galaxy.Current.CurrentTick * 10);                     
            
            int moduloID = (int)(location.StarSystem.ID % 100000);           
            this.battleseed = (int)(moduloID / stardate * 10);
            
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
		/// The combatants in this battle.
		/// </summary>
		public ISet<ICombatant> Combatants { get; private set; }
		public ISet<CombatObject> CombatObjects { get; private set; }

		/// <summary>
		/// the Fleets in this battle
		/// </summary>
		public ICollection<Fleet> Fleets { get; private set; }

		public CombatReplayLog ReplayLog { get; private set; }

        public Sector sectoratStart { get; private set; }

		private const double ticlen = 0.1; //physics tick length
		public const double CommandFrequency = 10;   //new commands (move, new targets etc) are given every 10 ticks.

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


		public Point3d SimNewtonianPhysics(CombatObject comObj)
		{
			comObj.cmbt_accel = (GravMath.accelVector(comObj.cmbt_mass, comObj.cmbt_thrust));

			comObj.cmbt_vel += comObj.cmbt_accel;

			comObj.cmbt_loc += comObj.cmbt_vel * ticlen;

			return comObj.cmbt_loc;
		}

		public Point3d InterpolatePosition(CombatObject comObj, double fractionalTick)
		{
			return comObj.cmbt_loc + comObj.cmbt_vel * fractionalTick;
		}

        private void FirstSetup()
        {
            foreach (Empire empire in EmpiresArray.Where(e => !Empires.ContainsKey(e)))
            {
                Empires.Add(empire, new CombatEmpire());
            }
            foreach (SpaceVehicle ship in Combatants)
            {
                CombatObject comObj = new CombatObject(ship, battleseed);
                CombatObjects.Add(comObj);
                Empires[ship.Owner].ownships.Add(comObj);

                foreach (KeyValuePair<Empire, CombatEmpire> empire in Empires)
                {
                    if (ship.IsHostileTo(empire.Key))
                        empire.Value.hostile.Add(comObj);
                    else if (ship.Owner != empire.Key)
                        empire.Value.friendly.Add(comObj);
                }
            }
        }

		public void SetUpPieces()
		{
            int startrange = 1200;
			Point3d[] startpoints = new Point3d[EmpiresArray.Count()];

			Compass angle = new Compass(360 / EmpiresArray.Count(), false);
			for (int i = 0; i <= EmpiresArray.Count() - 1; i++)
			{
				double angleoffset = angle.Radians * i;
				startpoints[i] = new Point3d(Trig.sides_ab(startrange, angleoffset));

			}

            if (!IsReplay)
                FirstSetup();
			//setup the game peices
			foreach (CombatObject comObj in CombatObjects)
			{

                int empindex = EmpiresArray.IndexOf(comObj.icomobj.Owner);
                comObj.cmbt_loc = new Point3d(startpoints[empindex]); //todo add offeset from this for each ship put in a formation (atm this is just all ships in one position) ie + point3d(x,y,z)
				//thiscomobj.cmbt_face = new Point3d(0, 0, 0); // todo have the ships face the other fleet if persuing or towards the sector they were heading if not persuing. 
                comObj.cmbt_head = new Compass(comObj.cmbt_loc, new Point3d(0, 0, 0));
                comObj.cmbt_att = new Compass(0);
                int speed = ((SpaceVehicle)comObj.icomobj).Speed;
				//thiscomobj.cmbt_vel = Trig.sides_ab(speed, (Trig.angleto(thiscomobj.cmbt_loc, thiscomobj.cmbt_face)));
                comObj.cmbt_vel = Trig.sides_ab(speed, comObj.cmbt_head.Radians);

                comObj.newDice(battleseed);

			}
		}

		public void commandAI(CombatObject comObj)
		{
			//do AI decision stuff.
			//pick a primary target to persue, use AI script from somewhere.  this could also be a formate point. and could be a vector rather than a static point. 
			combatWaypoint wpt = new combatWaypoint(Empires[comObj.icomobj.Owner].hostile[0]);
			comObj.waypointTarget = wpt;
			//pick a primary target to fire apon from a list of enemy within weapon range
			comObj.weaponTarget = new List<CombatObject>();
			comObj.weaponTarget.Add(Empires[comObj.icomobj.Owner].hostile[0]);

		}

		public void helm(CombatObject comObj)
		{
			//rotate ship
			double timetoturn = 0;
			//Compass angletoturn = new Compass(Trig.angleto(comObj.cmbt_face, comObj.waypointTarget.cmbt_loc));
            
            Compass angletoWaypoint = new Compass(comObj.cmbt_loc, comObj.waypointTarget.cmbt_loc);
            Compass angletoturn = new Compass(comObj.cmbt_head.Radians - angletoWaypoint.Radians);
			Point3d vectortowaypoint = comObj.waypointTarget.cmbt_loc - comObj.cmbt_loc;
			if (comObj.lastVectortoWaypoint != null)
				angletoturn.Radians = Trig.angleA(vectortowaypoint - comObj.lastVectortoWaypoint);

			timetoturn = angletoturn.Radians / comObj.maxRotate * ticlen;
            double oneEightytime = 3.14159265 / comObj.maxRotate * ticlen;
			Point3d offsetVector = comObj.waypointTarget.cmbt_vel - comObj.cmbt_vel; // O = a - b
            
            double timetomatchspeed = Trig.distance(comObj.waypointTarget.cmbt_vel, comObj.cmbt_vel) / (comObj.maxfowardThrust / comObj.cmbt_mass); //t = v / a

            double speedDelta = Trig.hypotinuse(offsetVector);
            double distance = Trig.distance(comObj.waypointTarget.cmbt_loc, comObj.cmbt_loc);
            double timetowpt = distance / speedDelta;

			bool thrustToTarget = true;

			//if/when we're going to overshoot teh waypoint
            if (timetowpt <= timetomatchspeed + oneEightytime)
			{
				angletoturn.Degrees += 180; //turn around and thrust the other way
				thrustToTarget = false;
			}

			if (angletoturn.Degrees < 180) //turn to the right
			{
				if (angletoturn.Radians > comObj.maxRotate)
				{
					//comObj.cmbt_face += comObj.Rotate;
                    comObj.cmbt_head.Radians += comObj.maxRotate;
				}
				else
				{
					//comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
                    comObj.cmbt_head.Radians += angletoWaypoint.Radians;
				}
			}
			else
			{
                if (angletoturn.Radians > -comObj.maxRotate)
				{
					//comObj.cmbt_face -= comObj.maxRotate;
                    comObj.cmbt_head.Radians -= comObj.maxRotate;
				}
				else
				{
					//comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
                    comObj.cmbt_head.Radians += angletoWaypoint.Radians;
				}
			}

			//thrust ship using strafe
			if (thrustToTarget) //(if we want to accelerate towards the target, not away from it)
			{
				comObj.cmbt_thrust = Trig.intermediatePoint(comObj.cmbt_loc, comObj.waypointTarget.cmbt_loc, comObj.maxStrafeThrust);
			}
			else
			{
				comObj.cmbt_thrust = Trig.intermediatePoint(comObj.cmbt_loc, comObj.waypointTarget.cmbt_loc, -comObj.maxStrafeThrust);
			}
			//main foward thrust - still needs some work, ie it doesnt know when to turn it off when close to a waypoint.
			double thrustby = 0;
			if (angletoturn.Degrees > 0 && angletoturn.Degrees < 90)
			{
				thrustby = (double)comObj.maxfowardThrust / (angletoturn.Degrees / 0.9);
			}
			else if (angletoturn.Degrees > 270 && angletoturn.Degrees < 360)
			{
				Compass angle = new Compass(-angletoturn.Degrees);
				angle.normalize();
				thrustby = (double)comObj.maxfowardThrust / (angle.Degrees / 0.9);
			}

			//Point3d fowardthrust = new Point3d(comObj.cmbt_face + thrustby);
            Point3d fowardthrust = new Point3d(Trig.sides_ab(thrustby, comObj.cmbt_head.Radians));
			comObj.cmbt_thrust += fowardthrust;
			comObj.lastVectortoWaypoint = vectortowaypoint;
		}

		private void firecontrol(int tic_countr, CombatObject comObj)
		{
			foreach (var weapon in comObj.icomobj.Weapons)
			{
				Vehicle ship = (Vehicle)comObj.icomobj;
				//ship.Weapons
				Component wpn = (Component)weapon;
				if (comObj.weaponTarget.Count() > 0 && wpn.CanTarget(comObj.weaponTarget[0].icomobj))
				{
					//wpn.Attack(comObj.weaponTarget[0].icomobj, (int)Trig.distance(comObj.cmbt_loc, comObj.weaponTarget[0].cmbt_loc), this);
					//combatEvent evnt = new combatEvent(tic_countr, "", comObj, comObj.weaponTarget[0]);
					//replaylog.addEvent(tic_countr, evnt);
					MountedComponentTemplate tmplt = wpn.Template;
					int maxRange = tmplt.WeaponMaxRange;
					int minRange = tmplt.WeaponMinRange;
					int accuracy = tmplt.WeaponAccuracy;
					int damage = tmplt.WeaponDamage;
					if (isinRange(comObj, wpn, comObj.weaponTarget[0]))
					{
						//this function figures out if there's a hit, deals the damage, and creates an event.
						CombatTakeFireEvent target_event = FireWeapon(tic_countr, comObj, wpn, comObj.weaponTarget[0]);
						CombatFireEvent attack_event = new CombatFireEvent(tic_countr, comObj, comObj.cmbt_loc, weapon, target_event);



						if (!IsReplay)
						{
							ReplayLog.Events.Add(target_event);
							ReplayLog.Events.Add(attack_event);
						}

					}
				}
			}
		}

		private void battleloop(int tic_countr)
		{

			foreach (CombatObject comObj in CombatObjects)
			{

				//heading and thrust
				helm(comObj);

				//fire ready weapons.
				firecontrol(tic_countr, comObj);

				//comObj.lastDistancetoWaypoint = Trig.distance(comObj.cmbt_loc, comObj.weaponTarget[0].cmbt_loc);
				SimNewtonianPhysics(comObj);
			}

		}

		public void Resolve()
		{
			//start combat
			Current.Add(this);



			SetUpPieces();


			//unleash the dogs of war!
			bool battleongoing = true;

			int tic_countr = 0;
			double cmdfreq_countr = 0;
			while (battleongoing)
			{
				battleloop(tic_countr);
				if (cmdfreq_countr >= CommandFrequency)
				{
					foreach (CombatObject comObj in CombatObjects)
						commandAI(comObj);
					cmdfreq_countr = 0;
				}

				bool ships_persuing = true;
				bool ships_inrange = true; //ships are in skipdrive interdiction range of enemy ships

				if (!ships_persuing && !ships_inrange)
					battleongoing = false;
				if (tic_countr > 10000)
					battleongoing = false;
				cmdfreq_countr++;
				tic_countr++;
			}

			//end combat



			Current.Remove(this);
			Previous.Add(this);
		}

		private bool isinRange(CombatObject attacker, Component weapon, CombatObject target)
		{
			bool inrange = false;
			var wpninfo = weapon.Template.ComponentTemplate.WeaponInfo;
			double distance_toTarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);


			if (wpninfo.WeaponType == WeaponTypes.DirectFire)
			{
				if (wpninfo.DisplayEffect.GetType() == typeof(Combat.BeamWeaponDisplayEffect))          //beam
				{
					if (distance_toTarget <= wpninfo.MaxRange && distance_toTarget >= wpninfo.MinRange)
						inrange = true;
				}
				else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.ProjectileWeaponDisplayEffect)) //projectile
				{
					double shotspeed = distance_toTarget * ticlen; //speed of bullet when ship is at standstill
					double shotspeed_actual = Trig.distance(attacker.cmbt_loc, Trig.intermediatePoint(attacker.cmbt_loc, target.cmbt_loc, shotspeed));
					double range_actual = shotspeed_actual * ticlen;
					if (range_actual <= wpninfo.MaxRange && range_actual >= wpninfo.MinRange)
						inrange = true;
				}
				else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.SeekerWeaponDisplayEffect))       //seeker
				{
					if (distance_toTarget <= wpninfo.MaxRange && distance_toTarget >= wpninfo.MinRange)
						inrange = true;
				}
			}
			return inrange;
		}

		private CombatTakeFireEvent FireWeapon(int tic, CombatObject attacker, Component weapon, CombatObject target)
		{

			double rangetotarget = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
			ICombatant target_icomobj = target.icomobj;
			//Vehicle defenderV = (Vehicle)target_icomobj;

			if (!weapon.CanTarget(target_icomobj))
				return null;

			// TODO - check range too
			var tohit = Mod.Current.Settings.WeaponAccuracyPointBlank + weapon.Template.WeaponAccuracy + weapon.Container.Accuracy - target_icomobj.Evasion;
			// TODO - moddable min/max hit chances with per-weapon overrides
			if (tohit > 99)
				tohit = 99;
			if (tohit < 1)
				tohit = 1;
            
			//bool hit = RandomHelper.Range(0, 99) < tohit;
            PRNG dice = attacker.getDice();
            bool hit = dice.Range(0, 99) < tohit;
			CombatTakeFireEvent target_event = new CombatTakeFireEvent(tic, target, target.cmbt_loc, hit);

			if (hit)
			{
				var shot = new Combat.Shot(weapon, target_icomobj, (int)rangetotarget);
				//defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
				int damage = shot.Damage;
				combatDamage(target, weapon, damage, attacker.getDice());
				if (target_icomobj.MaxNormalShields < target_icomobj.NormalShields)
					target_icomobj.NormalShields = target_icomobj.MaxNormalShields;
				if (target_icomobj.MaxPhasedShields < target_icomobj.PhasedShields)
					target_icomobj.PhasedShields = target_icomobj.MaxPhasedShields;
				//if (defender.IsDestroyed)
				//battle.LogTargetDeath(defender);

			}
			return target_event;
		}

		private void combatDamage(CombatObject target, Component weapon, int damage, PRNG attackersdice)
		{

			Combat.DamageType damageType = weapon.Template.ComponentTemplate.WeaponInfo.DamageType;
			Vehicle targetV = (Vehicle)target.icomobj;
			if (targetV.IsDestroyed)
				return; //damage; // she canna take any more!

			// TODO - worry about damage types, and make sure we have components that are not immune to the damage type so we don't get stuck in an infinite loop
			int shieldDmg = 0;
			if (targetV.NormalShields > 0)
			{
				var dmg = Math.Min(damage, targetV.NormalShields);
				targetV.NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (targetV.PhasedShields > 0)
			{
				var dmg = Math.Min(damage, targetV.PhasedShields);
				targetV.NormalShields -= dmg;
				damage -= dmg;
				shieldDmg += dmg;
			}
			if (shieldDmg > 0)// && battle != null)
				//battle.LogShieldDamage(this, shieldDmg);
				while (damage > 0 && !targetV.IsDestroyed)
				{
					var comps = targetV.Components.Where(c => c.Hitpoints > 0);
					var armor = comps.Where(c => c.HasAbility("Armor"));
					var internals = comps.Where(c => !c.HasAbility("Armor"));
					var canBeHit = armor.Any() ? armor : internals;
                    var comp = canBeHit.ToDictionary(c => c, c => c.HitChance).PickWeighted(attackersdice);
					damage = comp.TakeDamage(damageType, damage, null);// battle);
				}

			if (targetV.IsDestroyed)
			{
				//battle.LogTargetDeath(this);
				targetV.Dispose();
			}

			// update memory sight
			targetV.UpdateEmpireMemories();

		}

		public System.Drawing.Image Icon
		{
			get { return Combatants.OfType<ISpaceObject>().Largest().Icon; }
		}

		public System.Drawing.Image Portrait
		{
			get { return Combatants.OfType<ISpaceObject>().Largest().Portrait; }
		}
	}
}
