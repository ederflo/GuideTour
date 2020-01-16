using GuideTourData.Models;
using GuideTourWeb.Models.TourViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Helpers.ObjectHelpers
{
    public class TourHelper
    {
    
        public static List<TourViewModel> ToViewModel(List<Tour> tours, List<Guide> guides, List<Team> teams)
        {
            List<TourViewModel> result = new List<TourViewModel>();
            foreach(Tour tour in tours)
            {
                Guide g = guides.FirstOrDefault(x => x.Id == tour.GuideId);
                if (g != null)
                {
                    Team t = teams.FirstOrDefault(x => x.Id == g.TeamId);
                    result.Add(ToViewModel(tour, g, t));
                }
            }
            return result;
        }

        public static TourViewModel ToViewModel(Tour tour, Guide g, Team team)
        {
            return new TourViewModel()
            {
                Id = tour.Id,
                GuideId = tour.GuideId,
                GuideName = g.Name,
                EndedTour = tour.EndedTour,
                StartedTour = tour.StartedTour,
                Team = team.Name,
                TeamId = team.Id,
                VisitorName = tour.VisitorName
            };
        }
    }
}
