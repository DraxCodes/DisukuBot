using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Disuku.MongoStorage
{
    public class MongoDbStorage : IMongoDbStorage
    {
        private IMongoDatabase _dataBase;

        //TODO: https://www.codementor.io/pmbanugo/working-with-mongodb-in-net-1-basics-g4frivcvz

        public void InitializeDataBase(string database)
        {
            //Loadthe DisukuDB
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
