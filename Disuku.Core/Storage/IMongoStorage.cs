using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Disuku.Core.Storage
{
    public interface IMongoDbStorage
    {
        Task InitializeDbAsync(string databaseName);
        Task UpsertSingleRecordAsync<T>(string tableName, Guid id, T Item);
        Task<List<T>> LoadRecordsAsync<T>(string table, Expression<Func<T, bool>> predicate);
    }
}
