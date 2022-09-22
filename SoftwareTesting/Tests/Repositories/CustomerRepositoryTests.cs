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

namespace Tests.Repositories
{
    public class CustomerRepositoryTests
    {

        protected readonly ApplicationDbContext _context;
        public CustomerRepositoryTests()
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

            var mockData = PersonFakeData.GetAllCustomers();
            _context.Customers.AddRange(mockData);
            _context.SaveChanges();
            var repository = new Repository<Customer>(_context);

            /// Act
            var result = repository.GetAll();

            /// Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any());
        }



        [Fact]
        public void InsertCustomer_ShouldInsert()
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

            var retrivedCustomer = repository.Get(customer.Id);

            Assert.NotNull(retrivedCustomer);
            Assert.Equal(customer.Id, retrivedCustomer.Id);
            Assert.Equal(customer.CustomerName, retrivedCustomer.CustomerName);

        }

        [Fact]
        public void GetCustomer_ReturnsOneCustomer()
        {

            var mockData = PersonFakeData.GetAllCustomers();
            _context.Customers.AddRange(mockData);
            _context.SaveChanges();

            var repository = new Repository<Customer>(_context);
            var customerService = new CustomerService(repository);


            var RetrivedCustomer = customerService.GetCustomer(1);
            Assert.NotNull(RetrivedCustomer);

        }

        [Fact]
        public void UpdateCustomer_ShouldUpdate()
        {
            var mockData = PersonFakeData.GetAllCustomers();
            _context.Customers.AddRange(mockData);
            _context.SaveChanges();
            //var repository = new Repository<Customer>(_context);

            var repository = new Repository<Customer>(_context);
            var customerService = new CustomerService(repository);
            var customer = new Customer
            {
                Id = 1,
                CustomerName = "suman",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            var DoesCustomerExist = customerService.GetCustomer(customer.Id);
            _context.ChangeTracker.Clear();

            customerService.UpdateCustomer(customer);

            var retrivedCustomer = customerService.GetCustomer(customer.Id);

            Assert.NotNull(DoesCustomerExist);
            Assert.NotNull(retrivedCustomer);
            Assert.Equal(customer.Id, retrivedCustomer.Id);
            Assert.Equal(customer.CustomerName, retrivedCustomer.CustomerName);


        }

        [Fact]
        public void DeleteCustomer_ShouldDelete()
        {
            var mockData = PersonFakeData.GetAllCustomers();
            _context.Customers.AddRange(mockData);
            _context.SaveChanges();
            var repository = new Repository<Customer>(_context);
            var customerService = new CustomerService(repository);

            int IdOfCustomerToBeDeleted = 1;

            var retrivedCustomerBeforeDeletion = customerService.GetCustomer(IdOfCustomerToBeDeleted);
            customerService.DeleteCustomer(IdOfCustomerToBeDeleted);
            var retrivedCustomerAfterDeletion = customerService.GetCustomer(IdOfCustomerToBeDeleted);


            Assert.NotNull(retrivedCustomerBeforeDeletion);
            Assert.Null(retrivedCustomerAfterDeletion);
        }

    }
}
