using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IUserRepository
    {
        void Add(User variety);
        void Delete(int id);
        User Get(int id);
        List<User> GetAll();
        void Update(User variety);
    }
}