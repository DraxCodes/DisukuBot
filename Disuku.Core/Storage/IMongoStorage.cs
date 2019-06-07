﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Disuku.Core.Storage
{
    public interface IDbStorage
    {
        Task InitializeDbAsync(string databaseName);
        Task UpsertSingleRecordAsync<T>(string tableName, Guid id, T item);
        Task<List<T>> LoadRecordsAsync<T>(string table, Expression<Func<T, bool>> predicate);
        Task Insert<T>(string tableName, T item);
        Task Update<T>(string tableName, Guid id, T item);
        Task<bool> Exists<T>(string tableName, Guid id);
    }
}
