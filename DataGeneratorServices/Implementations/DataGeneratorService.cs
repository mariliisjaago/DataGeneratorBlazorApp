using DataAccess.ApiModels;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Models.Dtos;
using DataGeneratorServices.Interfaces;
using DataGeneratorServices.Mappers;

namespace DataGeneratorServices.Implementations
{

    public class DataGeneratorService : IDataGeneratorService
    {
        private readonly string[] _firstNames = new string[8] { "Tom", "Mark", "Jenny", "Michelle", "Michael", "Jason", "Susan", "Linda" };
        private readonly string[] _lastNames = new string[8] { "Smith", "McGellan", "Harlow", "Booker", "Weston", "Klein", "Hardinger", "Bellingham" };
        private readonly Dictionary<int, int> _daysPerMonth = new Dictionary<int, int>() { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        //    private readonly string[] hobbies = new string[30]
        //        {
        //            "hiking", "painting", "gardening", "photography", "cooking",
        //"cycling", "knitting", "reading", "writing", "dancing",
        //"yoga", "swimming", "playing guitar", "baking", "birdwatching",
        //"calligraphy", "pottery", "fishing", "jogging", "origami",
        //"scrapbooking", "chess", "woodworking", "meditation", "gaming",
        //"rock climbing", "sketching", "playing piano", "blogging", "astrophotography"
        //        };
        private readonly string[] _streetNames = new string[8] { "Pine", "School", "River", "Fifth Avenue", "Blueswan", "Treewise", "Lovebird", "Barnacle" };
        private readonly string[] _cities = new string[8] { "Berlin", "London", "Washington", "Tallinn", "Tartu", "Madrid", "Paris", "Warsaw" };


        public List<PersonDto> GenerateSampleData(GeneratorRequest request)
        {
            string[] allowedFirstNames = GetAllowedFirstNames(request.AllowedFirstNames);

            var persons = GetPersons(request.DataPointCount, allowedFirstNames);
            return persons.Select(p => p.MapToDto()).ToList();
        }

        private string[] GetAllowedFirstNames(string allowedFirstNames)
        {
            if (string.IsNullOrEmpty(allowedFirstNames))
            {
                return _firstNames;
            }

            var parts = allowedFirstNames.Split(",").Select(n => n.Trim());
            if (!parts.Any())
            {
                return _firstNames;
            }

            var allowedList = new List<string>();
            foreach (var name in _firstNames)
            {
                if (allowedFirstNames.Contains(name))
                {
                    allowedList.Add(name);
                }
            }

            return allowedList.ToArray();
        }

        private Person[] GetPersons(int dataPoints, string[] allowedFirstNames)
        {
            Person[] persons = new Person[dataPoints];

            for (int i = 0; i < dataPoints; i++)
            {
                Person person = GeneratePerson(allowedFirstNames);
                persons[i] = person;
            }

            return persons;
        }

        private Person GeneratePerson(string[] allowedFirstNames)
        {
            Random random = new Random();

            DateTime dateOfBirth = GetDateOfBirth(random);
            List<HobbyEnum> hobbies = GetHobbies(random);
            string zipCode = GetZipCode(random);

            var person = new Person()
            {
                FirstName = allowedFirstNames[random.Next(0, allowedFirstNames.Length - 1)],
                LastName = _lastNames[random.Next(0, 7)],
                DateOfBirth = dateOfBirth,
                Address = new Address()
                {
                    StreetAddress = $"{_streetNames[random.Next(0, 7)]} {random.Next(1, 333)}",
                    City = _cities[random.Next(0, 7)],
                    ZipCode = zipCode,
                    Country = (CountryEnum)random.Next(1, 6)
                }
            };

            person.PersonHobbies = hobbies.Select(h => new PersonHobby() { Person = person, Hobby = h }).ToList();

            return person;
        }

        private string GetZipCode(Random random)
        {
            int firstPart = random.Next(0, 99);
            string firstPartStr = firstPart.ToString("D2");

            int secondPart = random.Next(0, 999);
            string secondPartStr = secondPart.ToString("D3");

            return $"{firstPartStr}-{secondPartStr}";
        }

        private List<HobbyEnum> GetHobbies(Random random)
        {
            int countOfHobbies = random.Next(0, 7);
            HashSet<HobbyEnum> result = new HashSet<HobbyEnum>(countOfHobbies);
            for (int i = 0; i < countOfHobbies; i++)
            {
                int randomIdx = random.Next(1, 30); // do not generate Unknown as hobby
                result.Add((HobbyEnum)randomIdx);
            }

            return result.ToList();
        }

        private DateTime GetDateOfBirth(Random random)
        {
            int yearInt = random.Next(1950, 2024);
            int monthInt = random.Next(1, 12);
            int dayInt = _daysPerMonth.GetValueOrDefault(monthInt, 1);

            return new DateTime(year: yearInt, month: monthInt, day: dayInt);
        }
    }
}
