using ConceptArchitect.Utils;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace ConceptArchitect.BookManagement
{
    public class Author
    {
        [UniqueAuthorId]
        public string Id { get; set; }


        [Required]
        public string Name { get; set; }


        //optional
        public DateTime? DeathDate { get; set; }

        public string Tags { get; set; }


        //optional
        public string Photo { get; set; }


        [WordCount(10)]
        [Required]
        [StringLength(2000, MinimumLength =10)]
        public string Biography { get; set; }
        
        [Email]
        public string? Email { get; set; }

        public DateTime BirthDate { get; set; }

        //EF will auto populate is based on foreign key
        public List<Book> Books { get; set; } = new List<Book>();

        public override string ToString()
        {
            return $"Author[Id={Id} , Name={Name} ]";
        }



    }
}