using DataAccess.Models;
using DataAccess.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();

            app.UseCors(policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.MapGet("/api/persons", async (AppDbContext db) =>
            {
                var people = await db.Person
                    .Include(p => p.Hobbies)
                    .Include(p => p.Address)
                    .Select(p => new PersonDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        DateOfBirth = p.DateOfBirth,
                        Hobbies = p.Hobbies.Select(h => new HobbyDto
                        {
                            Id = h.Id,
                            Name = h.Name
                        }).ToList(),
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


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.Run();
        }

        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<Person> Person => Set<Person>();
            public DbSet<Address> Address => Set<Address>();
            public DbSet<Hobby> Hobby => Set<Hobby>();

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Person>()
                    .HasMany(p => p.Hobbies)
                    .WithMany(h => h.Persons)
                    .UsingEntity<Dictionary<string, object>>(
                        "HobbyInPerson",
                        j => j.HasOne<Hobby>()
                              .WithMany()
                              .HasForeignKey("HobbyId")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Person>()
                              .WithMany()
                              .HasForeignKey("PersonId")
                              .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("HobbyId", "PersonId");
                        }
                    );

                modelBuilder.Entity<Person>()
                    .HasOne(p => p.Address)
                    .WithOne(a => a.Person)
                    .HasForeignKey<Address>(a => a.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Address>()
                    .Property(a => a.Country)
                    .HasConversion<int>();
            }
        }
    }
}
