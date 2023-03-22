using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI.Controllers
{
    public class ServicesController : Controller
    // GET: ServicesController
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        // https://localhost:7131/api/services/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_servicesRepository.GetAll());
        }

     

    }
      
}
