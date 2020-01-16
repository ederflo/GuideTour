using GuideTourData.Models;
using GuideTourWeb.Models.TourViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Helpers.ObjectHelpers
{
    public class TeamHelper
    {
        public static List<TeamViewModel> AssignGuidesToTeam(List<Team> teams, List<Guide> guides)
        {
            List<TeamViewModel> result = toViewModel(teams);
            
            foreach (Guide g in guides) {
                TeamViewModel tVM = result.FirstOrDefault(x => x.TeamId == g.TeamId);
                if (tVM != null)
                    tVM.Guides.Add(g);
            }
            return result;
        }

        public static List<TeamViewModel> toViewModel(List<Team> teams)
        {
            List<TeamViewModel> result = new List<TeamViewModel>();
            foreach(Team t in teams)
            {
                result.Add(toViewModel(t));
            }
            return result;
        }

        public static TeamViewModel toViewModel(Team t)
        {
            return new TeamViewModel()
            {
                TeamId = t.Id.ToString(),
                TeamName = t.Name,
                Guides = new List<Guide>()
            };
        }
    }
}
