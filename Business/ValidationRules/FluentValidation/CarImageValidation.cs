using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidation : AbstractValidator<CarImage>
    {
        public CarImageValidation()
        {
            RuleFor(i=>i.CarId).NotEmpty();
            RuleFor(i=>i.CarId).GreaterThan(0);
        }
    }
}
