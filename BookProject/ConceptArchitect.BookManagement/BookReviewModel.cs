using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class BookReviewModel
    {

        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Reviews> Reviews { get; set; }


        /*
        public Book Book { get; set; }
        public Reviews Reviews { get; set; }

        public BookReviewModel(Book book, Reviews reviews)
        {
            Book = book;
            Reviews = reviews;
        }

        public BookReviewModel()
        {
        }
        */
    }
}
