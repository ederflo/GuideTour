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

        public async Task<Tour> Add(Tour tour)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return await tourDataAccess.CreateItemAsync(tour);
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
            tourToCancel.Canceld = true;

            return await Update(tourToCancel);
        }

        public static Tour NewTour(string guideId, string teacherId, string visitorName = null, string guideAppTour = null)
        {
            return new Tour()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                IfGuideAppId = null,
                EndedTour = null,
                StartedTour = null,
                VisitorName = visitorName,
                Canceld = false,
                GuideId = guideId,
                TeacherId = teacherId
            };
        }
    }
}
