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
    public class Battle_Space : INamed, ILocated
    {
        public Battle_Space(Sector location, combatReplayLog replaylog = null)
		{
			if (location == null)
				throw new Exception("Battles require a sector location.");
			Sector = location;
			//Log = new List<LogMessage>();
            EmpiresArray = (Sector.SpaceObjects.OfType<ICombatSpaceObject>().Select(sobj => sobj.Owner).Where(emp => emp != null).Distinct().ToArray());
			Combatants = new HashSet<ICombatObject>(Sector.SpaceObjects.OfType<ICombatObject>().Where(o => o.Owner != null).Union(Sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.CombatObjects)));
            
            foreach (var fleet in Sector.SpaceObjects.OfType<Fleet>())
            {
                fleets.Add(fleet);
            }
            this.replaylog = replaylog;
		}

        static Battle_Space()
        {
            Current = new HashSet<Battle_Space>();
            Previous = new HashSet<Battle_Space>();
        }

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
        private Dictionary<Empire, EmpireinCombat> Empires = new Dictionary<Empire,EmpireinCombat>{ };

		/// <summary>
		/// The combatants in this battle.
		/// </summary>
		public ISet<ICombatObject> Combatants { get; private set; }
        public ISet<CombatObj> comObjs { get; set; }
        /// <summary>
        /// the Fleets in this battle
        /// </summary>
        private List<Fleet> fleets = new List<Fleet> { };

        private combatReplayLog replaylog;

        private List<IMobileSpaceObject> combatgroups;

        private double ticlen = 0.1; //physics tick length
        private double comdfreq = 10;   //new commands (move, new targets etc) are given every 10 ticks.

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

        private int startrange = 120;

        /// <summary>
        /// can happen as often as the renderer can handle it. happens between renderPhysTic() 
        /// </summary>
        public void renderPhysTic()
        { }


        /// <summary>
        /// this should happen as fast as possible when processing, but be every ticlen when rendering. 
        /// </summary>
        public void simPhysTic(double tic)
        {
            foreach (CombatObj comObj in comObjs)
            {
                //move ship
                comObj.cmbt_loc += comObj.cmbt_vel * ticlen;
            }
        }


        private void setupPices()
        {
            Point3d[] startpoints = new Point3d[EmpiresArray.Count()];

            int angle = 0;
            for (int i = 0; i <= EmpiresArray.Count(); i++)
            {
                startpoints[i] = new Point3d(Trig.sides_ab(startrange, angle));
                startpoints[i].Z = 0;
            }

            //setup the game peices
            foreach (Empire empire in EmpiresArray)
            {
                Empires.Add(empire, null);
            }
            foreach (SpaceVehicle ship in Combatants)
            {
                CombatObj thiscomobj = new CombatObj(ship);
                int empindex = EmpiresArray.GetIndex(ship.Owner);
                thiscomobj.cmbt_loc = new Point3d(startpoints[empindex]); //todo add offeset from this for each ship put in a formation (atm this is just all ships in one position) ie + point3d(x,y,z)
                thiscomobj.cmbt_face = new Point3d(0, 0, 0); // todo have the ships face the other fleet if persuing or towards the sector they were heading if not persuing. 
                int speed = ship.Speed;
                thiscomobj.cmbt_vel = Trig.sides_ab(speed, (Trig.angleto(thiscomobj.cmbt_loc, thiscomobj.cmbt_face)));
                comObjs.Add(thiscomobj);
                Empires[ship.Owner].ownships.Add(thiscomobj);
                foreach (KeyValuePair<Empire, EmpireinCombat> empire in Empires)
                {
                    if (ship.IsHostileTo(empire.Key))
                        empire.Value.hostile.Add(thiscomobj);
                    else if (ship.Owner != empire.Key)
                        empire.Value.friendly.Add(thiscomobj);
                }
            }
        }

        private void commandAI(CombatObj comObj)
        {
            //do AI decision stuff.
            //pick a primary target to persue, use AI script from somewhere.  this could also be a formate point. and could be a vector rather than a static point. 
            combatWaypoint wpt = new combatWaypoint(Empires[comObj.icomobj.Owner].hostile[0]);
            comObj.waypointTarget = wpt;
            //pick a primary target to fire apon from a list of enemy within weapon range
            comObj.weaponTarget[0] = Empires[comObj.icomobj.Owner].hostile[0];
            
        }

        private void helm(CombatObj comObj)
        {
            //rotate ship
            double timetoturn = 0;
            Compass angletoturn = new Compass(Trig.angleto(comObj.cmbt_face, comObj.waypointTarget.cmbt_loc));
            Point3d vectortowaypoint = comObj.waypointTarget.cmbt_loc - comObj.cmbt_loc;
            if (comObj.lastVectortoWaypoint != null)
                angletoturn.Radians = Trig.angleA(vectortowaypoint - comObj.lastVectortoWaypoint);

            timetoturn = angletoturn.Radians / comObj.Rotate;
            Point3d offsetVector = comObj.waypointTarget.cmbt_vel - comObj.cmbt_vel; // O = a - b
            double timetomatchspeed = Trig.angleA(offsetVector) / comObj.Accel;

            double timetowpt = Trig.angleA(offsetVector) / ticlen;

            //if/when we're going to overshoot teh waypoint
            if (timetowpt <= timetomatchspeed + timetoturn)
            {
                angletoturn.Degrees += 180; //turn around and thrust the other way
            }

            if (angletoturn.Degrees < 180) //turn to the right
            {
                if (angletoturn.Degrees > comObj.Rotate)
                {
                    comObj.cmbt_face += comObj.Rotate;
                }
                else
                {
                    comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
                }
            }
            else
            {
                if (angletoturn.Degrees > -comObj.Rotate)
                {
                    comObj.cmbt_face -= comObj.Rotate;
                }
                else
                {
                    comObj.cmbt_face = comObj.waypointTarget.cmbt_loc;
                }
            }

            //thrust ship
            if (angletoturn.Degrees < 45 || angletoturn.Degrees > 315)
            {
                //I think.... also todo add straf ability (and reverse thrust)
                comObj.cmbt_vel += Trig.intermediatePoint(comObj.cmbt_vel, comObj.cmbt_face, comObj.Accel);
            }

            comObj.lastVectortoWaypoint = vectortowaypoint;
        }

        private void firecontrol(double tic_countr, CombatObj comObj)
        {
            foreach (var weapon in comObj.icomobj.Weapons)
            {
                Vehicle ship = (Vehicle)comObj.icomobj;
                //ship.Weapons
                Component wpn = (Component)weapon;
                if (wpn.CanTarget(comObj.weaponTarget[0].icomobj))
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
                        combatEvent fire = FireWeapon(tic_countr - 1, comObj, wpn, comObj.weaponTarget[0]);

                        if (weapon.Template.ComponentTemplate.WeaponInfo.DisplayEffect.GetType() == typeof(Combat.ProjectileWeaponDisplayEffect)) //projectile
                        {
                            fire.endevent(tic_countr, comObj.weaponTarget[0].cmbt_loc);
                        }
                    }
                }
            }
        }

        private void battleloop(double tic_countr)
        {

            foreach (CombatObj comObj in comObjs)
            {

                //heading and thrust
                helm(comObj);

                //fire ready weapons.
                firecontrol(tic_countr, comObj);
              
                //comObj.lastDistancetoWaypoint = Trig.distance(comObj.cmbt_loc, comObj.weaponTarget[0].cmbt_loc);
            }
            simPhysTic(tic_countr);
        }

        public void Resolve()
        {
            //start combat
            Current.Add(this);
            bool isreplay = false;
            if (replaylog != null)
            {
                isreplay = true;
            }

            setupPices();
            
            
            //unleash the dogs of war!
            bool battleongoing = true;

            double tic_countr = 0;
            double cmdfreq_countr = 0;
            while (battleongoing)
            {
                battleloop(tic_countr);
                if (cmdfreq_countr >= comdfreq)
                {
                    foreach (CombatObj comObj in comObjs)
                        commandAI(comObj);
                    cmdfreq_countr = 0;
                }

                bool ships_persuing = true;
                bool ships_inrange = true; //ships are in skipdrive interdiction range of enemy ships

                if (!ships_persuing && !ships_inrange)
                    battleongoing = false;
                cmdfreq_countr++;
                tic_countr++;
            }

            //end combat
            

 
            Current.Remove(this);
            Previous.Add(this);
        }

        private bool isinRange(CombatObj attacker, Component weapon, CombatObj target)
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

        private combatEvent FireWeapon(double tic, CombatObj attacker, Component weapon, CombatObj target)
        {
            combatEvent shotevent = new combatEvent(tic, "FireWeapon", attacker, target);
            double range = Trig.distance(attacker.cmbt_loc, target.cmbt_loc);
            ICombatObject defender = target.icomobj;
            Vehicle defenderV = (Vehicle)defender;

            if (!weapon.CanTarget(defender))
                return null;

            // TODO - check range too
            var tohit = Mod.Current.Settings.WeaponAccuracyPointBlank + weapon.Template.WeaponAccuracy + weapon.Container.Accuracy - defender.Evasion;
            // TODO - moddable min/max hit chances with per-weapon overrides
            if (tohit > 99)
                tohit = 99;
            if (tohit < 1)
                tohit = 1;
            bool hit = RandomHelper.Range(0, 99) < tohit;
            shotevent.hitTarget(hit);
            //battle.LogShot(this, hit);
            if (hit)
            {
                var shot = new Combat.Shot(weapon, defender, (int)range); 
                //defender.TakeDamage(weapon.Template.ComponentTemplate.WeaponInfo.DamageType, shot.Damage, battle);
                if (defender.MaxNormalShields < defender.NormalShields)
                    defender.NormalShields = defender.MaxNormalShields;
                if (defender.MaxPhasedShields < defender.PhasedShields)
                    defender.PhasedShields = defender.MaxPhasedShields;
                //if (defender.IsDestroyed)
                    //battle.LogTargetDeath(defender);
                
            }
            return shotevent;
        }

        private void combatDamage(CombatObj target, Component weapon, int damage)
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
                var comp = canBeHit.ToDictionary(c => c, c => c.HitChance).PickWeighted();
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

    }
}
