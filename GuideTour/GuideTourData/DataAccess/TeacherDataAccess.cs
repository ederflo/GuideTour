﻿using GuideTourData.Models;
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
            return await GetItemAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Teacher>> GetAllItemsAsync()
        {
            var result = await _ddb.Teachers.FindAsync(x => x.Type.Equals(typeof(Teacher).Name));
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

        public async Task<List<Teacher>> CreateItemsAsync(List<Teacher> items)
        {
            await _ddb.Teachers.InsertManyAsync(items);
            return items;
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
            ReplaceOneResult updateResult = await _ddb.Teachers.ReplaceOneAsync(x => x.Id.Equals(item.Id), item);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ? item : null;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            DeleteResult deleteResult = await _ddb.Teachers.DeleteOneAsync(x => x.Id.Equals(id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
