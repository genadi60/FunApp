using System.Diagnostics;
using FunApp.Services.DataServices;
using FunApp.Services.Models.Home;
using FunApp.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FunApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IJokesService _jokesService;

        public HomeController(IJokesService jokesService)
        {
            _jokesService = jokesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                IndexJokeViewModels = _jokesService.GetRandomJokes(10)
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"My application has {_jokesService.GetCount()} jokes.";

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
