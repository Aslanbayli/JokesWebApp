using AutoMapper;
using JokesWebApp.Models;
using JokesWebApp.Repositories;
using JokesWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JokesWebApp.Controllers
{
    public class JokesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public JokesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Jokes
        public async Task<IActionResult> Index()
        {
            var joke = _unitOfWork.Jokes.GetAll();
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
            return View("Index", await _unitOfWork.Jokes.ShowSearchFrom(SearchPhrase));
        }


        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Joke joke = await _unitOfWork.Jokes.GetById(id);

            if (joke == null)
            {
                return NotFound();
            }

            var detailsVM = _mapper.Map<JokeDetailsViewModel>(joke);

            return View(detailsVM);
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
        public async Task<IActionResult> Create(JokeCreateViewModel createVM)
        {

            if (ModelState.IsValid)
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var joke = new Joke
                {
                    JokeQuestion = createVM.JokeQuestion,
                    JokeAnswer = createVM.JokeAnswer,
                    UserId = id
                };


                _unitOfWork.Jokes.Insert(joke);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }


            return View(createVM);
        }


        // GET: Jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Joke joke = await _unitOfWork.Jokes.GetById(id);

            if (id == null || joke == null)
            {
                return NotFound();
            }

            if (joke.UserId != userId)
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }


            JokeEditViewModel editVM = _mapper.Map<JokeEditViewModel>(joke);

            return View(editVM);

        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JokeEditViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var joke = await _unitOfWork.Jokes.GetById(model.ID);

                if (joke is null)
                {
                    return NotFound();
                }

                joke.JokeQuestion = model.JokeQuestion;
                joke.JokeAnswer = model.JokeAnswer;

                _unitOfWork.Jokes.Update(joke);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: Jokes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Joke joke = await _unitOfWork.Jokes.GetById(id);

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
            _unitOfWork.Jokes.Delete(id);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

    }
}
