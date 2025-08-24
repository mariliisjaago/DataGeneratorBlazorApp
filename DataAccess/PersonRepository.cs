using DataAccess.Context;
using DataAccess.Models;

namespace DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _db;

        public PersonRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Save(List<Person> persons)
        {

            _db.AddRange(persons);
            _db.SaveChanges();
        }
    }
}
