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
    public class TeamDataAccess : IDataAccess<Team>
    {
        private readonly IDocumentDbRepository _ddb;

        public TeamDataAccess (IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task<Team> GetItemAsync(Expression<Func<Team, bool>> predicate)
        {
            var result = await _ddb.Teams.FindAsync(predicate);
            List<Team> guides = await result.ToListAsync();
            if (guides.Count > 1)
                return null;
            return guides.FirstOrDefault();
        }

        public async Task<Team> GetItemByIdAsync(string id)
        {
            var result = await _ddb.Teams.FindAsync(Builders<Team>.Filter.Eq(x => x.Id.ToString(), id));
            List<Team> teams = await result.ToListAsync();
            if (teams.Count > 1)
                return null;
            return teams.FirstOrDefault();
        }

        public async Task<IEnumerable<Team>> GetAllItemsAsync()
        {
            var result = await _ddb.Teams.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetItemsAsync(Expression<Func<Team, bool>> predicate)
        {
            var result = await _ddb.Teams.FindAsync(predicate);
            return await result.ToListAsync();
        }

        public async Task<Team> CreateItemAsync(Team item)
        {
            await _ddb.Teams.InsertOneAsync(item);
            return item;
        }

        public async Task<List<Team>> CreateItemsAsync(List<Team> items)
        {
            await _ddb.Teams.InsertManyAsync(items);
            return items;
        }

        public Task<IEnumerable<Team>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<Team> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> UpdateItemAsync(Team item)
        {
            ReplaceOneResult updateResult = await _ddb.Teams.ReplaceOneAsync(
                Builders<Team>.Filter.Eq(x => x.Id.ToString(), item.Id), item);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 1 ? item : null;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            DeleteResult deleteResult = await _ddb.Teams.DeleteOneAsync(Builders<Team>.Filter.Eq(x => x.Id.ToString(), id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
