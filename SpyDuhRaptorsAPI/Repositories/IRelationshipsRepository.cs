using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IRelationshipsRepository
    {
        IList<UserDto> GetAllFriends(int userId);
        IList<UserDto> GetAllEnemies(int userId);
    }
}
