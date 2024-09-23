using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiniteMovieReviewDatabase.Data;
using FiniteMovieReviewDatabase.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FiniteMovieReviewDatabase.Controllers
{
    public class DislikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DislikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dislikes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dislikes.Include(d => d.Movie).Include(d => d.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dislikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dislike = await _context.Dislikes
                .Include(d => d.Movie)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dislike == null)
            {
                return NotFound();
            }

            return View(dislike);
        }

		// POST: Likes/AddDislike
		[Authorize]
		[HttpPost]
		public async Task<ActionResult> AddDislike([FromBody] int movieId)
		{
			Console.WriteLine($"Received movieId: {movieId}");

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Find userId

			// Get all the liked movies
			var likedMovies = await _context.Likes
									.Where(c => c.UserId == userId)
									.Include(c => c.Movie)
									.ToListAsync();

			var movieToDislike = likedMovies.FirstOrDefault(c => c.MovieId == movieId); // Find if the movie is already liked or not

			var likeExists = await _context.Likes.FindAsync(movieId); // Find if the movie already has a like or not

			if (movieToDislike != null)
			{
				if (likeExists != null)
				{
					// Remove like from movie
					_context.Likes.Remove(likeExists);
					await _context.SaveChangesAsync();
					return Json(new { success = true, message = "Like removed, dislike again in order to add a dislike!" });
				}
				else
				{
					return Json(new { success = true, message = "You have already disliked this movie!" });
				}
			}
			else
			{
				var newDislike = new Dislike
				{
					UserId = userId,
					MovieId = movieId,
				};

				_context.Dislikes.Add(newDislike);
				await _context.SaveChangesAsync();
				return Json(new { success = true, message = "Dislike added to movie!" });
			}
		}

		// GET: Dislikes/Create
		public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Dislikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,UserId")] Dislike dislike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dislike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id", dislike.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", dislike.UserId);
            return View(dislike);
        }

        // GET: Dislikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dislike = await _context.Dislikes.FindAsync(id);
            if (dislike == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id", dislike.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", dislike.UserId);
            return View(dislike);
        }

        // POST: Dislikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,UserId")] Dislike dislike)
        {
            if (id != dislike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dislike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DislikeExists(dislike.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id", dislike.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", dislike.UserId);
            return View(dislike);
        }

        // GET: Dislikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dislike = await _context.Dislikes
                .Include(d => d.Movie)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dislike == null)
            {
                return NotFound();
            }

            return View(dislike);
        }

        // POST: Dislikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dislike = await _context.Dislikes.FindAsync(id);
            if (dislike != null)
            {
                _context.Dislikes.Remove(dislike);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DislikeExists(int id)
        {
            return _context.Dislikes.Any(e => e.Id == id);
        }
    }
}
