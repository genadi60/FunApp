using FunApp.Services.DataServices;
using System.ComponentModel.DataAnnotations;

namespace FunApp.Web.Models
{
    public class ValidCategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (ICategoriesService)validationContext
                .GetService(typeof(ICategoriesService));

            if (service.IsCategoryIdValid((int)value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid category Id.");
        }
    }
}
