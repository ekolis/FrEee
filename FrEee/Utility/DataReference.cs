using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FrEee.Utility
{
	[Serializable]
	public class DataReference<T> : IDataReference, IReference<T>
	{
		public DataReference()
		{
			Context = new ObjectGraphContext();
		}

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

		[JsonIgnore]
		public ObjectGraphContext Context { get; set; }

		public string Data
		{
			get; set;
		}

		public bool HasValue
		{
			get
			{
				var maxid = Context.KnownObjects[typeof(T)].Count - 1;
				return ID <= maxid;
			}
		}

		[JsonIgnore]
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

		[JsonIgnore]
		public T Value
		{
			get
			{
				var kobjs = Context.KnownObjects[typeof(T)];
				if (kobjs == null)
					kobjs = Context.KnownObjects[typeof(T)] = new List<object>();
				if (kobjs.Count > ID)
					return (T)kobjs[ID];
				else if (kobjs.Count == ID)
				{
					var o = typeof(T).Instantiate();
					Context.Add(o);
					return (T)o;
				}
				else
					throw new Exception($"Too high ID {ID} specified for object of type {typeof(T)}; there are not enough objects.");
			}
			set
			{
				if (Context.GetID(value) == null)
					ID = Context.Add(value);
			}
		}

		object IData.Value
		{
			get
			{
				return Value;
			}
		}

		public static implicit operator T(DataReference<T> reference)
		{
			return reference.Value;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// nothing to do here, this is just used for serialization and such
		}

		// can't implicitly convert objects to references because we need a object graph context
	}

	public interface IDataReference : IData
	{
		ObjectGraphContext Context { get; set; }
		int ID { get; set; }
	}
}