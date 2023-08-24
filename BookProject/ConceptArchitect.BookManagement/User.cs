using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    [Table("Members")]
    public class User
    {
        [Email]
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [Column("PhotoUrl")]
        public string Photo { get; set; }
    }
}
