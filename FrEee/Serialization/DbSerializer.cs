using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Modding;
using FrEee.Utility;
using static IronPython.Modules._ast;

namespace FrEee.Serialization
{
	/// <summary>
	/// Serializes objects using a database.
	/// </summary>
	public class DbSerializer
	{
		private const string TypeMarker = "$type";
		private const string ValueMarker = "$value";
		private const string ReferrableMarker = "!";
		private const string ModObjectMarker = "@";

		/// <summary>
		/// Serializes an object, but doesn't serialize referenced <see cref="IReferrable"/>s; just serializes their IDs.
		/// </summary>
		/// <param name="root">The object to serialize.</param>
		/// <returns>The seriaized data.</returns>
		public string SerializePartial(object root)
		{
			if (root is IReferrable referrable)
			{
				var dict = new Dictionary<string, object>();
				var context = new ObjectGraphContext();
				foreach (var kvp in root.GetData(context))
				{
					if (kvp.Value is IReferrable subreferrable)
					{
						// save ID of referrable
						dict.Add(ReferrableMarker + kvp.Key, subreferrable.ID.ToString());
					}
					else if (kvp.Value is IModObject modObject)
					{
						// save ID of mod object
						dict.Add(ModObjectMarker + kvp.Key, modObject.ModID);
					}
					else
					{
						// save serialized string of property value
						dict.Add(kvp.Key, SerializePartial(kvp.Value));
					}
				}

				// use the JSON serializer for now to do dictionary serialization
				return JsonSerializer.SerializeObject(dict);
			}
			else
			{
				// use the JSON serializer for now to do simple object serialization
				// ASSUMPTION: non-referrable objects don't have references to referrables
				return JsonSerializer.SerializeObject(root);
			}
		}

		/// <summary>
		/// Deserializes a partial object.
		/// </summary>
		/// <typeparam name="T">The expected object type.</typeparam>
		/// <param name="str">The string to deserialize.</param>
		/// <param name="context">Context for assembling deserialized objects.</param>
		/// <returns>The deserialized object.</returns>
		public T DeserializePartial<T>(string str, ObjectGraphContext context)
		{
			var obj = JsonSerializer.DeserializeObject<object>(str);
			if (obj is IDictionary<string, object> dict)
			{
				if (dict.Keys.Any(k => k.StartsWith(ModObjectMarker)))
				{
					// load mod object
					var key = dict.Keys.First(k => k.StartsWith(ModObjectMarker));
					return (T)Modding.Mod.Current.Find<IModObject>((string)dict[key]);
				}
				if (dict.Keys.Any(k => k.StartsWith(ReferrableMarker)))
				{
					// it's a referrable, build it from properties
					var type = new SafeType((string)dict[TypeMarker]);
					var referrable = type.Type.Instantiate();
					foreach (var kvp in dict.Where(q => char.IsLetter(q.Key.ElementAtOrDefault(0))))
					{
						if (kvp.Key.StartsWith(ReferrableMarker))
						{
							var key = kvp.Key.Substring(1);
							// TODO: look up other referrable later
						}
						else
						{
							var propertyValue = DeserializePartial<object>((string)kvp.Value, context);
							referrable.SetPropertyValue(kvp.Key, propertyValue);
						}
					}
					context.Add(referrable);
					return (T)referrable;
				}
				else
				{
					// just a plain old dictionary
					return (T)obj;
				}
			}
			else
			{
				// just a plain old object
				return (T)obj;
			}
		}
	}
}
