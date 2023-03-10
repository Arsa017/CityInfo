using CityInfo.Api.Entities;

namespace CityInfo.Api.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int PageNumber, int PageSize);
        Task<bool> CityExistsAsync(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);

        Task<bool> SaveChangesAsync();
    }
}
