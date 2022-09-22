using Castle.Core.Resource;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using RepositoryLayer;
using RepositoryLayer.RespositoryPattern;
using ServicesLayer.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.FakeDataProvider;

namespace Tests.Services
{
    public class CustomerServiceTests
    {
        protected readonly ApplicationDbContext _context;
        public CustomerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

        }
        [Fact]
        public void GetAll_ReturnsCollection()
        {
            /// Arrange
            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(_ => _.GetAll()).Returns(PersonFakeData.GetAllCustomers());
            var customerService = new CustomerService(repository.Object);

            /// Act
            var result = customerService.GetAllCustomers();

            /// Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any());
        }
        [Fact]
        public void GetCustomer_ReturnsCustomer()
        {
            var customer = new Customer
            {
                Id = 1,
                CustomerName = "David",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            /// Arrange
            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(_ => _.Get(customer.Id)).Returns(customer);
            var customerService = new CustomerService(repository.Object);

            /// Act
            var result = customerService.GetCustomer(customer.Id);

            /// Assert
            Assert.NotNull(result);
            //Assert.NotEmpty(result);

        }

        [Fact]
        public void InsertCustomer_ShouldInsert()
        {
            var repository = new Repository<Customer>(_context);
            var customer = new Customer
            {
                Id = 1,
                CustomerName = "David",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };

            var customerService = new CustomerService(repository);

            customerService.InsertCustomer(customer);

            var retrivedCustomer = repository.Get(customer.Id);

            Assert.NotNull(retrivedCustomer);
            Assert.Equal(customer.Id, retrivedCustomer.Id);
            Assert.Equal(customer.CustomerName, retrivedCustomer.CustomerName);

        }
        [Fact]
        public void UpdateCustomer_Updates()
        {

            var repository = new Repository<Customer>(_context);
            var customerService = new CustomerService(repository);

            var customer = new Customer
            {
                Id = 1,
                CustomerName = "David",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            customerService.InsertCustomer(customer);
            var customer2 = new Customer
            {
                Id = 1,
                CustomerName = "David silwal",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Paid"
            };

            var retrivedCustomerbefore = repository.Get(customer.Id);

           // var customerService = new CustomerService(repository);

            customerService.UpdateCustomer(customer);

            var retrivedCustomer = repository.Get(customer.Id);

            Assert.NotNull(retrivedCustomerbefore);
            Assert.NotNull(retrivedCustomer);
            Assert.Equal(customer.Id, retrivedCustomer.Id);
            Assert.Equal(customer.CustomerName, retrivedCustomer.CustomerName);

        }

        [Fact]
        public void DeleteCustomer_Deletes()
        {
            var repository = new Repository<Customer>(_context);
            var customerService = new CustomerService(repository);
            var customer = new Customer
            {
                Id = 1,
                CustomerName = "David",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            customerService.InsertCustomer(customer);
            var retrivedCustomerbefore = repository.Get(customer.Id);
           
            int one = 1;
            customerService.DeleteCustomer(one);

            var retrivedCustomerafter = repository.Get(customer.Id);

            Assert.NotNull(retrivedCustomerbefore);
            Assert.Null(retrivedCustomerafter);
        }
    }

}
