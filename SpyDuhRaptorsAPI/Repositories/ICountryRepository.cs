using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface ICountryRepository
    {
        void Add(Country variety);
        void Delete(int id);
        Country Get(int id);
        List<Country> GetAll();
        void Update(Country variety);
    }
}