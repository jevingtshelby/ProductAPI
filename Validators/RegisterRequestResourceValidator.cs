using FluentValidation;
using ProductAPI.Controllers.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Validators
{
    public class RegisterRequestResourceValidator : AbstractValidator<RegisterRequestResource>
    {
        public RegisterRequestResourceValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty().Matches("^[a-zA-Z]*$");
            RuleFor(x => x.Lastname)
                .NotEmpty().Matches("^[a-zA-Z]*$");
            RuleFor(x => x.Email)
                .NotEmpty().Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        }
    }
}
