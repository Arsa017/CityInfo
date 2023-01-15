using CityInfo.Api.Model;

namespace CityInfo.Api
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }

    //    public static CityDataStore Current { get; } = new CityDataStore();  // singleton pattern design


        public CityDataStore()
        {
            Cities = new List<CityDto>()
                {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the United States."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper located in Midtown Manhattan."
                        }
                    }
                },
                new CityDto() {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finnished.",
                    PointOfInterests = new List<PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Cathedral of Our Lady",
                            Description = "A Ghotic style cathedral, concevied by architects Jan and Pietter.."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Antwerp Central Station",
                            Description = "The finest example of railway architecture in Belgium."
                        }
                    }
                },
                new CityDto() {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                    PointOfInterests = new List<PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Eiffel Tower",
                            Description = "A wrought iron lattice tower on the Champ de Mars, named after..."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "The Louvre",
                            Description = "The world's largest museum."
                        }
                    }
                }
            };
        }
    }
}
