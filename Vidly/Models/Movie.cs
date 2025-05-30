namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleasesDate { get; set; }
        public byte NumberInStock { get; set; }

    }
}