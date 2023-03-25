using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IAgencyRepository
    {
        void Add(Agency variety);
        void Delete(int id);
        Agency Get(int id);
        List<Agency> GetAll();
        void Update(Agency variety);
    }
}