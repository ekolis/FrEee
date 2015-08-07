using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Game.Interfaces;

namespace FrEee.Utility
{
	public class DataReference<T> : IData, IReference<T>
	{
		public DataReference(ObjectGraphContext ctx)
		{
			Context = ctx;
		}

		private ObjectGraphContext Context;

		public string Data
		{
			get; set;
		}

		public int ID
		{
			get
			{
				return int.Parse(Data);
			}
			set
			{
				Data = value.ToString();
			}
		}

		public bool HasValue
		{
			get
			{
				var maxid = Context.KnownObjects[typeof(T)].Count - 1;
				return ID <= maxid;
			}
		}

		public T Value
		{
			get
			{
				return (T)Context.KnownObjects[typeof(T)][ID];
			}
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// nothing to do here, this is just used for serialization and such
		}
	}
}
