using Microsoft.AspNetCore.Mvc;
using BolsaValores.Models;

namespace BolsaValores.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            // Validação simples
            if (usuario.Email == "admin@email.com" && usuario.Senha == "123")
            {
                // Redireciona para a página de ações
                return RedirectToAction("Acao", "BolsaValores");
            }

            ViewBag.Mensagem = "Email ou senha inválidos.";
            return View();
        }
    }
}