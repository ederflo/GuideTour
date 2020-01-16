using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using GuideTourData.Models;
using GuideTourData.Services;
using System.Linq.Expressions;

namespace GuideTourData.DataAccess
{
    public class TourDataAccess : IDataAccess<Tour>
    {
        private readonly IDocumentDbRepository _ddb;

        public TourDataAccess(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }


        //private static readonly Dictionary<string, Tour> tours = new Dictionary<string, Tour>
        //{
        //    {
        //        "1",
        //        new Tour()
        //        {
        //            Id = "1",
        //            StartedTour = null,
        //            EndedTour = null,
        //            VisitorName = "Harald Töpfer"
        //        }
        //    },
        //    {
        //        "2",
        //        new Tour()
        //        {
        //            Id = "2",
        //            StartedTour = null,
        //            EndedTour = null,
        //            VisitorName = "Einstein"
        //        }
        //    },
        //    {
        //        "3",
        //        new Tour()
        //        {
        //            Id = "3",
        //            StartedTour = null,
        //            EndedTour = null,
        //            VisitorName = "Zweistein"
        //        }
        //    },
        //    {
        //        "4",
        //        new Tour()
        //        {
        //            Id = "4",
        //            StartedTour = null,
        //            EndedTour = null,
        //            VisitorName = "Gustav"
        //        }
        //    },
        //    {
        //        "5",
        //        new Tour()
        //        {
        //            Id = "5",
        //            StartedTour = null,
        //            EndedTour = null,
        //            VisitorName = "Günter Jauch"
        //        }
        //    },
        //    {
        //        "6",
        //        new Tour()
        //        {
        //            Id = "6",
        //            GuideName = "Lukas Kreuzer",
        //            GuideTeam = "Team B",
        //            StartedTour = null,
        //            EndedTour = null,
        //            VisitorName = "Steve Jobs"
        //        }
        //    },
        //};

        public Task<Tour> GetItemAsync(Expression<Func<Tour, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Tour> GetItemByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tour>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tour>> GetItemsAsync(Expression<Func<Tour, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Tour> CreateItemAsync(Tour item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tour>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<Tour> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Tour> UpdateItemAsync(Tour item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
