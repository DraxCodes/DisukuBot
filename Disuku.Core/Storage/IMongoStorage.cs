using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Disuku.Core.Storage
{
    public interface IDbStorage
    {
        Task InitializeDbAsync(string databaseName = null);
        Task<List<T>> LoadRecordsAsync<T>(Expression<Func<T, bool>> predicate, string table = null);
        Task Insert<T>(T item, string tableName = null);
        Task Update<T>(Guid id, T item, string tableName = null);
        Task<bool> Exists<T>(Guid id, string tableName = null);
    }
}
