using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Technology;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Gameplay.Commands.Orders;
namespace FrEee.Extensions;

public static class ChecksExtensions
{
	/// <summary>
	/// Determines if an object has a specified ability.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="abilityName"></param>
	/// <returns></returns>
	public static bool HasAbility(this IAbilityObject obj, string abilityName, bool includeShared = true)
	{
		IEnumerable<Ability> abils;
		if (includeShared && obj is IOwnableAbilityObject)
			abils = obj.UnstackedAbilities(true).Union(obj.SharedAbilities());
		else
			abils = obj.UnstackedAbilities(true);
		return abils.Any(abil => abil.Rule != null && abil.Rule.Matches(abilityName));
	}

	/// <summary>
	/// Determines if a common ability object has a specified ability for an empire.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="abilityName"></param>
	/// <returns></returns>
	public static bool HasAbility(this ICommonAbilityObject obj, string abilityName, Empire emp, bool includeShared = true)
	{
		IEnumerable<Ability> abils;
		if (includeShared)
			abils = obj.EmpireAbilities(emp).Union(obj.SharedAbilities(emp));
		else
			abils = obj.EmpireAbilities(emp);
		return abils.Any(abil => abil.Rule != null && abil.Rule.Matches(abilityName));
	}

	/// <summary>
	/// Does this object's ID match what the galaxy says it is?
	/// </summary>
	/// <param name="r"></param>
	/// <returns></returns>
	public static bool HasValidID(this IReferrable r)
	{
		return r.ID > 0;
	}

	/// <summary>
	/// Is this type safe to pass from the client to the server?
	/// Primitives, strings, points and colors are client safe.
	/// So are types implementing IPromotable.
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public static bool IsClientSafe(this Type t)
	{
		return
			t.IsPrimitive ||
			t == typeof(string) ||
			t == typeof(Point) ||
			t == typeof(Color) ||
			typeof(IEnumerable<object>).IsAssignableFrom(t) ||
			typeof(IEnumerable).IsAssignableFrom(t) ||
			typeof(IPromotable).IsAssignableFrom(t) ||
			t.BaseType != null && t.BaseType.IsClientSafe() ||
			t.GetInterfaces().Any(i => i.IsClientSafe());
	}

	public static bool IsDirectFire(this WeaponTypes wt)
	{
		return wt == WeaponTypes.DirectFire || wt == WeaponTypes.DirectFirePointDefense;
	}

	/// <summary>
	/// Tests for divisibility.
	/// </summary>
	/// <param name="dividend"></param>
	/// <param name="divisor"></param>
	/// <param name="treatZeroAsOne"></param>
	/// <returns></returns>
	public static bool IsDivisibleBy(this int dividend, int divisor)
	{
		return dividend % divisor == 0;
	}

	/// <summary>
	/// Is this space object hidden from view of an empire due to cloaking?
	/// Space objects are hidden from view if they have at least one cloaking ability, and
	/// all cloaking abilities they possess outrank the appropriate sensors possessed by the empire in the system.
	/// However a space object must possess at least one cloaking ability to actually be cloaked.
	/// </summary>
	/// <remarks>
	/// Unlike in SE4, where cloaks must outrank sensors in five specific sight types,
	/// in FrEee, sight types are just custom strings in the data files, so cloaks must only outrank sensors
	/// in sight types that the viewing empire actually possesses in the system.
	/// Thus, a level 2 temporal cloak will hide you from enemy sight on its own, even if you don't have a
	/// level 2 psychic cloak or a level 2 foobar cloak or whatever. However if the enemy has a level 2 temporal
	/// sensor, or a level 1 sensor of any type but temporal, then they can see you.
	/// </remarks>
	/// <param name="sobj"></param>
	/// <param name="emp"></param>
	/// <returns></returns>
	public static bool IsHiddenFrom(this ISpaceObject sobj, Empire emp)
	{
		var sys = sobj.StarSystem;
		var sec = sobj.Sector;
		var sensors = sys.EmpireAbilities(emp).Where(a => a.Rule.Name == "Sensor Level");
		var cloaks = sobj.Abilities().Where(a => a.Rule.Name == "Cloak Level");
		var joined = from sensor in sensors
					 join cloak in cloaks on sensor.Value1.Value equals cloak.Value1.Value into gj
					 from subcloak in gj.DefaultIfEmpty()
					 select new
					 {
						 SightType = sensor.Value1.Value,
						 SensorLevel = sensor.Value2.Value.ToInt(),
						 CloakLevel = subcloak == null ? 0 : subcloak.Value2.Value.ToInt(),
					 };

		int obscurationLevel = 0;
		if (sobj.CanBeObscured)
		{
			var so = sys.GetEmpireAbilityValue(sobj.Owner, "System - Sight Obscuration");
			obscurationLevel = new[]
			{
				sys.GetAbilityValue("System - Sight Obscuration"),
				so,
				sec.GetAbilityValue("Sector - Sight Obscuration"),
				sec.GetEmpireAbilityValue(sobj.Owner, "Sector - Sight Obscuration"),
			}.Max(a => a.ToInt());
		}
		return (cloaks.Any() || obscurationLevel > 0) 
			&& joined.All(j =>
				j.CloakLevel > j.SensorLevel || obscurationLevel > j.SensorLevel);
	}

