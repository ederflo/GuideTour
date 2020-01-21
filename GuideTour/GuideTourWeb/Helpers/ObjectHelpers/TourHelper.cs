using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourWeb.Models.MqttModels;
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

        public async Task<TourViewModel> ToViewModel(Tour tour)
        {
            TourViewModel result = null;
            GuideLogic guideLogic = new GuideLogic(_ddb);
            Guide g;
            Team t;
            if (tour != null)
            {
                (g, t) = await guideLogic.GetGuideAndTeam(tour.GuideId);
                if (g != null && t != null )
                {
                    result = ToViewModel(tour, g, t);
                }
            }
            return result;
        }

        public async Task<TourMqttModel> ToMqttModel(Tour tour, string atStation, TourMqttState state)
        {
            TourMqttModel result = null;
            GuideLogic guideLogic = new GuideLogic(_ddb);
            Guide g;
            Team t;
            if (tour != null)
            {
                (g, t) = await guideLogic.GetGuideAndTeam(tour.GuideId);
                if (g != null && t != null)
                {
                    result = ToMqttModel(tour, g, t, atStation, state);
                }
            }
            return result;
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
                GuideName = StringHelper.Slice(g.Name, 14, true),
                EndedTour = tour.EndedTour,
                StartedTour = tour.StartedTour,
                Team = StringHelper.Slice(team.Name, 14, true),
                TeamId = team.Id,
                VisitorName = tour.VisitorName != null ? StringHelper.Slice(tour.VisitorName, 25, true) : tour.VisitorName
            };
        }

        public static TourMqttModel ToMqttModel(Tour tour, Guide g, Team t, string atStation, TourMqttState state)
        {
            return new TourMqttModel()
            {
                Id = tour.Id,
                Team = t.Name,
                Guide = g.Name,
                NameOfGuests = tour.VisitorName,
                AtStation = atStation,
                State = state
            };
        }

        public static TourMqttModel ToMqttModel(TourViewModel tour, string atStation, TourMqttState state)
        {
            return new TourMqttModel()
            {
                Id = tour.Id,
                Team = tour.Team,
                Guide = tour.GuideName,
                NameOfGuests = tour.VisitorName,
                AtStation = atStation,
                State = state
            };
        }
    }
}
