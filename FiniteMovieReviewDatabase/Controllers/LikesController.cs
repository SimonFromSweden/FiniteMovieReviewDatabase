﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiniteMovieReviewDatabase.Data;
using FiniteMovieReviewDatabase.Models;

namespace FiniteMovieReviewDatabase.Controllers
{
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Likes.Include(l => l.Movie).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Movie)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // GET: Likes/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Likes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,UserId")] Like like)
        {
            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id", like.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", like.UserId);
            return View(like);
        }

        // GET: Likes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id", like.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", like.UserId);
            return View(like);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,UserId")] Like like)
        {
            if (id != like.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(like);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(like.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Set<Movie>(), "Id", "Id", like.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", like.UserId);
            return View(like);
        }

        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Movie)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            if (like != null)
            {
                _context.Likes.Remove(like);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
