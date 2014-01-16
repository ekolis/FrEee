using FrEee.Utility.Extensions;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
    public class CombatSeeker : CombatObject
    {
        public CombatSeeker(CombatObject attacker, CombatWeapon launcher, int ID)
            : base(new Point3d(attacker.cmbt_loc), new Point3d(attacker.cmbt_vel), ID)
        {

            SeekingWeaponInfo skrinfo = (SeekingWeaponInfo)launcher.weapon.Template.ComponentTemplate.WeaponInfo;
            int hitpoints = skrinfo.SeekerDurability;
            cmbt_mass = (Fix16)hitpoints;//(Fix16)s.MaxHitpoints; // sure why not?
            int wpnskrspd = skrinfo.SeekerSpeed;
            int wpnskrEvade = Mod.Current.Settings.SeekerEvasion;
            maxfowardThrust = (Fix16)wpnskrspd * this.cmbt_mass * (Fix16)0.001;
            maxStrafeThrust = ((Fix16)wpnskrspd * this.cmbt_mass * (Fix16)0.001) / ((Fix16)4 - (Fix16)wpnskrEvade * (Fix16)0.01);
            maxRotate = ((Fix16)wpnskrspd * this.cmbt_mass * (Fix16)0.001) / ((Fix16)12 - (Fix16)wpnskrEvade * (Fix16)0.1);
            

            cmbt_thrust = new Point3d(0, 0, 0);
            cmbt_accel = new Point3d(0, 0, 0);

            newDice(ID);

            this.launcher = launcher;
        }

        #region fields
        public CombatTakeFireEvent seekertargethit { get; set; }

        //the component that fired the missile.
        public CombatWeapon launcher { get; private set; }
        #endregion

        #region methods and functions


        #endregion
    }
}
