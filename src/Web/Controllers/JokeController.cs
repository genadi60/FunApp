using System;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Services.DataServices;
using FunApp.Services.MachineLearning;
using FunApp.Services.Models.Joke;
using FunApp.Web.Models.Joke;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FunApp.Web.Controllers
{
    public class JokeController : BaseController
    {
        private readonly IJokesService _jokesService;
        private readonly ICategoriesService _categoriesService;
        private readonly IJokesCategorizer _jokesCategorizer;

        public JokeController(IJokesService jokesService, ICategoriesService categoriesService, IJokesCategorizer jokesCategorizer)
        {
            _jokesService = jokesService;
            _categoriesService = categoriesService;
            _jokesCategorizer = jokesCategorizer;
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["Categories"] = _categoriesService.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NameAndCount
                });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJokeInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var id = await _jokesService.Create(input.CategoryId, input.Content);

            return RedirectToAction("Details", new {id = id});
        }

        public IActionResult Details(int id)
        {
            var detailsViewModel = _jokesService.ById<DetailsViewModel>(id);

            return View(detailsViewModel);
        }

        public IActionResult Edit(int id)
        {
            var jokeViewModel = _jokesService.ById<JokeViewModel>(id);
            ViewData["Categories"] = _categoriesService.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NameAndCount
                });

            return View(jokeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JokeViewModel model)
        {

            var id = await _jokesService.Edit(model);

            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _jokesService.Delete(id);
            if (!deleted)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public SuggestCategoryResult SuggestCategory(string joke)
        {
            var category = _jokesCategorizer.Categorize("MlModels/JokesCategoryModel.zip", joke);
            var categoryId = _categoriesService.GetCategoryId(category);
            return new SuggestCategoryResult{CategoryId = categoryId ?? 0, CategoryName = category};
        }
    }
}
