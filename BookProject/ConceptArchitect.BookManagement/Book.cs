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

		[Required]
		public string Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; }

		[Required]
		public string Author_Id { get; set; }
		public string Cover_Photo { get; set; }
	}
}
