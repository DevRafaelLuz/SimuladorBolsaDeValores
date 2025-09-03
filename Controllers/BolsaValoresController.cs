using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using BolsaValores.Models; // Se sua classe Acao estiver nessa pasta


namespace BolsaValores.Controllers
{
    public class BolsaValoresController : Controller
    {
        private readonly HttpClient _httpClient;

        public BolsaValoresController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();

            var hoje = DateTime.Today;
            var dezAnosAtras = hoje.AddYears(-10);

            var url = $"https://api.bcb.gov.br/dados/serie/bcdata.sgs.11/dados?formato=json&dataInicial={dezAnosAtras:dd/MM/yyyy}&dataFinal={hoje:dd/MM/yyyy}";

            var response = await httpClient.GetAsync(url);


            var json = await response.Content.ReadAsStringAsync();

            // Verifica se a resposta contém "error"
            if (json.Contains("\"error\""))
            {
                // Você pode logar, mostrar na tela ou tratar como quiser
                ViewBag.Erro = "Erro ao consultar API: parâmetros inválidos ou fora do intervalo permitido.";
                return View(new List<Acao>());
            }

            // Se não tiver erro, tenta deserializar normalmente
            var acoes = JsonSerializer.Deserialize<List<Acao>>(json);
            return View(acoes);
        }
    }
}