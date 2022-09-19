using System;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.DataAccessLayer;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CustomersProductsWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await dbContext.Products.ToListAsync());
            
        }

        [HttpPost]
        [Route("ADD")]
        public async Task<IActionResult> AddPerson(AddProduct _products)
        {
            var Products = new Product()
            {
                Name = _products.Name, 
                Description = _products.Description,
                Price = _products.Price
            };

            await dbContext.Products.AddAsync(Products);
            await dbContext.SaveChangesAsync();

            return Ok(Products);
        }

        //UPDATE DATA
        [HttpPut]
        [Route("Update/{Id:int}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] int Id,UpdateProduct _products)
        {
            var Products = await dbContext.Products.FindAsync(Id);
            if (Products != null)
            {
                Products.Name = _products.Name;
                Products.Description = _products.Description;
                Products.Price = _products.Price;

                await dbContext.SaveChangesAsync();

                return Ok(Products);
            }

            return NotFound();
        }

        //DELETE DATA
        [HttpDelete]
        [Route("DELETE/{Id:int}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int Id)
        {
            var Products = await dbContext.Products.FindAsync(Id);
            if (Products != null)
            {
                dbContext.Remove(Products);
                await dbContext.SaveChangesAsync();
                return Ok(Products);
            }
            return NotFound(nameof(Products));
        }
    }
}
