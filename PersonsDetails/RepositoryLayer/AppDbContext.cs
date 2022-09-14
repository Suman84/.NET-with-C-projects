using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions connection): base(connection)
        {

        }
        public DbSet<DomainLayer.Models.Person> Persons { get; set; }

        public async Task InitializedAsync()
        {

            if (!(await Persons.AnyAsync()))
            {
                //var Age = new int[] { 18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35 };
                int[] Age = Enumerable.Range(18, 100).ToArray();
                Random random1 = new();
                Random random2 = new();

                var personFaker = new Faker<DomainLayer.Models.Person>()
                    .RuleFor(p => p.Name, f => f.Person.FullName)
                    .RuleFor(p => p.Age, f => f.PickRandom(Age))
                    .RuleFor(p => p.Email, f => f.Person.FirstName + f.Person.LastName +random1.Next(1000,9999).ToString() + "@gmail.com")
                    .RuleFor(p => p.Phonenumber,f => random2.NextInt64(9800000000,9899999999))
                    .Generate(1000);
              
                await Persons.AddRangeAsync(personFaker);
                await SaveChangesAsync();
            }

        }



    }
}
