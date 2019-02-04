using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisukuData.Interfaces
{
    public interface IDisukuJsonDataService
    {
        Task<T> Retreive<T>(string path);
        Task Save(object obj, string path);
        bool Exists(string path);
    }
}
