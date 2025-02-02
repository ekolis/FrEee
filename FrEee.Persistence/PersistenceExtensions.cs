using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Persistence;
public static class PersistenceExtensions
{
	public static SafeDictionary<string, object> GetData(this object o, ObjectGraphContext context)
	{
		// serialize object type and field count
		if (o is IDataObject)
		{
			// use data object code! :D
			var dobj = (IDataObject)o;
			return dobj.Data;
		}
		else if (o != null)
		{
			// use reflection :(
			var dict = new SafeDictionary<string, object>();
			var props = ObjectGraphContext.GetKnownProperties(o.GetType()).Values.Where(p => !p.GetValue(o, null).SafeEquals(p.PropertyType.DefaultValue()));
			foreach (var p in props)
				dict[p.Name] = p.GetValue(o);
			return dict;
		}
		else
			return new SafeDictionary<string, object>();
	}

	public static void SetData(this object o, SafeDictionary<string, object> dict, ObjectGraphContext context)
	{
		if (context == null)
			context = new ObjectGraphContext();
		if (o is IDataObject)
		{
			// use data object code! :D
			var dobj = (IDataObject)o;
			dobj.Data = dict;
		}
		else if (o != null)
		{
			// use reflection :(
			foreach (var kvp in dict)
			{
				var pname = kvp.Key;
				var val = kvp.Value;
				var prop = ObjectGraphContext.GetKnownProperties(o.GetType())[pname];
				if (prop != null)
				{
					try
					{
						context.SetObjectProperty(o, prop, val);
					}
					catch (NullReferenceException)
					{
						if (o == null && prop == null)
							Console.Error.WriteLine($"Attempted to set unknown property {pname} on a null object.");
						else if (o == null)
							Console.Error.WriteLine($"Attempted to set property {pname} on a null object.");
						else if (prop == null)
							Console.Error.WriteLine($"Attempted to set unknown property {pname} on {o}.");
						else
							throw;
					}
					catch (InvalidCastException)
					{
						Console.Error.WriteLine($"Could not set property {pname} of object {o} of type {o.GetType()} to value {val} of type {val.GetType()}.");
						throw;
					}
				}
				else
					Console.Error.WriteLine($"Found unknown property {pname} in serialized data for object type {o.GetType()}.");
			}
		}
		else
			throw new NullReferenceException("Can't set data on a null object.");
	}
}
