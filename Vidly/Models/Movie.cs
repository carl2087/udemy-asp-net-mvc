using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleasesDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }

    }
}