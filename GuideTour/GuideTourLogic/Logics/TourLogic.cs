using GuideTourData;
using GuideTourData.DataAccess;
using GuideTourData.Models;
using GuideTourData.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourLogic.Logics
{
    public class TourLogic
    {
        private readonly IDocumentDbRepository _ddb;

        public TourLogic(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task<List<Tour>> Get() {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            var result = await tourDataAccess.GetAllItemsAsync();
            return result.ToList();
        }

        public async Task<Tour> Get(string id)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.GetItemByIdAsync(id);
        }

        public async Task<Tour> GetByIfGuideAppId(string ifGuideAppId)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.GetItemAsync(x => x.IfGuideAppId == ifGuideAppId);
        }

        public async Task<Tour> Add(Tour tour)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.CreateItemAsync(tour);
        }
        public async Task<List<Tour>> Add(List<Tour> tours)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.CreateItemsAsync(tours);
        }


        public async Task<Tour> Update(Tour tour)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.UpdateItemAsync(tour);
        }

        public async Task<bool> Delete(string id)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.DeleteItemAsync(id);
        }

        public async Task<Tour> StartTour(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            Tour tourToUpdate = await Get(id);

            if (tourToUpdate == null)
                return null;

            tourToUpdate.StartedTour = DateTime.Now;
            return tourToUpdate = await Update(tourToUpdate);
        }

        public async Task<Tour> CompleteTour(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            Tour tourToUpdate = await Get(id);

            if (tourToUpdate == null)
                return null;

            tourToUpdate.EndedTour = DateTime.Now;
            return await Update(tourToUpdate);
        }

        public async Task<Tour> CancelTour(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            Tour tourToCancel = await Get(id);

            if (tourToCancel == null)
                return null;

            tourToCancel.EndedTour = DateTime.Now;
            tourToCancel.Canceled = true;

            return await Update(tourToCancel);
        }

        public static Tour NewTour(string guideId, string teacherId, string visitorName = null, string guideAppTour = null)
        {
            return new Tour()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                IfGuideAppId = guideAppTour,
                EndedTour = null,
                StartedTour = null,
                VisitorName = visitorName,
                Canceled = false,
                GuideId = guideId,
                TeacherId = teacherId
            };
        }

        public static List<Tour> Clone(List<Tour> tours)
        {
            List<Tour> result = new List<Tour>();
            foreach (Tour t in tours)
            {
                result.Add(Clone(t));
            }
            return result;
        }

        public static Tour Clone(Tour t) {
            return new Tour()
            {
                Canceled = t.Canceled,
                TeacherId = t.TeacherId,
                EndedTour = t.EndedTour,
                StartedTour = t.StartedTour,
                GuideId = t.GuideId,
                Id = t.Id,
                IfGuideAppId = t.IfGuideAppId,
                VisitorName = t.VisitorName
            };
        }

        public static Dictionary<DateTime, int> GetToursPerHalfHour(List<Tour> tours)
        {
            List<Tour> clonedTours = Clone(tours);
            clonedTours = clonedTours.OrderBy(x => x.StartedTour).ToList();
            DateTime now = DateTime.Now;
            DateTime startOfTDOT = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            DateTime endOfTDOT = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0);
            TimeSpan wholeTDOT = endOfTDOT - startOfTDOT;
            int numOfUnits = (wholeTDOT.Hours * 2) + 1;
            Dictionary<DateTime, int> result = new Dictionary<DateTime, int>();
            for (int i = 0; i < numOfUnits; i++)
            {
                int cntOfTours = clonedTours.RemoveAll(x => x.StartedTour >= startOfTDOT && x.StartedTour < startOfTDOT.AddMinutes(30));
                result.Add(startOfTDOT, cntOfTours);
                startOfTDOT = startOfTDOT.AddMinutes(30);
            }
            return result;
        }
    }
}
