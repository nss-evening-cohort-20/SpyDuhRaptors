using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // https://localhost:5001/api/Users/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetAll());
        }

        // https://localhost:5001/api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var variety = _userRepository.Get(id);
            if (variety == null)
            {
                return NotFound();
            }
            return Ok(variety);
        }

        // https://localhost:5001/api/Users/
        [HttpPost]
        public IActionResult Post(User user)
        {
            _userRepository.Add(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        // https://localhost:5001/api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userRepository.Update(user);
            return NoContent();
        }

        // https://localhost:5001/api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return NoContent();
        }
    }
}
