using FrEee.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using FrEee.Objects.GameState;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Persistence;

namespace FrEee.Plugins.Default.Persistence;

[Serializable]
public class DataReference<T> : IDataReference, IReference<T>
{
	public DataReference()
	{
		Context = new ObjectGraphContext();
	}

	public DataReference(ObjectGraphContext ctx, T t = default)
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
	[field: NonSerialized]
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
			if (Context == null)
				Context = new ObjectGraphContext();
			var kobjs = Context.KnownObjects[typeof(T)];
			if (kobjs == null)
				kobjs = Context.KnownObjects[typeof(T)] = new List<object>();
			while (kobjs.Count <= ID)
			{
				var o = (T)typeof(T).Instantiate();
				Context.Add(o);
				// TODO - populate data in object somehow
			}
			return (T)kobjs[ID];
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

	public IPromotable ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		// nothing to do here, this is just used for serialization and such
		return this;
	}

	// can't implicitly convert objects to references because we need a object graph context
}

public interface IDataReference : IData
{
	ObjectGraphContext Context { get; set; }
	int ID { get; set; }
}
