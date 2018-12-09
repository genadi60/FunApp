using System;
using System.Diagnostics;
using System.Linq;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Web.Models;
using FunApp.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Joke> _repository;

        public HomeController(IRepository<Joke> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var jokes = _repository.All()
                .OrderBy(j => Guid.NewGuid())
                .Select(j => new IndexJokeViewModel
                {
                    Content = j.Content,
                    CategoryName = j.Category.Name
                })
                .Take(10)
                .ToList();
               
            var viewModel = new IndexViewModel
            {
                IndexJokeViewModels = jokes
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"My application has {_repository.All().Count()} jokes.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
