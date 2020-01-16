using GuideTourData.Models;
using GuideTourData.Services;
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

        public Task<Teacher> CreateItemAsync(Teacher item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Teacher>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> GetItemAsync(Expression<Func<Teacher, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> GetItemByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Teacher>> GetItemsAsync(Expression<Func<Teacher, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
