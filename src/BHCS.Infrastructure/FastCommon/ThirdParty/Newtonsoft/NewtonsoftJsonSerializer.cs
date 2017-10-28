using BHCS.Infrastructure.FastCommon.Serializes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.FastCommon.ThirdParty.Newtonsoft
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
