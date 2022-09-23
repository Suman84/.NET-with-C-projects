using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using CustomersProductsWebApp.Models;
using DomainLayer.Models;
using DomainLayer.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataAccessLayer;

namespace CustomersProductsWebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext customerDbContext;

        public CustomersController(AppDbContext customerDbContext)
        {
            this.customerDbContext = customerDbContext;
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var customers = await customerDbContext.Customers.ToListAsync();
            return View(customers);

        }


        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerViewModel addCustomerRequest, [FromServices] IValidator<Customer> validator)
        {
            validator = new CustomerValidator();

            var customer = new Customer()
            {
                Name = addCustomerRequest.Name,
                Email = addCustomerRequest.Email,
                PaymentType = addCustomerRequest.PaymentType,
                PhoneNumber = addCustomerRequest.PhoneNumber,
                Address = addCustomerRequest.Address,
                PID = addCustomerRequest.PID
            };
            var validated = validator.Validate(customer);

            if (validated.IsValid)
            {
                await customerDbContext.Customers.AddAsync(customer);
                await customerDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Invalid format");
            }

        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var customer = await customerDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if( customer != null)
            {
                var viewModel = new UpdateCustomerViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    PaymentType = customer.PaymentType,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    PID = customer.PID
                };


                return await Task.Run(() => View("View",viewModel));

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCustomerViewModel model, [FromServices] IValidator<Customer> validator)
        {
            validator = new CustomerValidator();
            var customer = await customerDbContext.Customers.FindAsync(model.Id);

            if(customer != null)
            {
                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.PaymentType = model.PaymentType;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Address = model.Address;
                customer.PID = model.PID;

                var validated = validator.Validate(customer);

                if (validated.IsValid)
                {
                    await customerDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCustomerViewModel model)
        {
            var customer = await customerDbContext.Customers.FindAsync(model.Id);
            if(customer!= null)
            {
                customerDbContext.Customers.Remove(customer);
                await customerDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
