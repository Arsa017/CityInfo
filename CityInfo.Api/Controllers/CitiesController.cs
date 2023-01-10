using CityInfo.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route(("api/cities"))]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CityDataStore.Current.Cities);
        }

        [HttpGet("{id}")]       // to work with paramaters currly brackets are used!
        public ActionResult<CityDto> GetCity(int id)
        {
            // find city
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        } 
    }
}
