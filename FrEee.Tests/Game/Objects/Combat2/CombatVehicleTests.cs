using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Modding.Loaders;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects;
using FrEee.Modding.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Combat2;
using FrEee.Utility.Extensions;

using NewtMath.f16;

namespace FrEee.Tests.Game.Objects.Combat2
{



    public static class testships2
    {
        public static Dictionary<string, ComponentTemplate> Components(Galaxy gal)
        {

            Mod mod = Mod.Load(null); 
            Dictionary<string, ComponentTemplate> components = new Dictionary<string, ComponentTemplate>();

            ComponentTemplate armor = mod.ComponentTemplates.FindByName("Armor I");
            gal.AssignID(armor);
            components.Add("AMR", armor);

            ComponentTemplate bridge = mod.ComponentTemplates.FindByName("Bridge");
            gal.AssignID(bridge);
            components.Add("BDG", bridge);


            var lifesuport = mod.ComponentTemplates.FindByName("Life Support");
            gal.AssignID(lifesuport);
            components.Add("LS", lifesuport);

            var crewQuarters = mod.ComponentTemplates.FindByName("Crew Quarters");
            gal.AssignID(crewQuarters);
            components.Add("CQ", crewQuarters);

            var engine = mod.ComponentTemplates.FindByName("Ion Engine I");
            gal.AssignID(engine);
            components.Add("Engn", engine);

            var cannon = mod.ComponentTemplates.FindByName("Depleted Uranium Cannon I");
            gal.AssignID(cannon);
            components.Add("Wpn_DF", cannon);

            var laser = mod.ComponentTemplates.FindByName("Anti - Proton Beam I");
            gal.AssignID(laser);
            components.Add("Wpn_BEAM", laser);

            var missleLauncher = mod.ComponentTemplates.FindByName("Capital Ship Missile I");
            gal.AssignID(missleLauncher);
            components.Add("Wpn_SK", missleLauncher);

            return components;
        }

        public static Empire empire(string name, Culture culture, Race race)
        {
            Empire emp = new Empire();
            emp.Name = name;
            emp.Culture = culture;
            emp.PrimaryRace = race;

            return emp;
        }

