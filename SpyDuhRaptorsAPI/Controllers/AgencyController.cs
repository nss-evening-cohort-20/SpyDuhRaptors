using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _agencyRepository;
        public AgencyController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        // https://localhost:5001/api/AgencyLookup/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_agencyRepository.GetAll());
        }

        // https://localhost:5001/api/AgencyLookup/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var variety = _agencyRepository.Get(id);
            if (variety == null)
            {
                return NotFound();
            }
            return Ok(variety);
        }

        // https://localhost:5001/api/AgencyLookup/
        [HttpPost]
        public IActionResult Post(Agency agency)
        {
            _agencyRepository.Add(agency);
            return CreatedAtAction("Get", new { id = agency.Id }, agency);
        }

        // https://localhost:5001/api/AgencyLookup/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Agency agency)
        {
            if (id != agency.Id)
            {
                return BadRequest();
            }

            _agencyRepository.Update(agency);
            return NoContent();
        }

        // https://localhost:5001/api/AgencyLookup/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agencyRepository.Delete(id);
            return NoContent();
        }
    }
}
