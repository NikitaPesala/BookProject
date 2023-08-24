using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class UniqueBookIdAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;
            
            var bookService =(IBookService) validationContext.GetService(typeof(IBookService));

            if (bookService == null)
                throw new ArgumentException("Author Service Not configured");

            try
            {
                var book = bookService
                                .GetBookById(value as string)
                                .Result;

                return new ValidationResult($"Duplicate Id: {value}. currently associated with {book.Title}");
            }
            catch(InvalidIdException<string> ex)
            {
                //success
            }

            return ValidationResult.Success; // If it is sucessful. No error

        }
    }
}
