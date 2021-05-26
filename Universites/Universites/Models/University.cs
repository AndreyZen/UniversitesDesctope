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
        public string[] domains { get; set; }
        [JsonProperty("web_pages")]
        public string[] WebPages { get; set; }
        public string alpha_two_code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        public string stateprovince { get; set; }
    }

}
