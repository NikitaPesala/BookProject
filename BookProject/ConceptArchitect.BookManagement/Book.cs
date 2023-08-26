using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class Book
    {
        [UniqueBookId]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Author Author { get; set; }


        [ExistingAuthor]
        public string AuthorId { get; set; }
       

        public string Cover { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0,5000)]
        public int Price { get; set; }

        [Required]
        public string Tags { get; set; }


    }
}
