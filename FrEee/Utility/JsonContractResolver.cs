using System.Linq;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#nullable enable

namespace FrEee.Utility
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
