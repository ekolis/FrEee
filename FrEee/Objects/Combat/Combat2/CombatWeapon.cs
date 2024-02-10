using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;
using FrEee.Utility.Extensions;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

using NewtMath.f16;
using FixMath.NET;
using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Combat2
{

    public class CombatWeapon
    {


        public CombatWeapon(Component weapon)
        {
#if DEBUG
            Console.WriteLine("Creating New CombatWeapon");
#endif
            this.weapon = weapon;
#if DEBUG
            Console.WriteLine("Getting Weapon Template WeaponInfo");
#endif
            var wpninfo = weapon.Template.ComponentTemplate.WeaponInfo;
#if DEBUG
            Console.WriteLine("Done");
            Console.WriteLine("Getting Range info");
#endif            
            int wpMaxR = wpninfo.MaxRange;
#if DEBUG
            Console.WriteLine("Done MaxRange");
#endif
            int wpMinR = wpninfo.MinRange;
#if DEBUG
            Console.WriteLine("Done MinRange");
#endif
#if DEBUG
            Console.WriteLine("done getting random info");
#endif
            if (wpninfo.DisplayEffect.GetType() == typeof(Combat.BeamWeaponDisplayEffect))
            {
                weaponType = "Beam";

                maxRange_distance = (Fix16)wpMaxR * Battle_Space.KilometersPerSquare;
                minRange = (Fix16)wpMinR * Battle_Space.KilometersPerSquare;
            }
            else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.ProjectileWeaponDisplayEffect))
            {
                weaponType = "Bolt";

                boltSpeed = (Fix16)wpMaxR * Battle_Space.KilometersPerSquare; 
                maxRange_time = (Fix16)1; // (maxTime for bolts) untill mod files can handle this, bolt weapons range is the distance it can go in 1 sec.
                minRange = ((Fix16)wpMinR / boltSpeed); //(minTime for bolts) distance / speed = time                  
            }
            else if (wpninfo.DisplayEffect.GetType() == typeof(Combat.SeekerWeaponDisplayEffect))
            {
#if DEBUG
                Console.WriteLine("CombatWeapon is a Seeker");
#endif
                SeekingWeaponInfo seekerinfo = (SeekingWeaponInfo)weapon.Template.ComponentTemplate.WeaponInfo;
                weaponType = "Seeker";
                boltSpeed = 0; //seekers get launched at 0 speed. 
                int mass = seekerinfo.SeekerDurability; // sure why not?
                int wpnskrspd = seekerinfo.SeekerSpeed;
                //Fix16 Thrust = (Fix16)wpnskrspd * mass * (Fix16)0.001;

                //boltSpeed = (Fix16)wpMaxR * (Fix16)1000 * (Fix16)(Battle_Space.TickLength); // convert from kilometers per second to meters per tick
                maxRange_time = (Fix16)wpMaxR / (Fix16)wpnskrspd; // (maxTime for Missiles) untill mod files can handle this, bolt weapons range is the distance it can go in 1 sec.
            }
            else //treat it like a beam I guess. 
            {
                weaponType = "Unknown";
				maxRange_distance = (Fix16)wpMaxR * Battle_Space.KilometersPerSquare;
				minRange = (Fix16)wpMinR * Battle_Space.KilometersPerSquare;
            }
            double wpiReloadRate = wpninfo.ReloadRate;
            reloadRate = (Fix16)wpiReloadRate;
            nextReload = 1;
#if DEBUG
            Console.WriteLine("Done creating CombatWeapon");
#endif

        }

        /// <summary>
        /// "Beam", "Bolt", "Seeker".
        /// </summary>
        public string weaponType { get; set; }

        /// <summary>
        /// the actual component.
        /// </summary>
		[DoNotAssignID]
        public Component weapon { get; private set; }

        /// <summary>
        /// nextReload tick (when battletick >= this, fire again, then reset this to current tick + reload rate)
        /// </summary>
        public int nextReload { get; set; }

        /// <summary>
        /// the rate the weapon can reload in seconds.
        /// </summary>
        public Fix16 reloadRate { get; private set; }

        /// <summary>
        /// if a bolt, what is it's speed if fired at rest? in m/s
        /// </summary>
        public Fix16 boltSpeed { get; private set; }

        /// <summary>
        /// if a bolt (or seeker?), this is time, else it's distance 
        /// </summary>
        public Fix16 maxRange
        {
            get
            {
                Fix16 retnum;
                if (weaponType == "Beam")
                {
                    retnum = maxRange_distance;
                }
                else
                {
                    retnum = maxRange_time;
                }
                return retnum;
            }
        }

        /// <summary>
        /// for seekers and bolts.
        /// </summary>
        public Fix16 maxRange_time { get; private set; }

        /// <summary>
        /// for beams.
        /// </summary>
        public Fix16 maxRange_distance { get; private set; }

        /// <summary>
        /// if a bolt (or seeker?), this is time, else it's distance 
        /// </summary>
        public Fix16 minRange { get; private set; }

        public bool CanTarget(ITargetable target)
        {
            return weapon.CanTarget(target);
        }

        public bool isinRange(CombatObject attacker, CombatObject target)
        {
            bool inrange = false;
            var wpninfo = weapon.Template.ComponentTemplate.WeaponInfo;
            Fix16 distance_toTarget = NMath.distance(attacker.cmbt_loc, target.cmbt_loc);

            string weaponRangeinfo = "RangeInfo:\r\n ";


            if (weaponType == "Beam")          //beam
            {
                if (distance_toTarget <= maxRange && distance_toTarget >= minRange)
                {
                    inrange = true;
                    weaponRangeinfo += "Range for Beam is good \r\n";
                }
            }
            else if (weaponType == "Bolt") //projectile
            {
                inrange = bolt_isinRange(attacker, target);
            }
            else if (weaponType == "Seeker")       //seeker
            {
                inrange = seeker_isinRange(attacker, target);
            }

            attacker.debuginfo += weaponRangeinfo;
            return inrange;
        }

        private bool seeker_isinRange(CombatObject attacker, CombatObject target)
        {
            bool isinRange = false;
            Fix16 seekerTimeToTarget = CombatSeeker.seekerTimeToTarget(attacker, target, (SeekingWeaponInfo)weapon.Template.ComponentTemplate.WeaponInfo);
            if (seekerTimeToTarget < maxRange_time)
                isinRange = true;
            return isinRange;
        }

        private bool bolt_isinRange(CombatObject attacker, CombatObject target)
        {
            bool isinRange = false;
            Fix16 boltTTT = boltTimeToTarget(attacker, target);
            //remember, maxRange is bolt lifetime in seconds 
            if (boltTTT <= maxRange && boltTTT >= minRange)
            {
                isinRange = true;
            }
            return isinRange;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns>speed in meters per second</returns>
        public Fix16 boltClosingSpeed(CombatObject attacker, CombatObject target)
        {
            Fix16 shotspeed = boltSpeed; //speed of bullet when ship is at standstill
            Fix16 shotspeed_actual = shotspeed + NMath.closingRate(attacker.cmbt_loc, attacker.cmbt_vel, target.cmbt_loc, target.cmbt_vel);
            return shotspeed_actual;// / Battle_Space.TicksPerSecond;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns>t in seconds</returns>
        public Fix16 boltTimeToTarget(CombatObject attacker, CombatObject target)
        {
            Fix16 distance_toTarget = NMath.distance(attacker.cmbt_loc, target.cmbt_loc);
            Fix16 boltTimetoTarget = distance_toTarget / boltClosingSpeed(attacker, target);
            return boltTimetoTarget;
        }

		public override string ToString()
		{
			return weapon.ToString();
		}
	}

}
