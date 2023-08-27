using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConceptArchitect.BookManagement
{
    [Table("Members")]
    public class User
    {
        [Email]
        [Key]
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [Column("PhotoUrl")]
        public string Photo { get; set; }
    }
}
