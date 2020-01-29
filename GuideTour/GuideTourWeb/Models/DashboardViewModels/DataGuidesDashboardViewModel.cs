using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.DashboardViewModels
{
    public class DataGuidesDashboardViewModel
    {
        public List<GuideDataTableRow> Rows { get; set; } = new List<GuideDataTableRow>();
    }

    public class GuideDataTableRow {
        [JsonProperty("guideId")]
        public string GuideId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("teamname")]
        public string Teamname { get; set; }

        [JsonProperty("teamId")]
        public string TeamId { get; set; }

    }
}
