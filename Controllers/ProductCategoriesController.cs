using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Controllers.Resources;
using ProductApp.Data;
using ProductApp.Models;

namespace ProductAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductCategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/categories")]
        public async Task<IEnumerable<ProductCategoryResource>> GetProductCategories()
        {
            //var categories = await context.ProductCategories.Include(m => m.Products).ToListAsync();
            var categories = await context.ProductCategories.ToListAsync();

            return mapper.Map<List<ProductCategory>, List<ProductCategoryResource>>(categories);
        }

        [HttpPost("/api/products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //extra noise not really needed
            //var productCategory = context.ProductCategories.FindAsync(productResource.ProductCategoryId);

            //if(productCategory == null)
            //{
            //    ModelState.AddModelError("ProductCategoryId", "Invalid ProductCategoryId");
            //    return BadRequest(ModelState);
            //}

            //var product = mapper.Map<ProductResource, Product>(productResource);
            var product = mapper.Map<ProductResource, Product>(productResource);

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("/api/products/{id}")]
        public async Task<IActionResult> GetProductbyId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            var result = mapper.Map<Product, ProductResource>(product);


            return Ok(result);
        }

        [HttpPut("/api/products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            mapper.Map<ProductResource, Product>(productResource, product);
            //.productResource.LastUpdatedBy = DateTime.Now; actually name of user who logged in

            await context.SaveChangesAsync();

            var result = mapper.Map<Product, ProductResource>(product, productResource);


            return Ok(result);
        }

        [HttpDelete("/api/products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            context.Remove(product);

            await context.SaveChangesAsync();

            return Ok(id);
        }
        
        [HttpGet("/api/products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            var productResource = mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
            
        }
    }
}