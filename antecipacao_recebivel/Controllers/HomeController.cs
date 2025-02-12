using antecipacao_recebivel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace antecipacao_recebivel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //TempData["Mensagem"] = "Olá, essa é uma mensagem temporária!";

           //return Redirect("https://localhost:7005/swagger");
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
