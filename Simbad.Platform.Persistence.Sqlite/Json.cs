using System;
using Newtonsoft.Json;

namespace Simbad.Platform.Persistence.Sqlite
{
    internal static class Json
    {
        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, CreateSettings());
        }

        public static object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type, CreateSettings());
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, CreateSettings());
        }

        private static JsonSerializerSettings CreateSettings()
        {
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };
        }
    }
}