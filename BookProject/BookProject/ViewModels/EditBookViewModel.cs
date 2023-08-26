using ConceptArchitect.BookManagement;
using ConceptArchitect.Utils;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookProject.ViewModels
{
    public class EditBookViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [ExistingAuthor]
        public string AuthorId { get; set; }

        public string Cover { get; set; }

        public string Description { get; set; }

        [Range(0, 5000)]
        public int Price { get; set; }

        public string Tags { get; set; }
    }
}
