using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.DashboardViewModels
{
    public class TeamWithToursViewModel
    {
        [JsonProperty("teamname")]
        public List<string> Teamnames { get; set; } = new List<string>();

        [JsonProperty("numOfTours")]
        public List<int> NumOfTours { get; set; } = new List<int>();
    }
}
