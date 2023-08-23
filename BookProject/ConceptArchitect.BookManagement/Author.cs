using System.ComponentModel.DataAnnotations;

namespace ConceptArchitect.BookManagement
{
    public class Author
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Biography { get; set; }
        public string Photo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"Author[Id={Id} , Name={Name} ]";
        }

    }
}