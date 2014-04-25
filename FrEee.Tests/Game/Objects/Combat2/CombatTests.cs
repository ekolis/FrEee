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
            


            string data;
            DataFile df;
            ComponentLoader loader;
            Mod mod = new Mod();

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
            data =
@"*BEGIN*

Name                  := Ion Engine I
Description           := Standard Ion Engine for sub-light inter-system travel.
Pic Num               := 9
Tonnage Space Taken   := 10
Tonnage Structure     := 20
Cost Minerals         := 200
Cost Organics         := 0
Cost Radioactives     := 50
Vehicle Type          := Ship\Base\Drone
Supply Amount Used    := 10
Restrictions          := None
General Group         := Engines
Family                := 9
Roman Numeral         := 1
Custom Group          := 0
Number of Tech Req    := 1
Tech Area Req 1       := Propulsion
Tech Level Req 1      := 1
Number of Abilities   := 3
Ability 1 Type        := Standard Ship Movement
Ability 1 Descr       := Generates 1 standard movement.
Ability 1 Val 1       := 1
Ability 1 Val 2       := 0
Ability 2 Type        := Supply Storage
Ability 2 Descr       := Can store 500 units of supply.
Ability 2 Val 1       := 500
Ability 2 Val 2       := 0
Ability 3 Type        := Movement Bonus
Ability 3 Descr       := 
Ability 3 Val 1       := 0
Ability 3 Val 2       := 0
Weapon Type           := None";
            df = new DataFile(data);
            loader = new ComponentLoader("Test Mod");
            loader.DataFile = df;            
            loader.Load(mod);
            engine = mod.ComponentTemplates.Single();
            Ability engmove = new Ability(engine);
            
            Mod.Current.ComponentTemplates.Add(engine);
            Galaxy.Current.AssignID(engine);
            components.Add("Engn", engine);

            var cannon = new ComponentTemplate();
            data =
@"*BEGIN*

Name                  := Depleted Uranium Cannon I
Description           := Medium range cannon which fires large depleted uranium shells.
Pic Num               := 98
Tonnage Space Taken   := 30
Tonnage Structure     := 30
Cost Minerals         := 100
Cost Organics         := 0
Cost Radioactives     := 5
Vehicle Type          := Ship\Base\Sat\WeapPlat\Drone
Supply Amount Used    := 2
Restrictions          := None
General Group         := Weapons
Family                := 2027
Roman Numeral         := 1
Custom Group          := 0
Number of Tech Req    := 1
Tech Area Req 1       := Projectile Weapons
Tech Level Req 1      := 1
Number of Abilities   := 0
Weapon Type           := Direct Fire
Weapon Target         := Ships\Planets\Ftr\Sat\Drone
Weapon Damage At Rng  := 20 20 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
Weapon Damage Type    := Normal
Weapon Reload Rate    := 1
Weapon Display Type   := Torp
Weapon Display        := 19
Weapon Modifier       := 0
Weapon Sound          := uranc.wav
Weapon Family         := 25";
            df = new DataFile(data);
            loader = new ComponentLoader("Test Mod");
            loader.DataFile = df;
            
            loader.Load(mod);
            cannon = mod.ComponentTemplates.Single();
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
