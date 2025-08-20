using DataAccess.Enums;

namespace DataAccess.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public CountryEnum Country { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}
