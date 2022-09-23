using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class ProductsValidator : AbstractValidator<Product>
    {
        public ProductsValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product Name is required");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required"); ;
            RuleFor(p => p.Price).NotEmpty().WithMessage("Price is required"); ;
        }

    }
}
