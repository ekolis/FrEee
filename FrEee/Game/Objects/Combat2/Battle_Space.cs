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
    class Battle_Space : INamed, ILocated
    {
        public Battle_Space(Sector location)
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
            //foreach (var item in Sector.SpaceObjects.OfType<
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

        

        private List<IMobileSpaceObject> combatgroups;
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

        

        public void Resolve()
        {
            //start combat
            Current.Add(this);

            

            Point3d[] startpoints = new Point3d[EmpiresArray.Count()];
            
            int angle = 0;
            for(int i = 0; i <= EmpiresArray.Count(); i++)
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
            
            
            //unleash the dogs of war!
            bool battleongoing = true;
            double ticlen = 0.1;
            double comdfreq = 10;
            while (battleongoing)
            {
                double cmdfreq_countr = 0;
                bool ships_persuing = true;
                bool ships_inrange = true; //ships are in skipdrive interdiction range of enemy ships
                //do stuff

                foreach (CombatObj comObj in comObjs)
                {
                    
                    if (cmdfreq_countr >= comdfreq)
                    {
                        //do AI decision stuff.
                        //pick a primary target to persue, use AI script from somewhere.  this could also be a formate point. and could be a vector rather than a static point. 
                        combatWaypoint wpt = new combatWaypoint(Empires[comObj.icomobj.Owner].hostile[0]);
                        comObj.movetarget = wpt;
                        //pick a primary target to fire apon from a list of enemy within weapon range
                        comObj.weaponTarget[0] = Empires[comObj.icomobj.Owner].hostile[0];
                        cmdfreq_countr = 0;
                    }

                    //rotate ship
                    double timetoturn = 0;
                    
                    Compass angletoturn = new Compass(Trig.angleto(comObj.cmbt_face, comObj.movetarget.cmbt_loc));
                    Point3d vectortotarget = comObj.movetarget.cmbt_loc - comObj.cmbt_loc;
                    if(comObj.lastvectortotarget != null)
                        angletoturn.Radians = Trig.angleA(vectortotarget - comObj.lastvectortotarget);
                    comObj.lastvectortotarget = vectortotarget;
                    
                    
                    timetoturn = angletoturn.Radians / comObj.Rotate;
                    Point3d offsetVector = comObj.movetarget.cmbt_vel - comObj.cmbt_vel; // O = a - b
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
                            comObj.cmbt_face = comObj.movetarget.cmbt_loc;
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
                            comObj.cmbt_face = comObj.movetarget.cmbt_loc;
                        }
                    }
                    //thrust ship
                    if (angletoturn.Degrees < 45 || angletoturn.Degrees > 315)
                    {
                        //I think.... also todo add straf ability (and reverse thrust)
                        comObj.cmbt_vel += Trig.intermediatePoint(comObj.cmbt_vel, comObj.cmbt_face, comObj.Accel);
                    }

                    //move ship
                    comObj.cmbt_loc += comObj.cmbt_vel * ticlen;

                    //fire ready weapons.
                    
        
                }


                if (!ships_persuing && !ships_inrange)
                    battleongoing = false;
                cmdfreq_countr++;
            }

            //end combat
            Current.Remove(this);
            Previous.Add(this);
        }

    }
}
