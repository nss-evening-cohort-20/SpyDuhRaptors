using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public AssignmentController(IAssignmentRepository assigmentRepository)
        {
            _assignmentRepository = assigmentRepository;
        }

        // https://localhost:5001/api/Assignment/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_assignmentRepository.GetAll());
        }

        // https://localhost:5001/api/Assignment/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var variety = _assignmentRepository.Get(id);
            if (variety == null)
            {
                return NotFound();
            }
            return Ok(variety);
        }

        // https://localhost:5001/api/Assignment/
        [HttpPost]
        public IActionResult Post(Assignment assignment)
        {
            _assignmentRepository.Add(assignment);
            return CreatedAtAction("Get", new { id = assignment.Id }, assignment);
        }

        // https://localhost:5001/api/Assignment/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return BadRequest();
            }

            _assignmentRepository.Update(assignment);
            return NoContent();
        }

        // https://localhost:5001/api/Assignment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _assignmentRepository.Delete(id);
            return NoContent();
        }
    }
}
