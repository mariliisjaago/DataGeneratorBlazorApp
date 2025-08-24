using DataAccess;
using DataAccess.Models;
using DataAccess.Models.Dtos;
using DataGeneratorServices.Interfaces;
using DataGeneratorServices.Mappers;

namespace DataGeneratorServices.Implementations
{
    public class DataSaverService : IDataSaverService
    {
        private readonly IPersonRepository _personRepository;

        public DataSaverService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void Save(List<PersonDto> persons)
        {
            List<Person> dbObjects = persons.Select(p => p.MapToDbObject()).ToList();
            _personRepository.Save(dbObjects);
        }
    }
}
