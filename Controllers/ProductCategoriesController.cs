using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Controllers.Resources;
using ProductApp.Data;
using ProductApp.Models;

namespace ProductAPI.Controllers
{
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
            var categories = await context.ProductCategories.Include(m => m.Products).ToListAsync();

            return mapper.Map<List<ProductCategory>, List<ProductCategoryResource>>(categories);
        }
    }
}