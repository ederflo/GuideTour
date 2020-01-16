using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourData.Models
{
    public class ImportTeam
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("guides")]
        public List<Guide> Guides { get; set; } = new List<Guide>();
    }
}
