using ConceptArchitect.Utils;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BookProject.ViewModels
{
    public class EditAuthorViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime? DeathDate { get; set; }

        public string Tags { get; set; }

        public string Photo { get; set; }


        [WordCount(10)]
        [StringLength(2000, MinimumLength = 10)]
        public string Biography { get; set; }

        [Email]
        public string? Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
