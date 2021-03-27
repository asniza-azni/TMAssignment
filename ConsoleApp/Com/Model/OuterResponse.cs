/** 
* Synopsis:
* 
*
* Written by: Asniza Azni 2021-03-26
* Synopsis:
* Revised: Date / Name / Synopsis
*                  
*/

using ConsoleApp.Com.Process;
using Newtonsoft.Json;

namespace ConsoleApp.Com.Model
{
    [JsonObject()]
    public class OuterResponse 
    { 
        [JsonProperty(PropertyName = "apt")]
        public string AptNo { get; set; }
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "section")]
        public string Section { get; set; }
        [JsonProperty(PropertyName = "postcode")]
        public string PostCode { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        
        public string ToJson()
        {
            return JsonHelper.Serialize(this);
        }
    }
}
