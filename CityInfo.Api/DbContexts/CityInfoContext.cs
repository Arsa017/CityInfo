using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;                        // null forgiveing operator
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) 
            :base(options)
        {
        
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
