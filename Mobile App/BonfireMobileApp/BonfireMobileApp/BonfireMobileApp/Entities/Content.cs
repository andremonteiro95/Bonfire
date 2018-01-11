using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonfireMobileApp.Entities
{
    public class Content
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("StartDate")]
        public string StartDate { get; set; }

        [JsonProperty("EndDate")]
        public string EndDate { get; set; }
    }
}
