using GuideTourData;
using GuideTourData.DataAccess;
using GuideTourData.Models;
using GuideTourData.Services;
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

        public Tour Get(string id)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return null;
        }

        public Tour Add(Tour tour)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return tourDataAccess.Add(tour);
        }

        public Tour Update(Tour tour)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return tourDataAccess.Update(tour);
        }

        public bool Delete(string id)
        {
            TourDataAccess tourDataAccess = new TourDataAccess(_ddb);
            return tourDataAccess.Delete(id);
        }

        public Tour StartTour(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            Tour tourToUpdate = Get(id);

            if (tourToUpdate == null)
                return null;

            tourToUpdate.StartedTour = DateTime.Now;
            tourToUpdate = Update(tourToUpdate);

            return tourToUpdate;
        }

        public Tour CompleteTour(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            Tour tourToUpdate = Get(id);

            if (tourToUpdate == null)
                return null;

            tourToUpdate.EndedTour = DateTime.Now;
            tourToUpdate = Update(tourToUpdate);

            return tourToUpdate;
        }

        public Tour CancelTour(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            Tour tourToCancel = Get(id);

            if (tourToCancel == null)
                return null;

            tourToCancel.EndedTour = DateTime.Now;
            tourToCancel.Canceld = true;
            tourToCancel = Update(tourToCancel);

            return tourToCancel;
        }
    }
}
