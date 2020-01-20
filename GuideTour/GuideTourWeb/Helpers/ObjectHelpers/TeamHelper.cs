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
            List<TeamViewModel> result = ToViewModel(teams);
            
            foreach (Guide g in guides) {
                TeamViewModel tVM = result.FirstOrDefault(x => x.TeamId == g.TeamId);
                if (tVM != null)
                    tVM.Guides.Add(g);
            }
            return result;
        }

        public static List<TeamViewModel> ToViewModel(List<Team> teams)
        {
            List<TeamViewModel> result = new List<TeamViewModel>();
            foreach(Team t in teams)
            {
                result.Add(ToViewModel(t));
            }
            return result;
        }

        public static TeamViewModel ToViewModel(Team t)
        {
            return new TeamViewModel()
            {
                TeamId = t.Id.ToString(),
                TeamName = t.Name,
                Guides = new List<Guide>()
            };
        }

        public static List<Team> SliceNames(List<Team> teams, int maxLength, bool withDot = true)
        {
            foreach (Team t in teams)
            {
                t.Name = StringHelper.Slice(t.Name, maxLength, withDot);
            }

            return teams;
        }

        public static Team Clone(Team t)
        {
            return new Team()
            {
                Id = t.Id,
                Name = t.Name,
                Type = t.Type
            };
        }
    }
}
