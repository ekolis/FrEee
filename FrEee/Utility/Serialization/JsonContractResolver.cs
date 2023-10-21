using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility.Serialization
{
	public class JsonContractResolver : DefaultContractResolver
	{

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{

			var property = base.CreateProperty(member, memberSerialization);

			if (member.GetCustomAttributes<DoNotSerializeAttribute>(true).Count() > 0)
				property.ShouldSerialize = instance => { return false; };

			return property;
		}

	}
}
