
namespace DataAccess.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public List<Hobby> Hobbies { get; set; } = new();
        public Address? Address { get; set; }
    }
}
