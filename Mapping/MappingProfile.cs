using AutoMapper;
using ProductAPI.Controllers.Resources;
using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryResource>();
            CreateMap<Product, ProductResource>();
        }
    }
}
