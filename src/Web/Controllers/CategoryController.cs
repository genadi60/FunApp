using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Services.DataServices;
using FunApp.Services.Models.Category;
using FunApp.Services.Models.Joke;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FunApp.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoriesService _categoriesService;

        private readonly IJokesService _jokesService;

        public CategoryController(ICategoriesService categoriesService, IJokesService jokesService)
        {
            _categoriesService = categoriesService;
            _jokesService = jokesService;
        }

        public IActionResult Index(int? page)
        {
            var categories = _categoriesService.GetAllViewModels();
            
            var nextPage = page ?? 1;

            var pagetCategories = categories.ToPagedList(nextPage, 10);

            var model = new AllCategoriesViewModel
            {
                Categories = pagetCategories
            };

            return View(model);
        }

        public IActionResult Details(int id, string name, int? page)
        {
            var jokes = _jokesService.ByCategory<DetailsViewModel>(id);

            var nextPage = page ?? 1;

            var pagedJokes = jokes.ToPagedList(nextPage, 4);

            var model = new JokesByCategoryDetailsViewModel
            {
                Name = name,
                Jokes = pagedJokes
            };

            return View(model);
        }
    }
}
