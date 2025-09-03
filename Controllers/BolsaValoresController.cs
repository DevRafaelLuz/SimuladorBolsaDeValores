using Microsoft.AspNetCore.Mvc;

namespace BolsaValores.Controllers
{
    public class BolsaValoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}