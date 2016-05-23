using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Secullum.Validation.Tests
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext()
        {
            People.Add(new Person() { Id = 1, Name = "fernando" });
            SaveChanges();
        }

        public DbSet<Person> People { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }
    }
}
