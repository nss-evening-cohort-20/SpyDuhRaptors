using Microsoft.AspNetCore.Mvc;
using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Controllers
{
    public interface IServicesLookUpController
    {
        ActionResult<IEnumerable<ServicesLookUp>> GetAll();
        ActionResult<ServicesLookUp> GetById(int id);
    }
}