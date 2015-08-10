using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static FrEee.Utility.Extensions.CommonExtensions;

namespace FrEee.Utility
{
	/// <summary>
	/// A data object which breaks objects down into scalars and references.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SimpleDataObject<T> : MarshalByRefObject, ISimpleDataObject
	{
		public SimpleDataObject()
		{

		}

		public SimpleDataObject(T t, ObjectGraphContext ctx = null)
		{
			Context = ctx;
			if (t != null)
				Data = t.GetData(Context);
			else
				Data = new SafeDictionary<string, object>();
		}

		public SimpleDataObject(SafeDictionary<string, IData<object>> simpleData, ObjectGraphContext ctx = null)
		{
			SimpleData = simpleData;
			Context = ctx;
		}

		private ObjectGraphContext Context;

		public SafeDictionary<string, IData<object>> SimpleData
		{
			get; set;
		}

		public SafeDictionary<string, object> Data
		{
			get
			{
				var dict = new SafeDictionary<string, object>();
				foreach (var pname in SimpleData.Keys.ExceptSingle("$type"))
					dict[pname] = SimpleData[pname]?.Value;
				return dict;
			}
			set
			{
				SimpleData = new SafeDictionary<string, IData<object>>();
				SimpleData["$type"] = new DataScalar(new SafeType(typeof(T)).Name);
				if (Context == null)
					Context = new ObjectGraphContext();
				foreach (var pname in value.Keys)
				{
					var pval = value[pname];
					if (pval == null)
						SimpleData[pname] = null;
					else if (pval.GetType().IsScalar())
						SimpleData[pname] = new DataScalar(pval);
					else
						SimpleData[pname] = (IData<object>)typeof(DataReference<>).MakeGenericType(pval.GetType()).Instantiate(Context, pval);
				}
			}
		}

		public T Value
		{
			get
			{
				var t = Instantiate<T>();
				t.SetData(Data, Context);
				return t;
			}
			set
			{
				Data = value.GetData(Context);
			}
		}

		public static implicit operator T(SimpleDataObject<T> d)
		{
			return d.Value;
		}
	}

	public interface ISimpleDataObject : IDataObject
	{
		SafeDictionary<string, IData<object>> SimpleData
		{
			get; set;
		}
	}

	/// <summary>
	/// Loads simple data objects from dictionaries and such.
	/// </summary>
	public static class SimpleDataObject
	{
		public static ISimpleDataObject Load(SafeDictionary<string, IData<object>> simpleData, ObjectGraphContext ctx = null)
		{
			var t = new SafeType(simpleData["$type"].Value as string).Type;
			return (ISimpleDataObject)typeof(SimpleDataObject<>).MakeGenericType(t).Instantiate(simpleData, ctx);
		}
	}
}
