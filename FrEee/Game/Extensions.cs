using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace FrEee.Game
{
	public static class Extensions
	{
		/// <summary>
		/// Clones an object.
		/// </summary>
		/// <typeparam name="T">The type of object to clone.</typeparam>
		/// <param name="obj">The object to clone.</param>
		/// <returns>The clone.</returns>
		public static T Clone<T>(this T obj) where T : new()
		{
			return Mapper.Map(obj, new T());
		}
	}
}
