using CityInfo.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route(("api/cities"))]
    public class CitiesController : ControllerBase
    {
        private readonly CityDataStore _citiesDataStore;

        public CitiesController(CityDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}")]       // to work with paramaters currly brackets are used!
        public ActionResult<CityDto> GetCity(int id)
        {
            // find city
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        } 
    }
}
