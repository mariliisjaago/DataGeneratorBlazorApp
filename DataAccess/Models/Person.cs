
namespace DataAccess.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public ICollection<PersonHobby> PersonHobbies { get; set; } = new List<PersonHobby>();
        public Address? Address { get; set; }
    }
}