	/// <summary>
	/// Is this order a new order added this turn, or one the server already knows about?
	/// </summary>
	/// <param name="order"></param>
	/// <returns></returns>
	public static bool IsNew(this IOrder order)
	{
		return Game.Current.Referrables.OfType<IAddOrderCommand>().Where(cmd => cmd.Order == order).Any();
	}

	public static bool IsPointDefense(this WeaponTypes wt)
	{
		return wt == WeaponTypes.DirectFirePointDefense || wt == WeaponTypes.SeekingPointDefense || wt == WeaponTypes.WarheadPointDefense;
	}

	public static bool IsScalar(this Type t)
	{
		return t.IsPrimitive || t.IsEnum || t == typeof(string);
	}

	public static bool IsSeeking(this WeaponTypes wt)
	{
		return wt == WeaponTypes.Seeking || wt == WeaponTypes.SeekingPointDefense;
	}

	public static void IssueOrder(this IOrderable obj, IOrder order)
	{
		if (obj.Owner != Empire.Current)
			throw new Exception("Cannot issue orders to another empire's objects.");
		Empire.Current.IssueOrder(obj, order);
	}

	public static bool IsWarhead(this WeaponTypes wt)
	{
		return wt == WeaponTypes.Warhead || wt == WeaponTypes.WarheadPointDefense;
	}

	/// <summary>
	/// Slower implementation of ContainsKey for dictionaries, for use in Equals/GetHashCode debugging
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dict">The dictionary.</param>
	/// <param name="key">The key.</param>
	/// <returns></returns>
	public static bool TestContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		where TKey: notnull
	{
		var hash = EqualityComparer<TKey>.Default.GetHashCode(key);
		return dict.Any(
			kvp => hash == EqualityComparer<TKey>.Default.GetHashCode(kvp.Key)

				&& EqualityComparer<TKey>.Default.Equals(kvp.Key, key)
		);
	}

	public static bool UpgradesTo<T>(this T self, T other) where T : IUpgradeable<T>
	{
		return self.NewerVersions.Contains(other);
	}

	/// <summary>
	/// Determines if this object is a memory of a known object.
	/// </summary>
	/// <param name="sobj">The object to check.</param>
	/// <returns>true if it is a memory of a known object, otherwise false.</returns>
	public static bool IsMemoryOfKnownObject(this ISpaceObject sobj)
	{
		return sobj.IsMemory && Empire.Current == null && (sobj.ID == 0 || Game.Current.GetReferrable(sobj.ID) is not null);
	}

	/// <summary>
	/// Determines the level of visibility of a space object to an empire.
	/// <br/>
	/// If you only need to verify that a space object has at least a certain level of visibility,
	/// it is less expensive to call <see cref="HasVisibility(ISpaceObject, Empire, Visibility)"/>.
	/// </summary>
	/// <param name="sobj">The space object to check.</param>
	/// <param name="emp">The empire to check against.</param>
	/// <returns>The visibility level.</returns>
	public static Visibility CheckSpaceObjectVisibility(this ISpaceObject sobj, Empire emp)
	{
		bool hasMemory = false;
		if (sobj.IsMemory)
		{
			var mowner = sobj.MemoryOwner();
			if (mowner == emp || mowner == null)
				return Visibility.Fogged;
			else
				return Visibility.Unknown; // can't see other players' memories
		}
		else
		{
			var mem = sobj.FindMemory(emp);
			if (mem != null)
				hasMemory = true;
		}

		if (emp == sobj.Owner)
			return Visibility.Owned;

		// You can always scan space objects you are in combat with.
		// But only their state at the time they were in combat; not for the rest of the turn!
		// TODO - what about glassed planets, they have no owner...
		if (Game.Current.Battles.Any(b =>
		(b.Combatants.OfType<ISpaceObject>().Contains(sobj)
			|| b.StartCombatants.Values.OfType<ISpaceObject>().Contains(sobj)
			|| b.EndCombatants.Values.OfType<ISpaceObject>().Contains(sobj))
		&& b.Combatants.Any(c => c.Owner == emp)))
			return Visibility.Scanned;

		// do we have anything that can see it?
		var sys = sobj.StarSystem;
		if (sys == null)
			return Visibility.Unknown;
		var seers = sys.FindSpaceObjects<ISpaceObject>(s => s.Owner == emp && !s.IsMemory);
		if (!seers.Any() || sobj.IsHiddenFrom(emp))
		{
			if (Game.Current.Setup.OmniscientView && sobj.StarSystem.ExploredByEmpires.Contains(emp))
				return Visibility.Visible;
			if (emp.AllSystemsExploredFromStart)
				return Visibility.Fogged;
			var known = emp.Memory[sobj.ID];
			if (known != null && sobj.GetType() == known.GetType())
				return Visibility.Fogged;
			else if (Game.Current.Battles.Any(b => b.Combatants.Any(c => c.ID == sobj.ID) && b.Combatants.Any(c => c.Owner == emp)))
				return Visibility.Fogged;
			else if (hasMemory)
				return Visibility.Fogged;
			else
				return Visibility.Unknown;
		}
		if (!sobj.HasAbility("Scanner Jammer"))
		{
			var scanners = seers.Where(s =>
				s.HasAbility("Long Range Scanner") && s.GetAbilityValue("Long Range Scanner").ToInt() >= s.Sector.Coordinates.EightWayDistance(sobj.FindSector().Coordinates)
				|| s.HasAbility("Long Range Scanner - System"));
			if (scanners.Any())
				return Visibility.Scanned;
		}
		return Visibility.Visible;
	}

