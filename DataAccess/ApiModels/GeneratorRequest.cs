using System.ComponentModel.DataAnnotations;

namespace DataAccess.ApiModels
{
    public class GeneratorRequest
    {
        [Required]
        [Range(1, 10000)]
        public int DataPointCount { get; set; }
        public string AllowedFirstNames { get; set; }
    }
}
