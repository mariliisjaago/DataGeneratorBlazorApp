using DataAccess.Models.Dtos;

namespace DataGeneratorServices.Interfaces
{
    public interface IDataSaverService
    {
        public void Save(List<PersonDto> persons);
    }
}
