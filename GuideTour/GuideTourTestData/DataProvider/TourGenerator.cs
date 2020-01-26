using GuideTourData.Models;
using GuideTourData.Services;
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
            DateTime startedTour = new DateTime();
            DateTime endedTour = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            for (int i = 0; i < numOfTours; i++)
            {
                string ifGuideAppId = null;
                bool fromApp = rand.Next(1, 100) > 95 ? true : false;
                bool canceled = rand.Next(1, 100) > 90 ? true : false;
                int hours, mins;
                (hours, mins) = ConvertRandToTime(rand.Next(50, 120));
                startedTour = endedTour;
                startedTour = startedTour.AddHours(hours);
                startedTour = startedTour.AddMinutes(mins);
                (hours, mins) = ConvertRandToTime(rand.Next(40, 100));
                endedTour = startedTour;
                endedTour = endedTour.AddHours(hours);
                endedTour = endedTour.AddMinutes(mins);

                if (fromApp)
                    ifGuideAppId = "-1";
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

        private static Tour New(DateTime startedTour, DateTime endedTour, string guideId,
            bool canceled, string ifGuideAppId)
        {
            return new Tour()
            {
                Canceled = canceled,
                EndedTour = endedTour,
                GuideId = guideId,
                Id = "-1",
                IfGuideAppId = ifGuideAppId,
                StartedTour = startedTour,
                TeacherId = null,
                VisitorName = null
            };
        }
    }
}
