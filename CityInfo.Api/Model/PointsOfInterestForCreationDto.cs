using System.ComponentModel.DataAnnotations;

namespace CityInfo.Api.Model
{
    public class PointsOfInterestForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
