using AutoMapper;

namespace CityInfo.Api.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Model.CityWithoutPointsOfInterestDto>();       // creating map from City Entiti (SOURCE TYPE) to CityWithoutPointsOfInterestDto model (DESTINATION TYPE)
            CreateMap<Entities.City, Model.CityDto>();
        }
    }
}
