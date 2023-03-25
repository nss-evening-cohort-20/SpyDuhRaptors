using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IUserRepository
    {
        void Add(User variety);
        void Delete(int id);
        UserDto Get(int id);
        List<UserDto> GetAll();
        void Update(User variety);
    }
}