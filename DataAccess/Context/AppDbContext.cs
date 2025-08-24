using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Person> Person => Set<Person>();
        public DbSet<Address> Address => Set<Address>();
        public DbSet<PersonHobby> PersonHobbies => Set<PersonHobby>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Person)
                .HasForeignKey<Address>(a => a.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersonHobby>()
                .HasKey(pc => new { pc.PersonId, pc.Hobby });

            modelBuilder.Entity<PersonHobby>()
                .HasOne(pc => pc.Person)
                .WithMany(p => p.PersonHobbies)
                .HasForeignKey(pc => pc.PersonId);

            modelBuilder.Entity<Address>()
                .Property(a => a.Country)
                .HasConversion<int>();
        }
    }
}
