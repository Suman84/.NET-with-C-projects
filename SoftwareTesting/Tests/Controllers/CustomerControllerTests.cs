using DomainLayer.Models;
using FluentAssert;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnionArchitecture.Controllers;
using RepositoryLayer.RespositoryPattern;
using ServicesLayer.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.FakeDataProvider;

namespace Tests.Controllers
{
    public class CustomerControllerTests
    {
        [Fact]
        public void GetAllCustomer_Returns_OK()
        {
            /// Arrange
            var customerService = new Mock<ICustomerService>();
            customerService.Setup(_ => _.GetAllCustomers()).Returns(PersonFakeData.GetAllCustomers());

            var controller = new CustomerController(customerService.Object);

            /// Act
            var result = (OkObjectResult)controller.GetAllCustomer();
            var result2 = controller.GetAllCustomer();

            /// Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            Assert.NotNull(result2);
           // Assert.Equal(result2.Count(), 1000);
           // Assert.True(result.Any());

            //NEED TO WRITE MORE ASSERTION
        }

       [Fact]
       public void GetCustomer_Returns_Ok()
        {
            var customer = new Customer()
            {
                Id = 1,
                CustomerName = "David",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            var customerService = new Mock<ICustomerService>();
            customerService.Setup(_ => _.GetCustomer(customer.Id)).Returns(customer);
            var controller = new CustomerController(customerService.Object);

            int IdOfCustomer = 10;

            //var result = (OkObjectResult)controller.GetCustomer(IdOfCustomer);
            var result2 = controller.GetCustomer(IdOfCustomer);

            //result2.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            Assert.NotNull(result2);
            //Assert.Equal(result2.Count(), 1);
            // Assert.True(result2.Any());

        }

        [Fact]
        public void InsertCustomer_returns_Ok()
        {

            var customerService = new Mock<ICustomerService>();
            var controller = new CustomerController(customerService.Object);
            var customer = new Customer
            {
                Id = 1,
                CustomerName = "suman",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            var IdDoesnotExist = controller.GetCustomer(customer.Id);

            var result = (OkObjectResult)controller.InsertCustomer(customer);
            var result2 = controller.InsertCustomer(customer);

            var retrivedCustomer = controller.GetCustomer(customer.Id);

            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            //Assert.Equal(customer.Id, retrivedCustomer.Id);
            //Assert.Equal(customer.CustomerName, retrivedCustomer.CustomerName);

        }
        [Fact]
        public void UpdateCustomers_Returns_OK()
        {
            var customerService = new Mock<ICustomerService>();
            var controller = new CustomerController(customerService.Object);
            var customer = new Customer
            {
                Id = 1,
                CustomerName = "suman2",
                CreatedDate = DateTime.Now,
                PurchasesProduct = "Test",
                PaymentType = "Invoiced"
            };
            var IdDoesExist = controller.GetCustomer(customer.Id);
            var result = (OkObjectResult)controller.UpdateCustomer(customer);
            var result2 = controller.UpdateCustomer(customer);

            var retrivedCustomer = controller.GetCustomer(customer.Id);

            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            Assert.NotNull(IdDoesExist);
            Assert.NotNull(retrivedCustomer);

        }
        [Fact]
        public void DeleteCustomers_Returns_Ok()
        {
            var customerService = new Mock<ICustomerService>();
            var controller = new CustomerController(customerService.Object);

            int IdOfCustomerToBeDeleted = 1;

            var retrivedCustomerBeforeDeletion = controller.GetCustomer(IdOfCustomerToBeDeleted);
            var result = (OkObjectResult)controller.DeleteCustomer(IdOfCustomerToBeDeleted);
            var retrivedCustomerAfterDeletion = controller.GetCustomer(IdOfCustomerToBeDeleted);

            Assert.NotEqual(retrivedCustomerAfterDeletion, retrivedCustomerBeforeDeletion);
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

            //retrivedCustomerBeforeDeletion.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            //retrivedCustomerAfterDeletion.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
        }

    }
}
