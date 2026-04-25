using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Data;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace JokesWebApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jokes
        public async Task<IActionResult> Index()
        {
            var jokes = await _context.Joke.ToListAsync() ?? new List<Joke>();
            return View(jokes);
        }

        // GET: Search Form
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // GET: Search Results (FIXED)
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            // Prevent empty/null search
            if (string.IsNullOrWhiteSpace(SearchPhrase))
            {
                return View("Index", new List<Joke>());
            }

            // Safe query (prevents null JokeQuestion crash)
            var result = await _context.Joke
                .Where(j => !string.IsNullOrEmpty(j.JokeQuestion) &&
                            j.JokeQuestion.Contains(SearchPhrase))
                .ToListAsync();

            return View("Index", result);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);

            if (joke == null) return NotFound();

            return View(joke);
        }

        // GET: Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new Joke());
        }

        // POST: Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
        {
            if (!ModelState.IsValid)
            {
                return View(joke);
            }

            _context.Add(joke);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Edit
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var joke = await _context.Joke.FindAsync(id);
            if (joke == null) return NotFound();

            return View(joke);
        }

        // POST: Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
        {
            if (id != joke.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(joke);
            }

            try
            {
                _context.Update(joke);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Joke.Any(e => e.Id == joke.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);

            if (joke == null) return NotFound();

            return View(joke);
        }

        // POST: Delete
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var joke = await _context.Joke.FindAsync(id);

            if (joke != null)
            {
                _context.Joke.Remove(joke);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}