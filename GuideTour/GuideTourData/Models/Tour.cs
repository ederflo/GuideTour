using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Models
{
    public class Tour
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("guideName")]
        public string GuideName { get; set; }

        [JsonProperty("guideTeam")]
        public string GuideTeam { get; set; }

        [JsonProperty("startedTour")]
        public DateTime? StartedTour { get; set; }

        [JsonProperty("endedTour")]
        public DateTime? EndedTour { get; set; }

        [JsonProperty("visitorName")]
        public string VisitorName { get; set; }
    }
}
