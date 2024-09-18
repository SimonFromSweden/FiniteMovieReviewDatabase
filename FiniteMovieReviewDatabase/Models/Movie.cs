namespace FiniteMovieReviewDatabase.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ReleaseYear { get; set; }
        public string? Category { get; set; }
        public string? IMDBLink { get; set; }
        public string? TrailerLink { get; set; }
        public string? ImgUrl { get; set; }

        // Navigation properties
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Dislike>? Dislikes { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }
}
