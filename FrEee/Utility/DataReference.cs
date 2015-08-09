using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Game.Interfaces;

namespace FrEee.Utility
{
	public class DataReference<T> : IData<T>, IReference<T>
	{
		public DataReference(ObjectGraphContext ctx, T t = default(T))
		{
			Context = ctx;
			Value = t;
		}

		public DataReference(ObjectGraphContext ctx, int id = -1)
		{
			Context = ctx;
			ID = id;
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
			set
			{
				if (Context.GetID(value) == null)
					Context.Add(value);
				ID = Context.GetID(value).Value;
			}
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// nothing to do here, this is just used for serialization and such
		}

		public static implicit operator T(DataReference<T> reference)
		{
			return reference.Value;
		}

		// can't implicitly convert objects to references because we need a object graph context
	}
}
