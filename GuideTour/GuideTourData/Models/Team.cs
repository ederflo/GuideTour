using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Models
{
    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("guides")]
        public List<Guide> Guides { get; set; } = new List<Guide>();
    }
}
