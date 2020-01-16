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
    public class TeacherDataAccess : IDataAccess<Teacher>
    {
        private readonly IDocumentDbRepository _ddb;

        public TeacherDataAccess(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }


        public async Task<Teacher> GetItemAsync(Expression<Func<Teacher, bool>> predicate)
        {
            var result = await _ddb.Teachers.FindAsync(predicate);
            List<Teacher> teachers = await result.ToListAsync();
            if (teachers.Count > 1)
                return null;
            return teachers.FirstOrDefault();
        }

        public async Task<Teacher> GetItemByIdAsync(string id)
        {
            var result = await _ddb.Teachers.FindAsync(Builders<Teacher>.Filter.Eq(x => x.Id.ToString(), id));
            List<Teacher> teachers = await result.ToListAsync();
            if (teachers.Count > 1)
                return null;
            return teachers.FirstOrDefault();
        }

        public async Task<IEnumerable<Teacher>> GetAllItemsAsync()
        {
            var result = await _ddb.Teachers.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetItemsAsync(Expression<Func<Teacher, bool>> predicate)
        {
            var result = await _ddb.Teachers.FindAsync(predicate);
            return await result.ToListAsync();
        }

        public async Task<Teacher> CreateItemAsync(Teacher item)
        {
            await _ddb.Teachers.InsertOneAsync(item);
            return item;
        }

        public Task<IEnumerable<Teacher>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Teacher> UpdateItemAsync(Teacher item)
        {
            ReplaceOneResult updateResult = await _ddb.Teachers.ReplaceOneAsync(
                Builders<Teacher>.Filter.Eq(x => x.Id.ToString(), item.Id), item);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 1 ? item : null;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            DeleteResult deleteResult = await _ddb.Teachers.DeleteOneAsync(Builders<Teacher>.Filter.Eq(x => x.Id.ToString(), id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
