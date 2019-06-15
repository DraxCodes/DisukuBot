using Disuku.Core.Storage;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Disuku.MongoStorage
{
    public class MongoDbStorage : IDataStore
    {
        //private Conf Config = ConfigService.GetConfig();
        //private string ConnectionString;
        private IMongoDatabase _dataBase;

        public Task InitializeDbAsync(string databaseName = null)
        {
            //ConnectionString = $"mongodb://{Config.Username}:{Config.Password}@{Config.Ip}:{Config.Port}";
            var client = new MongoClient();
            _dataBase = client.GetDatabase(databaseName);
            return Task.CompletedTask;
        }

        public Task<T> LoadRecordAsync<T>(Expression<Func<T, bool>> predicate, string table)
        {
            throw new NotImplementedException();
        }

        public async Task Insert<T>(T item, string tableName = null)
        {
            var collection = _dataBase.GetCollection<T>(tableName);
            await collection.InsertOneAsync(item);
        }

        public async Task Update<T>(Guid id, T item, string tableName = null)
        {
            var collection = _dataBase.GetCollection<T>(tableName);
            if (await Exists<T>(id, tableName))
            {
                await collection.ReplaceOneAsync(
                       new BsonDocument("_id", id),
                       item);
            }
        }

        public async Task<bool> Exists<T>(Guid id, string tableName = null)
        {
            var collection = _dataBase.GetCollection<T>(tableName);
            var item = await collection.FindAsync(new BsonDocument("_id", id));
            return item.Any();
        }

        public async Task<List<T>> LoadRecordsAsync<T>(Expression<Func<T, bool>> predicate, string tableName)
        {
            var collection = _dataBase.GetCollection<T>(tableName);
            var result = await collection.FindAsync(predicate);
            return result.ToList();
        }
    }
}
