using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Customer Name is required");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Email must in in email format");
            RuleFor(p => p.PaymentType).NotNull().WithMessage("Payment type is required");
            RuleFor(p => p.Address).NotNull().WithMessage("Address is required");
            RuleFor(p => p.PhoneNumber).GreaterThan(9000000000).LessThan(9999999999).WithMessage("Phone Number is invalid length");
            RuleFor(p => p.PID).LessThan(10).WithMessage("NO such product exists");
        }
    }
}
