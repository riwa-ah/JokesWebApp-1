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

        public JokesController(ApplicationDbContext context) //constructor
        {
            _context = context;
        }

        // GET: Jokes
        public async Task<IActionResult> Index()
        {
            var jokes = await _context.Joke.ToListAsync() ?? new List<Joke>(); //gets all jokes from db
            return View(jokes); 
        }

        // GET: Search Form
        public IActionResult ShowSearchForm()
        {
            return View(); //only displlayes the page
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
                            j.JokeQuestion.Contains(SearchPhrase)) //checks if there is any questin that have the inputed phrase
                .ToListAsync();

            return View("Index", result);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id) //search the joke based on the id
        {
            if (id == null) return NotFound();

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);

            if (joke == null) return NotFound();

            return View(joke);
        }

        // GET: Create
        [Authorize] //it needs to login first before creating (Areas-identity)
        public IActionResult Create()
        {
            return View(new Joke());
        }

        // POST: Create
        [Authorize]
        [HttpPost] //recieve form data
        [ValidateAntiForgeryToken] //save it to db
        public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
        {
            if (!ModelState.IsValid)
            {
                return View(joke);
            }

            _context.Add(joke);
            await _context.SaveChangesAsync(); //save it to db
            return RedirectToAction(nameof(Index));
        }

        // GET: Edit
        [Authorize]
        public async Task<IActionResult> Edit(int? id) //based on joke id
        {
            if (id == null) return NotFound();

            var joke = await _context.Joke.FindAsync(id); 
            if (joke == null) return NotFound();

            return View(joke); //loads the edit page
        }

        // POST: Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
        { //gets the input
            if (id != joke.Id) return NotFound();

            if (!ModelState.IsValid) 
            {
                return View(joke);
            }

            try
            {
                _context.Update(joke);
                await _context.SaveChangesAsync(); //update changes
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