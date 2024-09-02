using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r=>r.RentDate).NotEmpty();
            RuleFor(r=>r.RentDate).LessThan(DateTime.Now);
            RuleFor(r => r.ReturnDate).GreaterThan(DateTime.Now);
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.CarId).GreaterThan(0);
            RuleFor(r => r.CustomerId).GreaterThan(0);
            RuleFor(r => r.CustomerId).NotEmpty();
        }
    }
}
