using DataAccess.Enums;

namespace DataAccess.Models
{
    public class PersonHobby
    {
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public HobbyEnum Hobby { get; set; }
    }
}
