using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class Book
    {
        public string Id { get; set; }
                
        public string Title { get; set; }

        public Author Author { get; set; }

        [ExistingAuthor]
        public string AuthorId { get; set; }

        public string Cover { get; set; }

        [StringLength(2000,MinimumLength =50)]
        public string Description { get; set; }

        [Range(0,5000)]
        public int Price { get; set; }

        public string Tags { get; set; }


    }
}
