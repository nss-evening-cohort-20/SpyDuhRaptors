using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IRelationshipsRepository
    {
        IList<RelationshipsDto> GetAllFriends(int userId);
        IList<RelationshipsDto> GetAllEnemies(int userId);
    }
}
