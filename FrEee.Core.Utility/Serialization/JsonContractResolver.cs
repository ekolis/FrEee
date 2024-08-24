using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace FrEee.Serialization;

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
