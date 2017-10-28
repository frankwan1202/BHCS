using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.FastCommon.Serializes
{
    public interface IJsonSerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string jsonStr);
    }
}
