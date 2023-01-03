using CityInfo.Api.Model;

namespace CityInfo.Api
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CityDataStore Current { get; } = new CityDataStore();  // singleton pattern design
        public CityDataStore()
        {
            Cities= new List<CityDto>()
                {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park."
                },
                new CityDto() {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finnished."
                },
                new CityDto() {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower."
                }
            };
        }
    }
}
