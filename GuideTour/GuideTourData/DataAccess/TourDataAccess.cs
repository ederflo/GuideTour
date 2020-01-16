using System;
using System.Linq;
using MongoDB.Driver;
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

        public async Task<Tour> GetItemAsync(Expression<Func<Tour, bool>> predicate)
        {
            var result = await _ddb.Tours.FindAsync(predicate);
            List<Tour> tours = await result.ToListAsync();
            if (tours.Count > 1)
                return null;
            return tours.FirstOrDefault();
        }

        public async Task<Tour> GetItemByIdAsync(string id)
        {
            var result = await _ddb.Tours.FindAsync(Builders<Tour>.Filter.Eq(x => x.Id.ToString(), id));
            List<Tour> tours = await result.ToListAsync();
            if (tours.Count > 1)
                return null;
            return tours.FirstOrDefault();
        }

        public async Task<IEnumerable<Tour>> GetAllItemsAsync()
        {
            var result = await _ddb.Tours.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetItemsAsync(Expression<Func<Tour, bool>> predicate)
        {
            var result = await _ddb.Tours.FindAsync(predicate);
            return await result.ToListAsync();
        }

        public async Task<Tour> CreateItemAsync(Tour item)
        {
            await _ddb.Tours.InsertOneAsync(item);
            return item;
        }

        public Task<IEnumerable<Tour>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Tour> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Tour> UpdateItemAsync(Tour item)
        {
            ReplaceOneResult updateResult = await _ddb.Tours.ReplaceOneAsync(
                Builders<Tour>.Filter.Eq(x => x.Id.ToString(), item.Id), item);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 1 ? item : null;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            DeleteResult deleteResult = await _ddb.Tours.DeleteOneAsync(Builders<Tour>.Filter.Eq(x => x.Id.ToString(), id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
