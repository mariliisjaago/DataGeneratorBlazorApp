
namespace DataAccess.Models.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public List<HobbyDto> Hobbies { get; set; } = new();
        public AddressDto Address { get; set; } = null!;
    }
}