        public static Design<Ship> EscortDUC(Galaxy gal, Empire emp, Dictionary<string, ComponentTemplate> components)
        {
            Mod mod = Mod.Load(null); 
            Design<Ship> design = new Design<Ship>();
            gal.AssignID(design);
            design.Owner = emp;

            List<MountedComponentTemplate> mctlist = genericlistofcomponents(design, components);

            mctlist.Add(new MountedComponentTemplate(design, components["Wpn_DF"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["AMR"], null));

            foreach (var mct in mctlist)
                design.Components.Add(mct);

            design.Hull = (Hull<Ship>)mod.Hulls.FindByName("Escort");
            design.Strategy = new StragegyObject_Default();
            //designs.Add(design);
            return design;
        }

        public static Design<Ship> EscortCSM(Galaxy gal, Empire emp, Dictionary<string, ComponentTemplate> components)
        {
            Mod mod = Mod.Load(null);
            Design<Ship> design = new Design<Ship>();
            gal.AssignID(design);
            design.Owner = emp;

            List<MountedComponentTemplate> mctlist = genericlistofcomponents(design, components);

            mctlist.Add(new MountedComponentTemplate(design, components["Wpn_SK"], null));
            //mctlist.Add(new MountedComponentTemplate(design, components["AMR"], null));

            foreach (var mct in mctlist)
                design.Components.Add(mct);

            design.Hull = (Hull<Ship>)mod.Hulls.FindByName("Escort");
            design.Strategy = new StragegyObject_Default();
            //designs.Add(design);
            return design;
        }

        public static List<MountedComponentTemplate> genericlistofcomponents(Design<Ship> design, Dictionary<string, ComponentTemplate> components)
        {
            List<MountedComponentTemplate> mctlist = new List<MountedComponentTemplate>();
            mctlist.Add(new MountedComponentTemplate(design, components["BDG"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["LS"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["CQ"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["Engn"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["Engn"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["Engn"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["Engn"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["Engn"], null));

            return mctlist;
        }

        public static SpaceVehicle testShip(SimulatedEmpire emp, Design<Ship> design, int ID)
        {
            SimulatedSpaceObject simveh = new SimulatedSpaceObject((SpaceVehicle)design.Instantiate());
            simveh.SpaceObject.ID = ID;
            SpaceVehicle spaceveh = (SpaceVehicle)simveh.SpaceObject;
            spaceveh.Owner = emp.Empire;
            emp.SpaceObjects.Add(simveh);
            return spaceveh;
        }
    }

    


    [TestClass]
    public class CombatVehicleTests
    {
        Galaxy gal = new Galaxy();
        StarSystem sys = new StarSystem(1);
        Sector location;
        Battle_Space battle;
        
        public void setupEnvironment0()
        {
            location = new Sector(sys, new System.Drawing.Point());
            
            SimulatedEmpire simemp = new SimulatedEmpire(testships2.empire("TestEmpOne", new Culture(), new Race()));
            Design<Ship> design = testships2.EscortDUC(gal, simemp.Empire, testships2.Components(gal));
            SpaceVehicle sv = testships2.testShip(simemp, design, 100);
            location.Place(sv);

            battle = new Battle_Space(location);
        }

        public void setupEnvironment1()
        {
            location = new Sector(sys, new System.Drawing.Point());

            SimulatedEmpire simemp0 = new SimulatedEmpire(testships2.empire("TestEmpOne", new Culture(), new Race()));
            SimulatedEmpire simemp1 = new SimulatedEmpire(testships2.empire("TestEmpTwo", new Culture(), new Race()));
            Design<Ship> design0 = testships2.EscortCSM(gal, simemp0.Empire, testships2.Components(gal));
            Design<Ship> design1 = testships2.EscortDUC(gal, simemp1.Empire, testships2.Components(gal));

            SpaceVehicle sv0 = testships2.testShip(simemp0, design0, 100);
            SpaceVehicle sv1 = testships2.testShip(simemp1, design1, 200);
            location.Place(sv0);
            location.Place(sv1);

            battle = new Battle_Space(location);
        }

        [TestMethod]
        public void Combat_Nav10()
        {

            setupEnvironment0();
            Console.WriteLine("Nav test 0");

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoWaypoint = new Compass(0, false);
            PointXd waypntloc = new PointXd(0, 1000, 0);
            PointXd waypndVel = new PointXd(0, 1000, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            bool expectedToWaypoint = true;
            Compass expectedHeading = new Compass(0);
            Tuple<Compass, bool> expectednav = new Tuple<Compass, bool>(expectedHeading, expectedToWaypoint);
            CombatVehicle testComObj = battle.CombatVehicles.ToArray()[0];
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.waypointTarget = waypoint;

            Tuple<Compass, bool?> nav = testComObj.testNav(angletoWaypoint);
            battle.End(1);
            Assert.AreEqual(expectednav.Item1.Degrees, nav.Item1.Degrees);
            Assert.AreEqual(expectednav.Item2, nav.Item2);
        }

        [TestMethod]
        public void Combat_Nav11()
        {
            setupEnvironment0();

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoWaypoint = new Compass(180, false);
            PointXd waypntloc = new PointXd(0, -1000, 0);
            PointXd waypndVel = new PointXd(0, 0, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            bool expectedToWaypoint = true;
            Compass expectedHeading = new Compass(180, false);
            Tuple<Compass, bool> expectednav = new Tuple<Compass, bool>(expectedHeading, expectedToWaypoint);
            CombatVehicle testComObj = battle.CombatVehicles.ToArray()[0];
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 0, 0);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.waypointTarget = waypoint;

            Tuple<Compass, bool?> nav = testComObj.testNav(angletoWaypoint);
            battle.End(1);
            Assert.AreEqual(expectednav.Item1.Degrees, nav.Item1.Degrees);
            Assert.AreEqual(expectednav.Item2, nav.Item2);
        }

        [TestMethod]
        public void Combat_Nav12()
        {
            setupEnvironment0();

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoWaypoint = new Compass(0, false);
            PointXd waypntloc = new PointXd(0, 10, 0);
            PointXd waypndVel = new PointXd(0, 0, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            bool expectedThrustToWaypoint = false;
            Compass expectedHeading = new Compass(180, false);
            Tuple<Compass, bool> expectednav = new Tuple<Compass, bool>(expectedHeading, expectedThrustToWaypoint);
            CombatVehicle testComObj = battle.CombatVehicles.ToArray()[0];
            testComObj.cmbt_loc = new PointXd(0, 0, 0);
            testComObj.cmbt_vel = new PointXd(0, 5, 0);
            testComObj.maxfowardThrust = 150;
            testComObj.maxStrafeThrust = 37.5;
            testComObj.maxRotate = new Compass(45, false);
            testComObj.cmbt_head = new Compass(startHeading.Degrees, false);
            testComObj.waypointTarget = waypoint;

            Tuple<Compass, bool?> nav = testComObj.testNav(angletoWaypoint);
            battle.End(1);
            Assert.AreEqual(expectednav.Item1.Degrees, nav.Item1.Degrees);
            Assert.AreEqual(expectednav.Item2, nav.Item2);
        }

        [TestMethod]
        public void Combat_Missle()
        {
            setupEnvironment1();

            battle.Start();

            Compass startHeading = new Compass(0, false);
            Compass angletoWaypoint = new Compass(0, false);
            PointXd waypntloc = new PointXd(0, 0, 0);
            PointXd waypndVel = new PointXd(0, 0, 0);
            combatWaypoint waypoint = new combatWaypoint(waypntloc, waypndVel);

            bool expectedThrustToWaypoint = true;
            Compass expectedHeading = new Compass(0, false);
            Tuple<Compass, bool> expectednav = new Tuple<Compass, bool>(expectedHeading, expectedThrustToWaypoint);


            CombatVehicle attacker = battle.CombatVehicles.ToArray()[0];
            attacker.cmbt_loc = new PointXd(0, -100, 0);
            attacker.cmbt_vel = new PointXd(0, 0, 0);
            attacker.cmbt_head = new Compass(startHeading.Degrees, false);
            attacker.waypointTarget = waypoint;
            CombatWeapon wpn = attacker.Weapons.ToArray()[0];

            CombatVehicle target = battle.CombatVehicles.ToArray()[1];
            target.cmbt_loc = new PointXd(0, 100, 0);
            target.cmbt_vel = new PointXd(0, 0, 0);
            target.cmbt_head = new Compass(180, false);
            target.waypointTarget = waypoint;

            attacker.weaponTarget[0] = target;
            
            CombatSeeker testSeeker = new CombatSeeker(attacker, wpn, -1);
            
            battle.CombatNodes.Add(testSeeker);

            Tuple<Compass, bool?> nav = testSeeker.testNav(angletoWaypoint);
            battle.End(1);
            Assert.AreEqual(expectednav.Item1.Degrees, nav.Item1.Degrees);
            Assert.AreEqual(expectednav.Item2, nav.Item2);
        }

        [TestMethod]
        public void Combat_ReplayTest01()
        {

            setupEnvironment1();

            battle.Start();
            
            CombatObject battlecomobj = battle.CombatObjects.ToArray()[0];

            PointXd loc1 = new PointXd(battlecomobj.cmbt_loc);
            long diceit1 = battlecomobj.getDice().Iteration;
            int numenemies1 = battlecomobj.empire.ownships.Count();
            battle.Resolve(); 
                        
            battle.SetUpPieces();

            PointXd loc2 = new PointXd(battlecomobj.cmbt_loc);
            long diceit2 = battlecomobj.getDice().Iteration;
            int numenemies2 = battlecomobj.empire.ownships.Count();
            Assert.AreEqual(loc1, loc2);
            Assert.AreEqual(diceit1, diceit2);
            Assert.AreEqual(numenemies1, numenemies2);
        }

        [TestMethod]
        public void Combat_ReplayTest02()
        {

            setupEnvironment1();

            battle.Start();
            battle.Resolve();          
            CombatObject battlecomobj = battle.CombatObjects.ToArray()[0];

            PointXd loc1 = new PointXd(battlecomobj.cmbt_loc);
            long diceit1 = battlecomobj.getDice().Iteration;
            int numenemies1 = battlecomobj.empire.ownships.Count();

            battle.SetUpPieces(); //this should be replay setup now.

            PointXd loc2 = new PointXd(battlecomobj.cmbt_loc);
            long diceit2 = battlecomobj.getDice().Iteration;
            int numenemies2 = battlecomobj.empire.ownships.Count();

            Assert.AreEqual(true, battle.IsReplay);
            Assert.AreNotEqual(loc1, loc2);
            Assert.AreNotEqual(diceit1, diceit2);
            Assert.AreNotEqual(numenemies1, numenemies2);

        }
    }


}
