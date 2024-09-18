using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FiniteMovieReviewDatabase.Models;

namespace FiniteMovieReviewDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FiniteMovieReviewDatabase.Models.Comment> Comments { get; set; } = default!;
        public DbSet<FiniteMovieReviewDatabase.Models.Dislike> Dislikes { get; set; } = default!;
        public DbSet<FiniteMovieReviewDatabase.Models.Like> Likes { get; set; } = default!;
        public DbSet<FiniteMovieReviewDatabase.Models.Movie> Movies { get; set; } = default!;
    }
}
