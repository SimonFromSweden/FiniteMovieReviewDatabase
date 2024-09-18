using Microsoft.AspNetCore.Identity;

namespace FiniteMovieReviewDatabase.Models
{
    public class Like
    {
        public int Id { get; set; }

        // Foreign keys
        public int MovieId { get; set; }
        public string? UserId { get; set; }  // using IdentityUser

        // Navigation properties
        public Movie? Movie { get; set; }
        public IdentityUser? User { get; set; }
    }
}
