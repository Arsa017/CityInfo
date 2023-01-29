﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)              // we want to inject contract of Mapper, not the exact implementation
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(string? name)
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync(name);

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));      // mapping each item in the source list to item into destination list
        }

        [HttpGet("{id}")]       // to work with paramaters currly brackets are used!
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest) 
            {
                return Ok(_mapper.Map<CityDto>(city));                                  // returns ActionResult<CityDto>   -> needs generic aproach like IActionResult
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));               // returns ActionResult<CityWithoutPointsOfInterestDto>
        } 
    }
}
