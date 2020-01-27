using GuideTourData.Models;
using GuideTourData.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourTestData.DataProvider
{
    public class TourGenerator
    {
        public static List<Tour> Generate(List<Guide> guides)
        {
            List<Tour> tours = new List<Tour>();
            Random rand = new Random();
            foreach (Guide g in guides)
            {
                tours.AddRange(GenerateToursPerGuide(g, rand.Next(1, 4)));
            }
            return tours;
        }

        private static List<Tour> GenerateToursPerGuide(Guide g, int numOfTours)
        {
            List<Tour> tours = new List<Tour>();
            Random rand = new Random();
            DateTime now = DateTime.Now;
            DateTime? startedTour = new DateTime();
            DateTime? endedTour = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            bool isOngoing = rand.Next(1, 100) > 80 ? true : false;
            for (int i = 0; i < numOfTours; i++)
            {
                string ifGuideAppId = null;
                bool fromApp = rand.Next(1, 100) <= 97 ? true : false;
                bool canceled = rand.Next(1, 100) > 95 ? true : false;
                int hours, mins;
                (hours, mins) = ConvertRandToTime(rand.Next(50, 120));
                startedTour = endedTour;
                startedTour = startedTour.Value.AddHours(hours);
                startedTour = startedTour.Value.AddMinutes(mins);
                if (i >= (numOfTours - 1) && isOngoing)
                {
                    endedTour = null;
                }
                else
                {
                    (hours, mins) = ConvertRandToTime(rand.Next(40, 100));
                    endedTour = startedTour;
                    endedTour = endedTour.Value.AddHours(hours);
                    endedTour = endedTour.Value.AddMinutes(mins);
                }

                if (fromApp)
                    ifGuideAppId = ObjectId.GenerateNewId().ToString();
                tours.Add(New(startedTour, endedTour, g.Id, canceled, ifGuideAppId));
            }

            return tours;
        }

        private static (int, int) ConvertRandToTime(int rand)
        {
            int hours = rand / 60;
            int min = rand % 60;
            return (hours, min);
        }

        private static Tour New(DateTime? startedTour, DateTime? endedTour, string guideId,
            bool canceled, string ifGuideAppId)
        {
            return new Tour()
            {
                Canceled = canceled,
                EndedTour = endedTour,
                GuideId = guideId,
                Id = ObjectId.GenerateNewId().ToString(),
                IfGuideAppId = ifGuideAppId,
                StartedTour = startedTour,
                TeacherId = null,
                VisitorName = null
            };
        }
    }
}
