using JokesWebApp.Models;
using JokesWebApp.Repositories;
using JokesWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JokesWebApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly IJokeRepository joke_repository;

        public JokesController(IJokeRepository repository)
        {
            this.joke_repository = repository;
        }


        // GET: Jokes
        public async Task<IActionResult> Index()
        {
            var joke = joke_repository.GetAll();
            return View(await joke);
        }

        // GET:  Jokes/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // POST:  Jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await joke_repository.ShowSearchFrom(SearchPhrase));
        }


        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Joke joke = await joke_repository.GetById(id);

            if (joke == null)
            {
                return NotFound();
            }

            JokeDetailsViewModel detailsVM = new JokeDetailsViewModel()
            {
                JokeQuestion = joke.JokeQuestion,
                JokeAnswer = joke.JokeAnswer,
                ID = joke.ID
            };

            return View(detailsVM);
        }


        // GET: Jokes/Create
        [Authorize]
        public IActionResult Create()
        {
            JokeCreateViewModel createVM = new JokeCreateViewModel();
            return View(createVM);
        }

        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, JokeQuestion, JokeAnswer")] Joke joke)
        {

            if (ModelState.IsValid)
            {
                joke_repository.Insert(joke);
                await joke_repository.Save();
                return RedirectToAction(nameof(Index));
            }

            JokeCreateViewModel createVM = new JokeCreateViewModel()
            {
                JokeQuestion = joke.JokeQuestion,
                JokeAnswer = joke.JokeAnswer,
                ID = joke.ID,
                UserID = joke.UserId
            };


            return View(createVM);
        }


        // GET: Jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {

            Joke joke = await joke_repository.GetById(id);

            if (id == null)
            {
                return NotFound();
            }

            if (joke == null)
            {
                return NotFound();
            }

            JokeEditViewModel editVM = new JokeEditViewModel()
            {
                JokeQuestion = joke.JokeQuestion,
                JokeAnswer = joke.JokeAnswer,
                ID = joke.ID,
                UserID = joke.UserId
            };

            return View(editVM);

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
                    joke_repository.Update(joke);
                    await joke_repository.Save();
                }
                catch
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

            Joke joke = await joke_repository.GetById(id);

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
            joke_repository.Delete(id);
            await joke_repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeExists(int id)
        {
            return joke_repository.GetById(id) != null;
        }

    }
}
