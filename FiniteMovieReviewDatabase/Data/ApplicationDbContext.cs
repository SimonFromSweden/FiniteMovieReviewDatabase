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
        public DbSet<FiniteMovieReviewDatabase.Models.Comment> Comment { get; set; } = default!;
        public DbSet<FiniteMovieReviewDatabase.Models.Dislike> Dislike { get; set; } = default!;
        public DbSet<FiniteMovieReviewDatabase.Models.Like> Like { get; set; } = default!;
        public DbSet<FiniteMovieReviewDatabase.Models.Movie> Movie { get; set; } = default!;
    }
}
