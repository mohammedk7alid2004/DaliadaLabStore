using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace WebApplication4
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions.Split(',');
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!_allowedExtensions.Any(ext =>
                    ext.Trim().ToLowerInvariant() == fileExtension))
                {
                    return new ValidationResult(
                        $"Allowed file extensions are: {string.Join(", ", _allowedExtensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}