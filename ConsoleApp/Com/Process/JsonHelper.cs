/** 
* Synopsis:
* 
*
* Written by: Asniza Azni 2021-03-26
* Synopsis:
* Revised: Date / Name / Synopsis
*                  
*/
 
using Newtonsoft.Json;

namespace ConsoleApp.Com.Process
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = BaseFirstContractResolver.Instance
        };

        public static T CreateCopy<T>(T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, settings);
        }
    }
}
