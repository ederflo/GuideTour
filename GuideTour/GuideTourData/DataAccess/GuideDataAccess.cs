using GuideTourData.Models;
using GuideTourData.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.DataAccess
{
    public class GuideDataAccess : IDataAccess<Guide>
    {
        private readonly IDocumentDbRepository _ddb;

        public GuideDataAccess(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task<Guide> GetItemAsync(Expression<Func<Guide, bool>> predicate)
        {
            var result = await _ddb.Guides.FindAsync(predicate);
            List<Guide> guides = await result.ToListAsync();
            if (guides.Count > 1)
                return null;
            return guides.FirstOrDefault();
        }

        public async Task<Guide> GetItemByIdAsync(string id)
        {
            var result = await _ddb.Guides.FindAsync(Builders<Guide>.Filter.Eq(x => x.Id.ToString(), id));
            List<Guide> guides = await result.ToListAsync();
            if (guides.Count > 1)
                return null;
            return guides.FirstOrDefault();
        }

        public async Task<IEnumerable<Guide>> GetAllItemsAsync()
        {
            var result = await _ddb.Guides.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Guide>> GetItemsAsync(Expression<Func<Guide, bool>> predicate)
        {
            var result = await _ddb.Guides.FindAsync(predicate);
            return await result.ToListAsync();
        }

        public Task<Guide> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guide>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Guide> CreateItemAsync(Guide item)
        {
            await _ddb.Guides.InsertOneAsync(item);
            return item;
        }

        public async Task<List<Guide>> CreateItemsAsync(List<Guide> items)
        {
            await _ddb.Guides.InsertManyAsync(items);
            return items;
        }

        public async Task<Guide> UpdateItemAsync(Guide item)
        {
            ReplaceOneResult updateResult = await _ddb.Guides.ReplaceOneAsync(
                Builders<Guide>.Filter.Eq(x => x.Id.ToString(), item.Id), item);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 1 ? item : null;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            DeleteResult deleteResult = await _ddb.Guides.DeleteOneAsync(Builders<Guide>.Filter.Eq(x => x.Id.ToString(), id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
