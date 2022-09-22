using Bogus;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public async Task InitializedAsync()
        {

            if (!(await Customers.AnyAsync()))
            {
                var payment = new string[] { "Paid", "Invoiced", "UnPaid" };

                var personFaker = (new Faker<Customer>()
                    .RuleFor(p => p.CustomerName, f => f.Person.FullName)
                    .RuleFor(p => p.PaymentType, f => f.PickRandom(payment))
                    .RuleFor(p => p.PurchasesProduct, f => f.Commerce.Product()))
                    .Generate(1000);

                foreach (var person in personFaker)
                {
                    var address = (new Faker<Address>()
                        .RuleFor(p => p.Country, f => f.Address.County())
                        .RuleFor(p => p.City, f => f.Address.City())).GenerateBetween(1, 5);
                    person.Addresses = address;
                }

                await Customers.AddRangeAsync(personFaker);
                await SaveChangesAsync();
            }

        }
    }
}
