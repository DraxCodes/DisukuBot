using System;
using System.Collections.Generic;
using System.Text;

namespace Disuku.MongoStorage
{
    public interface IMongoDbStorage
    {
        void LoadDataBase(string database);
        bool Exists();
        void Store<T>(T Item);
        T RestoreSingle<T>(T Item);
        IEnumerable<T> RestoreMany<T>(T item);
    }
}
