using FrEee.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Serialization
{
	public class GalaxyMapConverter : JsonConverter
	{
		public override void WriteJson(
			JsonWriter writer, object value, JsonSerializer serializer)
		{
			var map = (IDictionary<Point, StarSystem>)value;

			var j = new JObject();
			foreach (var key in map.Keys)
			{
				var jw = new JTokenWriter();
				serializer.Serialize(jw, map[key]);
				jw.Close();
				j[key.X + ", " + key.Y] = jw.Token;
			}
			serializer.Serialize(writer, j);
		}

		public override object ReadJson(
			JsonReader reader, Type objectType, object existingValue,
			JsonSerializer serializer)
		{
			var j = serializer.Deserialize<JObject>(reader);

			IDictionary<Point, StarSystem> map;
			if (objectType.IsInterface || objectType.IsAbstract)
				map = new Dictionary<Point, StarSystem>();
			else
				map = (IDictionary<Point, StarSystem>)Activator.CreateInstance(objectType);

			foreach (var item in j)
			{
				var coords = item.Key.Split(',').Select(n => int.Parse(n.Trim())).ToArray();
				var jr = new JTokenReader(item.Value);
				map.Add(new Point(coords[0], coords[1]), serializer.Deserialize<StarSystem>(jr));
			}

			return map;

		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(IDictionary<Point, StarSystem>).IsAssignableFrom(objectType);
		}
	}
}
