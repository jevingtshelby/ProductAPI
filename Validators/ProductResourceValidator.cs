using FluentValidation;
using ProductAPI.Controllers.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Validators
{
    public class ProductResourceValidator : AbstractValidator<ProductResource>
    {
        public ProductResourceValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Expiry).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.ProductCategoryId).NotEmpty();
        }

    }
}
