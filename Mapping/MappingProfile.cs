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
            //Domain to API Resource
            CreateMap<ProductCategory, ProductCategoryResource>();
            CreateMap<Product, ProductResource>();

            //API Resource to Domain
            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore()); 
        }
    }
}
