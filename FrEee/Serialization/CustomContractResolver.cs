using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FrEee.Serialization
{
	public class CustomContractResolver : JsonContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var jsonProperty = base.CreateProperty(member, memberSerialization);
			if (member is PropertyInfo propertyInfo)
			{
				// make all properties with setters writable and readable, even if setters are private
				// and all properties without setters non-writable and non-readable
				// however properties marked with [DoNotSerialize] should not be writable or readable
				jsonProperty.Readable = jsonProperty.Writable =
					propertyInfo.GetSetMethod(true) is not null && !member.HasAttribute<DoNotSerializeAttribute>();

			}

			return jsonProperty;
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			return base.CreateProperties(type, memberSerialization);
		}
	}
}
