using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
	public class Book
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Author_Id { get; set; }
		public string Cover_Photo { get; set; }
	}
}
