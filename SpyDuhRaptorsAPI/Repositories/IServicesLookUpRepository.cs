using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IServicesLookUpRepository
    {
        List<ServicesLookUp> GetAll();
        ServicesLookUp? GetById(int id);
    }
}