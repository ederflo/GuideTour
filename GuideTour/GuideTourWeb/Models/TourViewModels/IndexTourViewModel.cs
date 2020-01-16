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
        public List<TourViewModel> NotStarted { get; set; } = new List<TourViewModel>();

        public List<TourViewModel> Started { get; set; } = new List<TourViewModel>();

        public List<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();

        [JsonProperty("guideName")]
        public string GuideName { get; set; }

        [JsonProperty("guideTeam")]
        public string GuideTeam { get; set; }

        [JsonProperty("visitorName")]
        public string VisitorName { get; set; }

        [JsonProperty("teacherId")]
        public string TeacherId { get; set; }
    }
}
