using JokesWebApp.Data;
using JokesWebApp.Models;
using JokesWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            return View(await _context.Joke.ToListAsync());
        }


        // GET:  Jokes/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // POST:  Jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Joke.Where(j => j.JokeQuestion.Contains(SearchPhrase)).ToListAsync());
        }


        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.ID == id);
            if (joke == null)
            {
                return NotFound();
            }

            JokeDetailsViewModel detailsVM = new JokeDetailsViewModel { JokeQuestion = joke.JokeQuestion, JokeAnswer = joke.JokeAnswer, ID = joke.ID };

            return View(detailsVM);
                                                    
        }

        static string jokeID;

        // GET: Jokes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, JokeQuestion, JokeAnswer")] Joke joke)
        {
            jokeID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                joke.UserId = jokeID;
                _context.Add(joke);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            JokeCreateViewModel createVM = new JokeCreateViewModel { JokeQuestion = joke.JokeQuestion, JokeAnswer = joke.JokeAnswer, ID = joke.ID, UserID = joke.UserId};

            return View(createVM);
        }


        // GET: Jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            JokeEditViewModel editVM = new JokeEditViewModel();

            if (jokeID == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var joke = await _context.Joke.FindAsync(id);
                if (joke == null)
                {
                    return NotFound();
                }

                editVM.JokeQuestion = joke.JokeQuestion;
                editVM.JokeAnswer = joke.JokeAnswer;
                editVM.ID = joke.ID;

                return View(editVM);
            }
            else
            {
                return View("JokeError");
            }



        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, JokeQuestion, JokeAnswer")] Joke joke)
        {

            if (id != joke.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joke);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeExists(joke.ID))
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
            
            return View(joke);
        }


        // GET: Jokes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.ID == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // POST: Jokes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var joke = await _context.Joke.FindAsync(id);
            _context.Joke.Remove(joke);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeExists(int id)
        {
            return _context.Joke.Any(e => e.ID == id);
        }
    }
}
