using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Disuku.MongoStorage
{
    public class MongoDbStorage : IMongoDbStorage
    {
        private IMongoDatabase _dataBase;

        public void LoadDataBase(string database)
        {
            var client = new MongoClient();
            _dataBase = client.GetDatabase(database);
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<T> RestoreMany<T>(T item)
        {
            throw new NotImplementedException();
        }

        public T RestoreSingle<T>(T Item)
        {
            throw new NotImplementedException();
        }

        public void Store<T>(T Item)
        {
            throw new NotImplementedException();
        }
    }
}
