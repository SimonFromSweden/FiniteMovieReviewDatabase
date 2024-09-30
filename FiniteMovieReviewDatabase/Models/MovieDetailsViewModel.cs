namespace FiniteMovieReviewDatabase.Models
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment NewComment { get; set; } // For the new comment form
    }
}
