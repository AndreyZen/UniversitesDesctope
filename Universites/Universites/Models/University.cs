using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universites.Models
{
    public class University
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("domains")]
        public string[] Domains { get; set; }
        [JsonProperty("web_pages")]
        public string[] WebPages { get; set; }
        [JsonProperty("alpha_two_code")]
        public string AlphaTwoCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("stateprovince")]
        public string StateProvince { get; set; }
    }

}
