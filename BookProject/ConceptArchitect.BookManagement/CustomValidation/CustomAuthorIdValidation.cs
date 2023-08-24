using System.ComponentModel.DataAnnotations;

namespace ConceptArchitect.BookManagement
{
    public class CustomAuthorIdValidation: ValidationAttribute
    {
        IAuthorService authorService;

        public override bool IsValid(object value)
        {
            return authorService.GetAuthorById((string)value) != null;
        }
    }
}
