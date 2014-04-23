using System;
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

namespace FrEee.Tests.Game.Objects.Combat2
{





    public class testships
    {
        Galaxy gal = new Galaxy();
        public List<SimulatedEmpire> empires = new List<SimulatedEmpire>();
        List<Design<Ship>> designs = new List<Design<Ship>>();
        public List<SpaceVehicle> ships = new List<SpaceVehicle>();
        Hull<Ship> hull;

        Dictionary<string, ComponentTemplate> components = new Dictionary<string, ComponentTemplate>();

        public testships()
        {

            empires.Add(new SimulatedEmpire(new Empire()));
            empires.Add(new SimulatedEmpire(new Empire()));
            empires[0].Empire.Name = "TestEmpOne";
            empires[0].Empire.Culture = new Culture();
            empires[1].Empire.Name = "TestEmpTwo";
            empires[0].Empire.Culture = new Culture();
            Mod.Current = new Mod();

            //createhull
            hull = new Hull<Ship>();
            hull.Size = 100;
            Mod.Current.Hulls.Add(hull);
            Galaxy.Current.AssignID(hull);

            

            var armor = new ComponentTemplate();
            armor.Size = 10;
            armor.Durability = new Formula<int>(armor, "self.Size * 3", FormulaType.Dynamic);
            Mod.Current.ComponentTemplates.Add(armor);
            Galaxy.Current.AssignID(armor);
            components.Add("AMR", armor);

            var bridge = new ComponentTemplate();
            bridge.Size = 10;
            bridge.Durability = 10;
            Ability CnC = new Ability(bridge);
            Mod.Current.ComponentTemplates.Add(bridge);
            Galaxy.Current.AssignID(bridge);
            components.Add("BDG", bridge);

            var lifesuport = new ComponentTemplate();
            lifesuport.Size = 10;
            lifesuport.Durability = 10;
            Mod.Current.ComponentTemplates.Add(lifesuport);
            Galaxy.Current.AssignID(lifesuport);
            components.Add("LS", lifesuport);

            var crewQuarters = new ComponentTemplate();
            crewQuarters.Size = 10;
            crewQuarters.Durability = 10;
            Mod.Current.ComponentTemplates.Add(crewQuarters);
            Galaxy.Current.AssignID(crewQuarters);
            components.Add("CQ", crewQuarters);

            var engine = new ComponentTemplate();
            engine.Size = 10;
            engine.Durability = 10;
            Ability engmove = new Ability(engine);
            
            Mod.Current.ComponentTemplates.Add(engine);
            Galaxy.Current.AssignID(engine);
            components.Add("Engn", engine);

            var cannon = new ComponentTemplate();
            cannon.Size = 10;
            cannon.Durability = 10;
            Mod.Current.ComponentTemplates.Add(cannon);
            Galaxy.Current.AssignID(cannon);
            components.Add("Wpn_DF", cannon);

            var laser = new ComponentTemplate();
            laser.Size = 10;
            laser.Durability = 10;
            Mod.Current.ComponentTemplates.Add(laser);
            Galaxy.Current.AssignID(laser);
            components.Add("Wpn_BEAM", laser);

            var missleLauncher = new ComponentTemplate();
            missleLauncher.Size = 10;
            missleLauncher.Durability = 10;
            Mod.Current.ComponentTemplates.Add(missleLauncher);
            Galaxy.Current.AssignID(missleLauncher);
            components.Add("Wpn_SK", missleLauncher);

            //create designs and ships
            testdesign0(empires[0].Empire);
            testship0(empires[0], designs[0]);
            testdesign0(empires[1].Empire);
            testship0(empires[1], designs[0]);

        }

        public List<MountedComponentTemplate> genericlistofcomponents(Design<Ship> design)
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

        public void testdesign0(Empire emp)
        {
            var design = new Design<Ship>();
            
            designs.Add(design);
            
            Galaxy.Current.AssignID(design);
            design.Owner = emp;


           

            List<MountedComponentTemplate> mctlist = genericlistofcomponents(design);

            mctlist.Add(new MountedComponentTemplate(design, components["Wpn_DF"], null));
            mctlist.Add(new MountedComponentTemplate(design, components["AMR"], null));
                        
            foreach (var mct in mctlist)
                design.Components.Add(mct);

            design.Hull = hull;
            designs.Add(design);
        }

        public void testship0(SimulatedEmpire emp, Design<Ship> dsn)
        {
            var sv = new SimulatedSpaceObject((SpaceVehicle)dsn.Instantiate());
            var v = (SpaceVehicle)sv.SpaceObject;
            v.Owner = emp.Empire;
            emp.SpaceObjects.Add(sv);
            //BindSpaceObjectList();
            ships.Add(v);
        }
    }

    [TestClass]
    public class BattleTests_combat
    {

        [TestMethod]
        public void BattleSpace_Combat()
        {
            testships testships = new testships();
            StarSystem sys = new StarSystem(1);
            Sector location = new Sector(sys, new System.Drawing.Point());
            List<SpaceVehicle> ships = testships.ships;

            foreach (SpaceVehicle ship in ships)
            {
                location.Place(ship);
            }

            Battle_Space battle = new Battle_Space(location);
        }
    }
}
