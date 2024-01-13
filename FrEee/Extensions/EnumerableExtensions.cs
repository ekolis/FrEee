using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrEee.Utility; using FrEee.Serialization;

namespace FrEee.Extensions
{
	/// <summary>
	/// All extensions dealing strictly with generic enumerables.
	/// </summary>
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> ConcatSingle<T>(this T t1, T t2)
		{
			return t1.SingleItem().Append(t2);
		}

		public static IEnumerable<T> ExceptSingle<T>(this IEnumerable<T> src, T badguy)
		{
			return src.Except(new T[] { badguy });
		}

		/// <summary>
		/// Flattens groupings into a single sequence.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="lookup"></param>
		/// <returns></returns>
		public static IEnumerable<TValue> Flatten<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> lookup)
		{
			return lookup.SelectMany(g => g);
		}

		public static int IndexOf<T>(this IEnumerable<T> haystack, T needle)
		{
			int i = 0;
			foreach (var item in haystack)
			{
				if (item.Equals(needle))
					return i;
				i++;
			}
			return -1;
		}

		public static T MaxOrDefault<T>(this IEnumerable<T> stuff)
		{
			if (!stuff.Any())
				return default(T);
			return stuff.Max();
		}

		public static TProp MaxOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector)
		{
			return stuff.Select(selector).MaxOrDefault();
		}

		public static T MinOrDefault<T>(this IEnumerable<T> stuff)
		{
			if (!stuff.Any())
				return default(T);
			return stuff.Min();
		}

		public static TProp MinOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector)
		{
			return stuff.Select(selector).MinOrDefault();
		}

		/// <summary>
		/// Finds the next item in a list, or null if there is no next item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static T Next<T>(this IEnumerable<T> list, T item, bool wrap = false)
		{
			var index = list.IndexOf(item) + 1;
			if (index >= list.Count())
			{
				if (wrap)
					return list.FirstOrDefault();
				else
					return default(T);
			}
			else
				return list.ElementAt(index);
		}

		/// <summary>
		/// Converts an enumeration to an array, then does something to each item in a parallel fashion.
		/// </summary>
		/// <param name="items"></param>
		/// <param name="action"></param>
		public static void ParallelSafeForeach<T>(this IEnumerable<T> items, Action<T> action)
		{
			if (items != null && action != null)
			{
				Parallel.ForEach(items.ToArray(), action);
			}
		}

		/// <summary>
		/// Picks a random element from a sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickRandom<T>(this IEnumerable<T> src, PRNG prng = null)
		{
			if (!src.Any())
				return default(T);
			return src.ElementAt(RandomHelper.Next(src.Count(), prng));
		}

		/// <summary>
		/// Picks a random element from a weighted sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickWeighted<T>(this IDictionary<T, int> src, PRNG prng = null)
		{
			var total = src.Sum(kvp => kvp.Value);
			int num;
			if (prng == null)
				num = RandomHelper.Next(total);
			else
				num = prng.Next(total);

			int sofar = 0;
			foreach (var kvp in src)
			{
				sofar += kvp.Value;
				if (num < sofar)
					return kvp.Key;
			}
			return default(T); // nothing to pick...
		}

		/// <summary>
		/// Picks a random element from a weighted sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickWeighted<T>(this IDictionary<T, long> src, PRNG prng = null)
		{
			var total = src.Sum(kvp => kvp.Value);
			long num;
			if (prng == null)
				num = RandomHelper.Next(total);
			else
				num = prng.Next(total);
			long sofar = 0;
			foreach (var kvp in src)
			{
				sofar += kvp.Value;
				if (num < sofar)
					return kvp.Key;
			}
			return default(T); // nothing to pick...
		}

		/// <summary>
		/// Picks a random element from a weighted sequence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T PickWeighted<T>(this IDictionary<T, double> src, PRNG prng = null)
		{
			var total = src.Sum(kvp => kvp.Value);
			double num;
			if (prng == null)
				num = RandomHelper.Next(total);
			else
				num = prng.Next(total);
			double sofar = 0;
			foreach (var kvp in src)
			{
				sofar += kvp.Value;
				if (num < sofar)
					return kvp.Key;
			}
			return default(T); // nothing to pick...
		}

		public static T PickWeighted<T>(this IEnumerable<T> src, Func<T, int> weighter, PRNG prng = null)
		{
			return src.ToDictionary(x => x, x => weighter(x)).PickWeighted(prng);
		}

		public static T PickWeighted<T>(this IEnumerable<T> src, Func<T, long> weighter, PRNG prng = null)
		{
			return src.ToDictionary(x => x, x => weighter(x)).PickWeighted(prng);
		}

		public static T PickWeighted<T>(this IEnumerable<T> src, Func<T, double> weighter, PRNG prng = null)
		{
			return src.ToDictionary(x => x, x => weighter(x)).PickWeighted(prng);
		}

		/// <summary>
		/// Finds the previous item in a list, or null if there is no previous item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static T Previous<T>(this IEnumerable<T> list, T item, bool wrap = false)
		{
			var index = list.IndexOf(item) - 1;
			if (index < 0)
			{
				if (wrap)
					return list.LastOrDefault();
				else
					return default(T);
			}
			else
				return list.ElementAt(index);
		}

		/// <summary>
		/// Converts an enumeration to an array, then does something to each item.
		/// </summary>
		/// <param name="items"></param>
		/// <param name="action"></param>
		public static void SafeForeach<T>(this IEnumerable<T> items, Action<T> action)
		{
			if (items != null && action != null)
			{
				foreach (var item in items.ToArray())
					action(item);
			}
		}

		/// <summary>
		/// Orders elements randomly.
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> src, PRNG prng = null)
		{
			return src.Select(value => (RandomHelper.Next(int.MaxValue, prng), value)).OrderBy(q => q.Item1).Select(q => q.value);
		}

		/// <summary>
		/// Creates an enumerable containing a single item.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IEnumerable<T> SingleItem<T>(this T obj)
		{
			return new T[] { obj };
		}

		/// <summary>
		/// "Squashes" a nested collection into a collection of tuples.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <returns></returns>
		public static IEnumerable<Tuple<TParent, TChild>> Squash<TParent, TChild>(this IEnumerable<TParent> parents, Func<TParent, IEnumerable<TChild>> childSelector)
		{
			foreach (var parent in parents)
			{
				foreach (var child in childSelector(parent))
					yield return Tuple.Create(parent, child);
			}
		}

		/// <summary>
		/// "Squashes" a nested lookup into a collection of tuples.
		/// </summary>
		/// <typeparam name="TKey1"></typeparam>
		/// <typeparam name="TKey2"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="lookup"></param>
		/// <returns></returns>
		public static IEnumerable<Tuple<TKey1, TKey2, TValue>> Squash<TKey1, TKey2, TValue>(this ILookup<TKey1, ILookup<TKey2, TValue>> lookup)
		{
			foreach (var group1 in lookup)
			{
				foreach (var sublookup in group1)
				{
					foreach (var group2 in sublookup)
					{
						foreach (var item in group2)
							yield return Tuple.Create(group1.Key, group2.Key, item);
					}
				}
			}
		}

		/// <summary>
		/// Orders elements randomly after another OrderBy or ThenBy clause.
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static IEnumerable<T> ThenShuffle<T>(this IOrderedEnumerable<T> src, PRNG prng = null)
		{
			return src.ThenBy(t => RandomHelper.Next(int.MaxValue, prng));
		}

		public static IEnumerable<T> UnionSingle<T>(this IEnumerable<T> ts, T t)
		{
			return ts.Union(t.SingleItem());
		}

		public static IEnumerable<T> UnionSingle<T>(this T t1, T t2)
		{
			return t1.SingleItem().UnionSingle(t2);
		}

		/// <summary>
		/// Returns the elements of a sequence that have the maximum of some selected value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TCompared"></typeparam>
		/// <param name="src"></param>
		/// <param name="getter"></param>
		/// <returns></returns>
		public static IEnumerable<T> WithMax<T, TCompared>(this IEnumerable<T> src, Func<T, TCompared> selector)
		{
			if (!src.Any())
				return Enumerable.Empty<T>();
			var list = src.Select(item => new { Item = item, Value = selector(item) });
			var max = list.Max(x => x.Value);
			return list.Where(x => x.Value.SafeEquals(max)).Select(x => x.Item);
		}

		/// <summary>
		/// Returns the elements of a sequence that have the minimum of some selected value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TCompared"></typeparam>
		/// <param name="src"></param>
		/// <param name="getter"></param>
		/// <returns></returns>
		public static IEnumerable<T> WithMin<T, TCompared>(this IEnumerable<T> src, Func<T, TCompared> selector)
		{
			if (!src.Any())
				return Enumerable.Empty<T>();
			var list = src.Select(item => new { Item = item, Value = selector(item) });
			var min = list.Min(x => x.Value);
			return list.Where(x => x.Value.SafeEquals(min)).Select(x => x.Item);
		}
	}
}
