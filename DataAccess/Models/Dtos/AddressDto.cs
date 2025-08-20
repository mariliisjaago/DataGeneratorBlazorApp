using DataAccess.Enums;

namespace DataAccess.Models.Dtos
{
    public class AddressDto
    {
        public string StreetAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public CountryEnum Country { get; set; }
    }
}
