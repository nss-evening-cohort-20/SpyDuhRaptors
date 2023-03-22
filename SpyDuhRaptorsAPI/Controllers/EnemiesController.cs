using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnemiesController : ControllerBase
    {
        private readonly IRelationshipsRepository _relationshipsRepository;

        public EnemiesController(IRelationshipsRepository relationshipsRepository)
        {
            _relationshipsRepository = relationshipsRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var enemiesDtos = _relationshipsRepository.GetAllEnemies(id);
            if (enemiesDtos == null)
            {
                return NotFound();
            }
            return Ok(enemiesDtos);
        }
    }
}
