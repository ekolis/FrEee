using FrEee.Utility.Extensions;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

using FixMath.NET;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat2
{
    public class CombatSeeker : CombatObject, ITargetable
    {
        public CombatSeeker(CombatObject attacker, CombatWeapon launcher, int ID)
            : base(null, new Point3d(attacker.cmbt_loc), new Point3d(attacker.cmbt_vel), ID, "SKR")
        {
			WorkingObject = this;
            SeekingWeaponInfo skrinfo = (SeekingWeaponInfo)launcher.weapon.Template.ComponentTemplate.WeaponInfo;
			Hitpoints = MaxHitpoints = skrinfo.SeekerDurability;
            cmbt_mass = (Fix16)Hitpoints;//(Fix16)s.MaxHitpoints; // sure why not?
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

        #region fields & properties
        public CombatTakeFireEvent seekertargethit { get; set; }

        //the component that fired the missile.
        public CombatWeapon launcher { get; private set; }

		public WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Seeker; }
		}

		public int Hitpoints
		{
			get;
			set;
		}

		public int NormalShields
		{
			get;
			set;
		}

		public int PhasedShields
		{
			get;
			set;
		}

		public int MaxHitpoints
		{
			get;
			private set;
		}

		public int MaxNormalShields
		{
			get { return 0; }
		}

		public int MaxPhasedShields
		{
			get { return 0; }
		}

		public int ShieldHitpoints
		{
			get { return NormalShields + PhasedShields; }
		}

		public int ArmorHitpoints
		{
			get { return 0; }
		}

		public int HullHitpoints
		{
			get { return Hitpoints; }
		}

		public int MaxShieldHitpoints
		{
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxArmorHitpoints
		{
			get { return 0; }
		}

		public int MaxHullHitpoints
		{
			get { return MaxHitpoints; }
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}

		public int HitChance
		{
			get { return 1; }
		}

		public Empire Owner
		{
			// seeker owner is irrelevant outside of combat, and we have CombatEmpire for that
			get { return null; }
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public int Evasion
		{
			get
			{
				// TODO - per-seeker evasion settings
				return Mod.Current.Settings.SeekerEvasion;
			}
		}

        #endregion

        #region methods & functions

        public override void renewtoStart()
        {
            //do nothing. this should not ever happen here.
        }

		public void ReplenishShields()
		{
			// seekers don't have shields
		}

		public int? Repair(int? amount = null)
		{
			if (amount == null)
			{
				Hitpoints = MaxHitpoints;
				return null;
			}
			if (amount + Hitpoints > MaxHitpoints)
			{
				amount -= (MaxHitpoints - Hitpoints);
				Hitpoints = MaxHitpoints;
				return amount;
			}
			Hitpoints += amount.Value;
			return 0;
		}

		public int TakeDamage(DamageType dmgType, int damage, Battle battle)
		{
			// TODO - damage types
			if (damage > Hitpoints)
			{
				damage -= Hitpoints;
				Hitpoints = 0;
				return damage;
			}
			Hitpoints -= damage;
			return 0;
		}

		public void Dispose()
		{
			Hitpoints = 0;
			IsDisposed = true;
		}

		public override int handleShieldDamage(int damage)
		{
			// seekers don't have shields, just leak the damage
			return damage;
		}

		public override int handleComponentDamage(int damage, DamageType damageType, PRNG attackersdice)
		{
			return TakeDamage(damageType, damage, null);
		}

        #endregion
	}
}
