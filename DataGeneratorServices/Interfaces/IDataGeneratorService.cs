
using DataAccess.ApiModels;
using DataAccess.Models.Dtos;

namespace DataGeneratorServices.Interfaces
{
    public interface IDataGeneratorService
    {
        List<PersonDto> GenerateSampleData(GeneratorRequest request);
    }
}
