using GuideTourData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.DashboardViewModels
{
    public class DataTeamsDashboardViewModel
    {
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