	/// <summary>
	/// Checks for a specific visibility level of a space object for an empire.
	/// If the object has that level of visibility or higher, we return true.
	/// </summary>
	/// <remarks>
	/// This allows us to short circuit the more expensive scanner code.
	/// See also parallel code at <see cref="CheckSpaceObjectVisibility(ISpaceObject, Empire)"/>
	/// </remarks>
	/// <param name="sobj">The space object to check.</param>
	/// <param name="emp">The empire to check against.</param>
	/// <param name="desiredVisibility">The requested visibility level.</param>
	/// <returns>true if the space object has at least the requested visibility level, otherwise false.</returns>
	public static bool HasVisibility(this ISpaceObject sobj, Empire emp, Visibility desiredVisibility)
	{
		bool hasMemory = false;
		if (sobj.IsMemory)
		{
			var mowner = sobj.MemoryOwner();
			if (mowner == emp || mowner == null)
				return Visibility.Fogged >= desiredVisibility;
			else
				return Visibility.Unknown >= desiredVisibility; // can't see other players' memories
		}
		else
		{
			var mem = sobj.FindMemory(emp);
			if (mem != null)
				hasMemory = true;
		}

		if (emp == sobj.Owner)
			return Visibility.Owned >= desiredVisibility;

		// You can always scan space objects you are in combat with.
		// But only their state at the time they were in combat; not for the rest of the turn!
		// TODO - what about glassed planets, they have no owner...
		if (Game.Current.Battles.Any(b =>
		(b.Combatants.OfType<ISpaceObject>().Contains(sobj)
			|| b.StartCombatants.Values.OfType<ISpaceObject>().Contains(sobj)
			|| b.EndCombatants.Values.OfType<ISpaceObject>().Contains(sobj))
		&& b.Combatants.Any(c => c.Owner == emp)))
			return Visibility.Scanned >= desiredVisibility;

		// do we have anything that can see it?
		var sys = sobj.StarSystem;
		if (sys == null)
			return Visibility.Unknown >= desiredVisibility;
		var seers = sys.FindSpaceObjects<ISpaceObject>(s => s.Owner == emp && !s.IsMemory);
		if (!seers.Any() || sobj.IsHiddenFrom(emp))
		{
			if (Game.Current.Setup.OmniscientView && sobj.StarSystem.ExploredByEmpires.Contains(emp))
				return Visibility.Visible >= desiredVisibility;
			if (emp.AllSystemsExploredFromStart)
				return Visibility.Fogged >= desiredVisibility;
			var known = emp.Memory[sobj.ID];
			if (known != null && sobj.GetType() == known.GetType())
				return Visibility.Fogged >= desiredVisibility;
			else if (Game.Current.Battles.Any(b => b.Combatants.Any(c => c.ID == sobj.ID) && b.Combatants.Any(c => c.Owner == emp)))
				return Visibility.Fogged >= desiredVisibility;
			else if (hasMemory)
				return Visibility.Fogged >= desiredVisibility;
			else
				return Visibility.Unknown >= desiredVisibility;
		}

		// short circuit scanner code: we now know that the object is either visible or scanned
		// so if we only care if it's visible or less, we can skip the scanner check.
		if (Visibility.Visible >= desiredVisibility)
			return true;

		if (!sobj.HasAbility("Scanner Jammer"))
		{
			var scanners = seers.Where(s =>
				s.HasAbility("Long Range Scanner") && s.GetAbilityValue("Long Range Scanner").ToInt() >= s.Sector.Coordinates.EightWayDistance(sobj.FindSector().Coordinates)
				|| s.HasAbility("Long Range Scanner - System"));
			if (scanners.Any())
				return Visibility.Scanned >= desiredVisibility;
		}

		// we know that the requested visibility level is greater than "visible"
		// and the object has a scanner jammer
		// therefore it can't be scanned, and we already know it's not owned, we checked that up top
		// so we can just return false because the requested visibility (scanned or owned) can't be met
		return false;
	}
}
