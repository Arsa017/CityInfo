using CityInfo.Api.DbContexts;
using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();                 // returns all cities asinchronously, ordered by city name
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery,
            int pageNumber, int pageSize)
        { 
         
            // collection to start from
            var collection = _context.Cities as IQueryable<City>;

            if (!string.IsNullOrEmpty(name))                                                // applying fillter for city name
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrEmpty(searchQuery))                                         // applying search for searchQuery
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            // when ToListAsync() statement is reached, our builded Query is sent to the database; always add paging funcionality last of query
            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber  - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointOfInterests)               // when return a city, we want to include all PointsOfInterest where it's id is cityId
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();  // return a city without it's PointsOfInterest
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointOfInterests.Add(pointOfInterest);
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);                // call SaveChangesAsync() returns amount of entities that have been changed
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }
    }
}
