using JokesWebApp.Models;
using JokesWebApp.Repositories;
using JokesWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            var joke = joke_repository.GetAll();
            return View(joke);
        }

        // GET:  Jokes/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // POST:  Jokes/ShowSearchResults
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            return View("Index", joke_repository.ShowSearchFrom(SearchPhrase));
        }


        // GET: Jokes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Joke joke = joke_repository.GetById(id);

            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);

        }


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
        public IActionResult Create([Bind("ID, JokeQuestion, JokeAnswer")] Joke joke)
        {

            if (ModelState.IsValid)
            {
                joke_repository.Insert(joke);
                joke_repository.Save();
                return RedirectToAction(nameof(Index));
            }

            JokeCreateViewModel createVM = new JokeCreateViewModel
            {
                JokeQuestion = joke.JokeQuestion,
                JokeAnswer = joke.JokeAnswer,
                ID = joke.ID,
                UserID = joke.UserId
            };


            return View(joke);
        }


        // GET: Jokes/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {

            Joke joke = joke_repository.GetById(id);

            if (id == null)
            {
                return NotFound();
            }

            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);

        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID, JokeQuestion, JokeAnswer")] Joke joke)
        {
            if (id != joke.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                joke_repository.Update(joke);
                joke_repository.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(joke);
        }


        // GET: Jokes/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Joke joke = joke_repository.GetById(id);

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
        public IActionResult DeleteConfirmed(int id)
        {
            joke_repository.Delete(id);
            joke_repository.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
