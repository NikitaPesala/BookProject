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
        public string Id { get; set; }
                
        public string Title { get; set; }

        public Author Author { get; set; }


        [ExistingAuthor]
        public string AuthorId { get; set; }
       
     

        public string Cover { get; set; }

        public string Description { get; set; }

        [Range(0,5000)]
        public int Price { get; set; }

        public string Tags { get; set; }


    }
}
