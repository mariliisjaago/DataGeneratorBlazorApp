
namespace DataAccess.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Person> Persons { get; set; } = new();
    }
}
