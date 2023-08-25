using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class Reviews
    {

      
        public string Id { get; set; }


        [Required]

        public User User { get; set; }
        public Book Book { get; set; }
       
        //optional
        public int Rating { get; set; }

   

        //optional
        public string? Details { get; set; }

    }
}
