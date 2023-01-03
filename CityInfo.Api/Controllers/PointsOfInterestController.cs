using CityInfo.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);   
            
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{pointOfInterestId}")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            // finding point of interest for founded city
            var pointOfInterest = city.PointOfInterests.FirstOrDefault(p => p.Id == pointOfInterestId);
           
            if (pointOfInterest is null) 
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }
    }
}
