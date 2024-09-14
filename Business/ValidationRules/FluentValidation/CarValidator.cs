using Core.Utilities.Helpers.Abstracts;
using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()

        {
            RuleFor(c => c.BrandId).GreaterThan(0);
            RuleFor(c=>c.ModelYear).GreaterThan(0);
            RuleFor(c=>c.ModelYear).LessThan(DateTime.Now.Year);
            RuleFor(c=>c.ColorId).GreaterThan(0);
            RuleFor(c => c.DailyPrice).Must(StartsWithNeg)
                .WithMessage("Car prices cannot be negative");
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(3);
            RuleFor(c => c.Name).NotEmpty();

        }

        private bool StartsWithNeg(string arg)
        {
            return !(arg.StartsWith("-"));
        }
    }
}
