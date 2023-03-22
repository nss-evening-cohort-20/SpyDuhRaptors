using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    // GET: ServicesController
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }
        //Get All//
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_servicesRepository.GetAll());
        }
        // GetId//
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var found = _servicesRepository.GetById(id);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
        //Post//
        [HttpPost]
        public IActionResult Post(Services services)
        {
            try
            {
                if (!_servicesRepository.Insert(services))
                {
                    return BadRequest(services);
                }

                return CreatedAtAction("Get", new { id = services.Id }, services);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint"))
                {
                    return BadRequest("That doesn't exist, you can't do that!");
                }

                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        //Put//
        [HttpPut("{id}")]
        public IActionResult Put(int id, Services services)
        {
            try
            {
                if (id != services.Id || !_servicesRepository.Update(services))
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Delete//
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var found = _servicesRepository.GetById(id) is not null;
                if (!found)
                {
                    return NotFound();
                }

                if (!_servicesRepository.Delete(id))
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }        
      
}
