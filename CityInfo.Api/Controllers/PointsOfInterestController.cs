using CityInfo.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            try
            {
                //throw new Exception("Test greska!");
                var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id: {cityId} wasn't found when accessing the points of interes.");
                    return NotFound();
                }

                return Ok(city.PointOfInterests);
            } catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interest for city with ID: {cityId}.", ex);
                return StatusCode(500, "A problem happend while handling your request.");
            }

            
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

        [HttpPost]  // adding new resource with provided values
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

        [HttpPut("{pointOfInterestId}")]   // fully update of resource; consumer of the API must provide values for ALL fields for resource
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
           [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // find point of interest
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(
                p => p.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]    // pratial update of resource; update only for fields specified into the patchDocument    
        public ActionResult PartiallyUpdatePointOfInterest(
            int cityId, int pointOfInterestId,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);          // applying patch document on the object that we want to patch
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
        
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId) 
        {
            var city = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }

            city.PointOfInterests.Remove(pointOfInterestFromStore);
            return NoContent();
        }

    }
}
