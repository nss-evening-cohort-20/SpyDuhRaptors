using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public interface IAssignmentRepository
    {
        void Add(Assignment variety);
        void Delete(int id);
        Assignment Get(int id);
        List<Assignment> GetAll();
        void Update(Assignment variety);
    }
}