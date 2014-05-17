using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Tests.Modding
{
	/// <summary>
	/// The stock FrEee mod.
	/// </summary>
	public static class StockMod
	{
		public static Mod Instance
		{
			get
			{
				if (instance == null)
					instance = Mod.Load(null);
				return instance;
			}
		}

		private static Mod instance;
	}
}
