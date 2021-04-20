using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Controllers.Resources;
using ProductAPI.Interfaces;
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
        private readonly IProductRepository productRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductCategoriesController(ApplicationDbContext context, IMapper mapper, IProductRepository productRepo, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.productRepo = productRepo;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Policy = "UserWithActiveStatus")]
        [HttpGet("/api/categories")]
        public async Task<IEnumerable<ProductCategoryResource>> GetProductCategories()
        {
            //var categories = await context.ProductCategories.Include(m => m.Products).ToListAsync();
            //var categories = await context.ProductCategories.ToListAsync();
            var categories = await productRepo.GetProductCategories();

            return mapper.Map<List<ProductCategory>, List<ProductCategoryResource>>(categories.ToList());
        }

        [HttpPost("/api/products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(productResource.ProductImage != null)
            {
                string folder = "products/images";
                folder += Guid.NewGuid().ToString() + "_" + productResource.ProductImage;

                productResource.ImagePath = folder;

                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                await productResource.ProductImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            
            var product = mapper.Map<ProductResource, Product>(productResource);

            var productReturn = await productRepo.CreateProduct(product);//context.Products.Add(product);
            //await context.SaveChangesAsync();

            return Ok(productReturn);
        }

        [HttpGet("/api/products/{id}")]
        public async Task<IActionResult> GetProductbyId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await productRepo.GetProductbyId(id);//await context.Products.FindAsync(id);

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

            var product = await productRepo.GetProductbyId(id);          

            if (product == null)
                return NotFound();

            if (productResource.ProductImage != null)
            {
                string folder = "products/images";
                folder += Guid.NewGuid().ToString() + "_" + productResource.ProductImage;

                productResource.ImagePath = folder;

                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                await productResource.ProductImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

            mapper.Map<ProductResource, Product>(productResource, product);

            var productUpdated = await productRepo.UpdateProduct(id, product);

            var result = mapper.Map<Product, ProductResource>(productUpdated, productResource);


            return Ok(result);
        }

        [HttpDelete("/api/products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await productRepo.GetProductbyId(id);

            if (product == null)
                return NotFound();

            productRepo.DeleteProduct(id);

            return Ok(id);
        }
        
        [HttpGet("/api/products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepo.GetAllProducts();

            if (products == null)
                return NotFound();

            var productResource = mapper.Map<List<Product>, List<ProductResource>>(products);

            return Ok(productResource);
            
        }
    }
}