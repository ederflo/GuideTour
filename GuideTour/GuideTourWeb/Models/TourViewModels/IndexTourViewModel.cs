using GuideTourData;
using GuideTourData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.TourViewModels
{
    public class IndexTourViewModel
    {
        public List<Tour> NotStarted { get; set; } = new List<Tour>();

        public List<Tour> Started { get; set; } = new List<Tour>();

        public List<Team> Teams { get; set; } = new List<Team>();

        [JsonProperty("guideName")]
        public string GuideName { get; set; }

        [JsonProperty("guideTeam")]
        public string GuideTeam { get; set; }

        [JsonProperty("visitorName")]
        public string VisitorName { get; set; }
    }
}
