using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IServicesRepository
    {
        bool Delete(int id);
        List<Services> GetAll();
        Services? GetById(int id);
        bool Insert(Services service);
        bool Update(Services services);
    }
}