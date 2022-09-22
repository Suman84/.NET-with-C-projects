using Bogus;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.FakeDataProvider
{
    internal static class PersonFakeData
    {
        public static IEnumerable<Customer> GetAllCustomers()
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
            return personFaker;
        }
    }
}
