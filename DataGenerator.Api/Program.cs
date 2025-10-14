using DataAccess;
using DataAccess.ApiModels;
using DataAccess.Context;
using DataAccess.Models.Dtos;
using DataGeneratorServices.Implementations;
using DataGeneratorServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            builder.Services.AddAuthorization();
            builder.Services.AddCors();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IDataGeneratorService, DataGeneratorService>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<IDataSaverService, DataSaverService>();


            var app = builder.Build();
            app.MapDefaultEndpoints();

            app.UseCors(policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.MapGet("/api/getall", async (AppDbContext db) =>
            {
                var people = await db.Person
                    .Include(p => p.PersonHobbies)
                    .Include(p => p.Address)
                    .Select(p => new PersonDto
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        DateOfBirth = p.DateOfBirth,
                        Hobbies = p.PersonHobbies.Select(h => h.Hobby).ToList(),
                        Address = new AddressDto()
                        {
                            StreetAddress = p.Address.StreetAddress,
                            City = p.Address.City,
                            ZipCode = p.Address.ZipCode,
                            Country = p.Address.Country
                        }
                    }).ToListAsync();

                return Results.Ok(people);
            });

            app.MapGet("/api/generate", (int dataPointCount, string? allowedFirstNames, [FromServices] IDataGeneratorService service) =>
            {
                var request = new GeneratorRequest() { DataPointCount = dataPointCount, AllowedFirstNames = allowedFirstNames };
                var data = service.GenerateSampleData(request);
                return Results.Ok(data);
            });

            app.MapPost("/api/save", async (List<PersonDto> persons, [FromServices] IDataSaverService service, AppDbContext db) =>
            {
                service.Save(persons);
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.Run();
        }
    }
}
