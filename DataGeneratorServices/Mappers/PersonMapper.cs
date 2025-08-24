using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Models.Dtos;

namespace DataGeneratorServices.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto MapToDto(this Person person)
        {
            if (person == null) return null;

            PersonDto personDto = new PersonDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Address = person.Address != null ? person.Address.MapToDto() : null,
                Hobbies = person.PersonHobbies != null ? person.PersonHobbies.Select(h => h.Hobby).ToList() : new List<HobbyEnum>()
            };

            return personDto;
        }

        public static AddressDto MapToDto(this Address address)
        {
            return new AddressDto()
            {
                StreetAddress = address.StreetAddress,
                City = address.City,
                ZipCode = address.ZipCode,
                Country = address.Country
            };
        }

        public static Person MapToDbObject(this PersonDto personDto)
        {
            if (personDto == null) return null;

            Person person = new Person()
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                DateOfBirth = personDto.DateOfBirth,
                Address = personDto.Address != null ? personDto.Address.MapToDbObject() : null
            };

            person.PersonHobbies = personDto.Hobbies != null ? personDto.Hobbies.Select(h => new PersonHobby() { Person = person, Hobby = h }).ToList() : null;

            return person;
        }

        public static Address MapToDbObject(this AddressDto addressDto)
        {
            return new Address()
            {
                StreetAddress = addressDto.StreetAddress,
                City = addressDto.City,
                ZipCode = addressDto.ZipCode,
                Country = addressDto.Country
            };
        }
    }
}
