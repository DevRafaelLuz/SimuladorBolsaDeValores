using Microsoft.AspNetCore.Mvc;
using BolsaValores.Models;
using BolsaValores.Data;
using System.Linq;

namespace BolsaValores.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            // Busca no banco um usuário com email e senha correspondentes
            var usuarioValido = _context.Usuarios
                .FirstOrDefault(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

            if (usuarioValido != null)
            {
                // Login bem-sucedido
                return RedirectToAction("Acao", "BolsaValores");
            }

            ViewBag.Mensagem = "Email ou senha inválidos.";
            return View();
        }
    }
}