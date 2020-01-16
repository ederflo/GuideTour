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
            var result = await _ddb.Guides.FindAsync(Builders<Guide>.Filter.Eq(x => x.Id, id));
            List<Guide> guides = await result.ToListAsync();
            if (guides.Count > 1)
                return null;
            return guides.FirstOrDefault();
        }

        public Task<IEnumerable<Guide>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guide>> GetItemsAsync(Expression<Func<Guide, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Guide> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guide>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<Guide> CreateItemAsync(Guide item)
        {
            throw new NotImplementedException();
        }

        public async Task<Guide> UpdateItemAsync(Guide item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
