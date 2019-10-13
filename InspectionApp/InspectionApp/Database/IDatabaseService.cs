using System;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace InspectionApp.Database
{
    public interface IDatabaseService<T> where T : class, new()
    {
        Task<List<T>> Get();

        Task<T> Get(int id);

        Task<ObservableCollection<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);

        Task<T> GetMaxCode<TValue>(Expression<Func<T, TValue>> orderBy = null, Boolean ascending = true);

        Task<T> Get(Expression<Func<T, bool>> predicate);

        AsyncTableQuery<T> AsQueryable();

        Task InsertAll(IList<T> entity);

        Task<int> InsertItemAsync(T entity);

        Task<int> UpdateItemAsync(T entity);

        Task<int> DeleteAllAsync<T>();

        Task<int> DeleteItemAsync(Expression<Func<T, bool>> predicate);

        Task<int> Count(Expression<Func<T, bool>> predicate = null);
    }
}
