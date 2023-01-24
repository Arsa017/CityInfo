using CityInfo.Api.Model;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route(("api/cities"))]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)             // we want to inject contract, not the exact implementation
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();

            var result = new List<CityWithoutPointsOfInterestDto>();                // mapping enities to CityWithoutPointsOfInterestDto model
            foreach (var cityEntity in cityEntities)                    
            {
                result.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                });
            }

            return Ok(result);
            //  return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}")]       // to work with paramaters currly brackets are used!
        public ActionResult<CityDto> GetCity(int id)
        {


            //// find city
            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            //return Ok(city);
            return Ok();
        } 
    }
}
