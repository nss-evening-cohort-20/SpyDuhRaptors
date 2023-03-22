using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SpyDuhRaptorsAPI.Repositories;
using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesLookUpController : ControllerBase
    {
        private readonly ServicesLookUpRepository _repository;

        public ServicesLookUpController(ServicesLookUpRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServicesLookUp>> GetAll()
        {
            var results = _repository.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public ActionResult<ServicesLookUp> GetById(int id)
        {
            var result = _repository.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
