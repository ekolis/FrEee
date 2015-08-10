﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility.Extensions;
using Newtonsoft.Json;
using NJS = Newtonsoft.Json.JsonSerializer;

namespace FrEee.Utility
{
	public class JsonSerializer
	{
		static JsonSerializer()
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
				Formatting = Formatting.Indented,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				ObjectCreationHandling = ObjectCreationHandling.Auto,
				TypeNameHandling = TypeNameHandling.All,
				TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
			};
		}

		public string SerializeToString(object o)
		{
			var ctx = new ObjectGraphContext();
			var kos = new List<object>();
			var parser = new ObjectGraphParser();
			if (o == null)
			{
				// do nothing
			}
			else if (o.GetType().IsScalar())
				kos.Add(DataScalar.Create(o));
			else
			{
				Action<object> Parser_StartObject = x =>
				{
					if (!x.GetType().IsScalar())
					{
						var dobj = SimpleDataObject.Create(x, ctx);
						kos.Add(dobj);
					}
				};
				parser.StartObject += new ObjectGraphParser.ObjectDelegate(Parser_StartObject);
				parser.Parse(o);
			}
			foreach (var dobj in kos.OfType<ISimpleDataObject>())
				dobj.InitializeData(ctx);
			return JsonConvert.SerializeObject(kos);
		}

		public object DeserializeFromString(string s)
		{
			var list = JsonConvert.DeserializeObject<List<object>>(s);
			if (list.Count == 0)
				return null;
			var first = list[0];
			if (first == null)
				return null;
			// TODO - make IData and ISimpleDataObject implement a common interface
			if (first is ISimpleDataObject)
			{
				var f = first as ISimpleDataObject;
				foreach (var item in list)
				{
					if (item is ISimpleDataObject)
					{
						var dobj = item as ISimpleDataObject;
						dobj.Context = f.Context;
						foreach (var r in dobj.SimpleData.Values.OfType<IDataReference>())
							r.Context = f.Context;
					}
					if (item is IData)
						f.Context.Add((item as IData).Value);
				}
			}
			foreach (var item in list.OfType<ISimpleDataObject>())
				item.InitializeValue();
			if (first is ISimpleDataObject)
				return (first as ISimpleDataObject).Value;
			if (first is IData)
				return (first as IData).Value;
			return first;
		}
	}
}
