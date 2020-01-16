using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Services
{
    public interface IDataAccess<T> where T : class
    {
        Task<T> GetItemByIdAsync(string id);

        Task<T> GetItemAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllItemsAsync();

        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);

        Task<T> CreateSingleQueryAsync(string sqlExpression);

        Task<IEnumerable<T>> CreateQueryAsync(string sqlExpression);

        Task<T> CreateItemAsync(T item);

        Task<T> UpdateItemAsync(T item);

        Task DeleteItemAsync(string id, string partitionKey);
    }
}
