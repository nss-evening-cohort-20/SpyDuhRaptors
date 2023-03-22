using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IRelationshipsRepository _relationshipsRepository;

        public FriendsController(IRelationshipsRepository relationshipsRepository)
        {
            _relationshipsRepository = relationshipsRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var friendsDtos = _relationshipsRepository.GetAllFriends(id);
            if (friendsDtos == null)
            {
                return NotFound();
            }
            return Ok(friendsDtos);
        }
    }
}
