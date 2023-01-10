using CityInfo.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
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

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(      // returns created resourse in the response
            int cityId, [FromBody] PointsOfInterestForCreationDto pointOfInterest)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // demo purposes - to be improved
            var maxPointOfInterestId = CityDataStore.Current.Cities.SelectMany(
                c => c.PointOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterests.Add(finalPointOfInterest);

            return CreatedAtRoute(routeName: "GetPointOfInterest",
                routeValues: new
                {
                    cityId = cityId,                                    // name of parameters must be the same as the name of action parameters!
                    pointOfInterestId = finalPointOfInterest.Id
                },
                value: finalPointOfInterest
                );
        }

    }
}
