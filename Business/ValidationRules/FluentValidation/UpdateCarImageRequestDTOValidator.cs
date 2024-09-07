using Entity.DTOs.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UpdateCarImageRequestDTOValidator : AbstractValidator<UpdateCarImageRequestDTO>
    {
        public UpdateCarImageRequestDTOValidator()
        {
            RuleFor(x => x.CarImageId)
           .NotEmpty()
           .GreaterThan(0);

            RuleFor(x => x.ImageFile)
                .NotNull()
                .Must(BeAValidFile).WithMessage("Invalid file format.")
                .Must(file => file.Length <= 4 * 1024 * 1024) // Limit file size to 4MB
                .WithMessage("File size must be less than or equal to 2MB.");
        }
        private bool BeAValidFile(IFormFile file)
        {
            if (file == null) return false;

            // Validate file extension (e.g., allow only .jpg and .png)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}
