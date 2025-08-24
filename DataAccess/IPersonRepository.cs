using DataAccess.Models;

namespace DataAccess
{
    public interface IPersonRepository
    {
        public void Save(List<Person> persons);
    }
}
