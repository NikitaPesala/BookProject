using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class ExistingAuthorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var id = value as string;

            if (string.IsNullOrEmpty(id))
                return ValidationResult.Success;

            var authorService = (IAuthorService)validationContext.GetService(typeof(IAuthorService));
            if (authorService == null)
                throw new ArgumentException("Author Service is NOT configured");

            var author = authorService.GetAuthorById(id).Result;

            if (author == null)
                return new ValidationResult($"Invalid Author Id :'{id}'");
            else
                return ValidationResult.Success;


        }
    }
}
