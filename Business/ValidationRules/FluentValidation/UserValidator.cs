using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            //lets check here later aight?
            RuleFor(u=>u.Email).NotEmpty();
            RuleFor(u=>u.Email).MinimumLength(6);
            RuleFor(u=>u.FirstName).MinimumLength(6);
            RuleFor(u=>u.FirstName).NotEmpty();
            RuleFor(u=>u.LastName).NotEmpty();
            RuleFor(u => u.LastName).MinimumLength(6);
            RuleFor(u=>u.Password).MinimumLength(4);
            RuleFor(u=>u.Password).NotEmpty();
        }
    }
}
