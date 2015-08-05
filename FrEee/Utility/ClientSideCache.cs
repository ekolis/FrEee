using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Utility
{
	/// <summary>
	/// A client side cache for data.
	/// Can also cache data server side when a flag is enabled.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ClientSideCache<T>
	{
		public ClientSideCache(Func<T> compute)
		{
			this.compute = compute;
		}

		private Func<T> compute;

		private bool isServerSideCacheEnabled;

		public bool IsServerSideCacheEnabled
		{
			get
			{
				return isServerSideCacheEnabled;
			}
			set
			{
				isServerSideCacheEnabled = value;
				if (!isServerSideCacheEnabled)
					isDirty = true;
			}
		}

		public bool IsCacheEnabled
		{
			get
			{
				return IsServerSideCacheEnabled || Empire.Current != null;
			}
		}

		private bool isDirty = true;

		private T value;

		public T Value
		{
			get
			{
				if (IsCacheEnabled)
				{
					if (!isDirty)
						return value;
					else
					{
						value = compute();
						isDirty = false;
						return value;
					}
				}
				else
					return compute();
			}
		}

		public static implicit operator T(ClientSideCache<T> cache)
		{
			return cache.Value;
		}
	}
}
