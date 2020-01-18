using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourWeb.Models.TourViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Helpers.ObjectHelpers
{
    public class TourHelper
    {
        private readonly IDocumentDbRepository _ddb;

        public TourHelper(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }
    
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

        public async Task<TourViewModel> ToViewModel(Tour tour)
        {
            TourViewModel result = null;
            GuideLogic guideLogic = new GuideLogic(_ddb);
            TeamLogic teamLogic = new TeamLogic(_ddb);

            if (tour != null)
            {
                Guide g = await guideLogic.Get(tour.GuideId);
                if (g != null)
                {
                    Team t = await teamLogic.Get(g.TeamId);
                    if (t != null)
                    {
                        result = ToViewModel(tour, g, t);
                    }
                }
            }

            return result;
        }
    }
}
