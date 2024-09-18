using Microsoft.AspNetCore.Identity;

namespace FiniteMovieReviewDatabase.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CommentDate { get; set; }

        // Foreign keys
        public int MovieId { get; set; }
        public string? UserId { get; set; }

        // Navigation properties
        public Movie? Movie { get; set; }
        public IdentityUser? User { get; set; }
    }
}
