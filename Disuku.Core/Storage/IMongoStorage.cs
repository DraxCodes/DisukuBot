using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Disuku.Core.Storage
{
    public interface IDataStore
    {
        Task InitializeDbAsync(string databaseName = null);
        Task<List<T>> LoadRecordsAsync<T>(Expression<Func<T, bool>> predicate, string table);
        Task<T> LoadRecordAsync<T>(Expression<Func<T, bool>> predicate, string table);
        Task Insert<T>(T item, string tableName);
        Task Update<T>(Guid id, T item, string tableName);
        Task<bool> Exists<T>(Guid id, string tableName);
    }
}
