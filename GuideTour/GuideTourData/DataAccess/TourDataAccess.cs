using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using GuideTourData.Models;

namespace GuideTourData.DataAccess
{
    public class TourDataAccess
    {
        private static readonly Dictionary<string, Tour> tours = new Dictionary<string, Tour>
        {
            {
                "1",
                new Tour()
                {
                    Id = "1",
                    GuideName = "Florian Eder",
                    GuideTeam = "Team A",
                    StartedTour = null,
                    EndedTour = null,
                    VisitorName = "Harald Töpfer"
                }
            },
            {
                "2",
                new Tour()
                {
                    Id = "2",
                    GuideName = "Daniel Tschlatscher",
                    GuideTeam = "Team A",
                    StartedTour = null,
                    EndedTour = null,
                    VisitorName = "Einstein"
                }
            },
            {
                "3",
                new Tour()
                {
                    Id = "3",
                    GuideName = "Noah Resch",
                    GuideTeam = "Team A",
                    StartedTour = null,
                    EndedTour = null,
                    VisitorName = "Zweistein"
                }
            },
            {
                "4",
                new Tour()
                {
                    Id = "4",
                    GuideName = "Dominic Jelitsch",
                    GuideTeam = "Team A",
                    StartedTour = null,
                    EndedTour = null,
                    VisitorName = "Gustav"
                }
            },
            {
                "5",
                new Tour()
                {
                    Id = "5",
                    GuideName = "Fabian Koder",
                    GuideTeam = "Team B",
                    StartedTour = null,
                    EndedTour = null,
                    VisitorName = "Günter Jauch"
                }
            },
            {
                "6",
                new Tour()
                {
                    Id = "6",
                    GuideName = "Lukas Kreuzer",
                    GuideTeam = "Team B",
                    StartedTour = null,
                    EndedTour = null,
                    VisitorName = "Steve Jobs"
                }
            },
        };


        public List<Tour> Get()
        {
            return tours.Values.ToList();
        }

        public Tour Get(string id)
        {
            return tours[id];
        }

        public Tour Add(Tour tour)
        {
            if (tour == null)
                return null;

            if (string.IsNullOrWhiteSpace(tour.Id))
            {
                Random rand = new Random();
                tour.Id = rand.Next().ToString();
            }

            tours.Add(tour.Id, tour);
            return tour;
        }


        public Tour Update(Tour tour)
        {
            if (tour == null || string.IsNullOrWhiteSpace(tour.Id))
                return null;

            Tour tourToUpdate = tours[tour.Id];

            if (tourToUpdate != null)
                tourToUpdate = tour;

            return tourToUpdate;
        }

        public bool Delete(string id)
        {
            bool succeeded = false;

            if (string.IsNullOrWhiteSpace(id))
                return succeeded;

            if (tours.ContainsKey(id))
                succeeded = tours.Remove(id);

            return succeeded;
        }
    }
}
