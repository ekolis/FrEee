using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;

namespace FrEee.Tests
{
	public static class TestUtilities
	{
		public static IHull<T> CreateHull<T>(string name)
			where T : IVehicle
		{
			IHull<T> hull = new Hull<T>();
			hull.Name = name;
			hull.ModID = name;
			Mod.Current.Hulls.Add(hull);
			return hull;
		}

		public static IHull<T> CreateHull<T>(this IDesign<T> design, string name = null)
			where T : IVehicle
		{
			var hull = CreateHull<T>(name ?? design.BaseName);
			design.Hull = hull;
			return hull;
		}
	}
}
