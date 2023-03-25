using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // https://localhost:5001/api/Countries/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_countryRepository.GetAll());
        }

        // https://localhost:5001/api/Countries/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var variety = _countryRepository.Get(id);
            if (variety == null)
            {
                return NotFound();
            }
            return Ok(variety);
        }

        // https://localhost:5001/api/Countries/
        [HttpPost]
        public IActionResult Post(Country country)
        {
            _countryRepository.Add(country);
            return CreatedAtAction("Get", new { id = country.Id }, country);
        }

        // https://localhost:5001/api/Countries/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _countryRepository.Update(country);
            return NoContent();
        }

        // https://localhost:5001/api/Countries/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _countryRepository.Delete(id);
            return NoContent();
        }
    }
}
