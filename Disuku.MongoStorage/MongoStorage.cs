using Disuku.Core.Storage;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Disuku.MongoStorage
{
    public class MongoDbStorage : IMongoDbStorage
    {
        private IMongoDatabase _dataBase;

        public Task InitializeDbAsync(string databaseName)
        {
            var client = new MongoClient();
            _dataBase = client.GetDatabase(databaseName);
            return Task.CompletedTask;
        }

        public async Task UpsertSingleRecordAsync<T>(string tableName, Guid id, T record)
        {
            var collection = _dataBase.GetCollection<T>(tableName);
            await collection.ReplaceOneAsync(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions
                {
                    IsUpsert = true
                });
        }

        public async Task<List<T>> LoadRecordsAsync<T>(string table, Expression<Func<T, bool>> predicate)
        {
            var collection = _dataBase.GetCollection<T>(table);
            var result = await collection.FindAsync(predicate);
            return result.ToList();
        }
    }
}
