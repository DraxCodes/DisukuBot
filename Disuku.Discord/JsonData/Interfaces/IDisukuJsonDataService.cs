using System.Threading.Tasks;

namespace Disuku.Discord.JsonData.Interfaces
{
    public interface IDisukuJsonDataService
    {
        Task<T> Retreive<T>(string path);
        Task Save(object obj, string path);
        bool FileExists(string path);
    }
}
