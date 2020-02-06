using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace FrEee.Utility.Extensions
{
	public static class ChecksExtensions
	{
		private static SafeDictionary<MemberInfo, IEnumerable<Attribute>> attributeCache = new SafeDictionary<MemberInfo, IEnumerable<Attribute>>();

		private static SafeDictionary<Type, IEnumerable<Type>> interfaceCache = new SafeDictionary<Type, IEnumerable<Type>>();

		private static SafeDictionary<Type, IEnumerable<MemberInfo>> memberCache = new SafeDictionary<Type, IEnumerable<MemberInfo>>();

		/// <summary>
		/// Checks for attributes in a class or its interfaces.
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="attributeType"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetAttributes<T>(this MemberInfo mi) where T : Attribute
		{
			if (attributeCache[mi] == null)
				attributeCache[mi] = Attribute.GetCustomAttributes(mi).ToArray();
			var atts = attributeCache[mi].OfType<T>();
			foreach (var att in atts)
				yield return att;
			if (interfaceCache[mi.DeclaringType] == null)
				interfaceCache[mi.DeclaringType] = mi.DeclaringType.GetInterfaces();
			foreach (var i in interfaceCache[mi.DeclaringType])
			{
				if (memberCache[i] == null)
					memberCache[i] = i.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToArray(); // TODO - refactor into method
				var mi2 = memberCache[i].SingleOrDefault(x => x.MemberType == mi.MemberType && x.Name == mi.Name);
				if (mi2 != null)
				{
					foreach (var att2 in mi2.GetAttributes<T>())
						yield return att2;
				}
			}
		}

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
		/// Checks for attributes in a class or its interfaces.
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="attributeType"></param>
		/// <returns></returns>
		public static bool HasAttribute<T>(this MemberInfo mi)
		{
			return mi.HasAttribute(typeof(T));
		}

		/// <summary>
		/// Checks for attributes in a class or its interfaces.
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="attributeType"></param>
		/// <returns></returns>
		public static bool HasAttribute(this MemberInfo mi, Type attributeType, bool checkInterfaces = true)
		{
			if (attributeCache[mi] == null)
				attributeCache[mi] = Attribute.GetCustomAttributes(mi).ToArray();
			if (attributeCache[mi].Where(a => attributeType.IsAssignableFrom(a.GetType())).Any())
				return true;
			var dt = mi is Type ? mi as Type : mi.DeclaringType;
			if (checkInterfaces)
			{
				if (interfaceCache[dt] == null)
					interfaceCache[dt] = dt.GetInterfaces();
				foreach (var i in interfaceCache[dt])
				{
					if (memberCache[i] == null)
						memberCache[i] = i.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToArray(); // TODO - refactor into method
					if (memberCache[i].Any(m => m.Name == mi.Name && m.MemberType == mi.MemberType && m.HasAttribute(attributeType, false))) // no need to check interfaces of interfaces, they're already listed by GetInterfaces
						return true;
				}
			}
			return false;
		}

		public static bool HasProperty(this ExpandoObject obj, string propertyName)
		{
			return obj.GetType().GetProperty(propertyName) != null;
		}

		/// <summary>
		/// Does this object's ID match what the galaxy says it is?
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public static bool HasValidID(this IReferrable r)
		{
			return Galaxy.Current.referrables.ContainsKey(r.ID) && Galaxy.Current.referrables[r.ID] == r;
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
				if (sobj.StarSystem.Name == "Citronelle")
				{

				}
				var so = sys.GetEmpireAbilityValue(sobj.Owner, "System - Sight Obscuration");
				obscurationLevel = new[]
				{
					sys.GetAbilityValue("System - Sight Obscuration"),
					so,
					sec.GetAbilityValue("Sector - Sight Obscuration"),
					sec.GetEmpireAbilityValue(sobj.Owner, "Sector - Sight Obscuration"),
				}.Max(a => a.ToInt());
			}
			return (cloaks.Any() || obscurationLevel > 0) && joined.All(j => j.CloakLevel > j.SensorLevel || obscurationLevel > j.SensorLevel);
		}

		/// <summary>
		/// Is this order a new order added this turn, or one the server already knows about?
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public static bool IsNew(this IOrder order)
		{
			return Galaxy.Current.Referrables.OfType<AddOrderCommand>().Where(cmd => cmd.Order == order).Any();
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

		public static bool IsValid(this IErrorProne obj)
		{
			return !obj.Errors.Any();
		}

		public static bool IsWarhead(this WeaponTypes wt)
		{
			return wt == WeaponTypes.Warhead || wt == WeaponTypes.WarheadPointDefense;
		}

		public static bool StillExists<T>(this T old, IEnumerable<T> oldItems, IEnumerable<T> nuItems)
			where T : IModObject
		{
			var match = nuItems.FindByModID(old.ModID);
			if (match != null)
				return true;
			match = nuItems.FindByTypeNameIndex(old.GetType(), old.Name, oldItems.GetIndex(old));
			return match != null;
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
			return sobj.IsMemory && Empire.Current == null && (sobj.ID == 0 || Galaxy.Current.referrables.ContainsKey(sobj.ID));
		}
	}
}