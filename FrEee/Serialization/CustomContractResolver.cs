using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FrEee.Serialization
{
	public class CustomContractResolver : JsonContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var jsonProperty = base.CreateProperty(member, memberSerialization);
			if (!jsonProperty.Writable)
			{
				if (member is PropertyInfo propertyInfo)
				{
					// make all properties with setters writable, even if setters are private
					jsonProperty.Writable = propertyInfo.GetSetMethod(true) != null;
				}
			}

			return jsonProperty;
		}
	}
}
