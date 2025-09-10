using Microsoft.AspNetCore.Mvc;
using BolsaValores.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Globalization;

namespace BolsaValores.Controllers
{
    public class BolsaValoresController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string ApiKey = "MAMXSKLST8SG27HL";
        private static readonly string[] symbols = { "PETR4.SA", "VALE3.SA", "ITUB4.SA" };
        private const string CacheKey = "acoes_cache";

        public BolsaValoresController(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cache = memoryCache;
        }

        public async Task<IActionResult> Acao()
        {
            List<Acao>? acoes;

            // Tenta obter do cache
            if (!_cache.TryGetValue(CacheKey, out acoes) || acoes == null || acoes.Count == 0)
            {
                acoes = new List<Acao>();
                bool erroApi = false;
                bool limiteAtingido = false;

                foreach (var symbol in symbols)
                {
                    var url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={ApiKey}";
                    try
                    {
                        var response = await _httpClient.GetAsync(url);
                        var json = await response.Content.ReadAsStringAsync();

                        // LOG para debug: salva a resposta da API
                        System.IO.File.AppendAllText("log_api.txt", $"[{symbol}] {json}\n\n");

                        // Verifica se a resposta indica limite diário atingido
                        if (json.Contains("\"Information\"") || json.Contains("rate limit"))
                        {
                            limiteAtingido = true;
                            break;
                        }

                        using var doc = JsonDocument.Parse(json);
                        if (doc.RootElement.TryGetProperty("Global Quote", out var quote) && quote.TryGetProperty("01. symbol", out var nomeProp))
                        {
                            var nome = nomeProp.GetString();
                            var precoStr = quote.TryGetProperty("05. price", out var precoProp) ? precoProp.GetString() : "0";
                            if (!decimal.TryParse(precoStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var preco))
                            {
                                preco = 0;
                            }
                            var variacao = quote.TryGetProperty("10. change percent", out var varElem) ? varElem.GetString() : "N/A";
                            acoes.Add(new Acao { Nome = nome, Preco = preco, Variacao = variacao });
                        }
                        else
                        {
                            erroApi = true;
                        }
                    }
                    catch
                    {
                        erroApi = true;
                    }
                }

                // Salva no cache por 2 minutos
                if (acoes.Count > 0)
                    _cache.Set(CacheKey, acoes, TimeSpan.FromMinutes(2));

                if (limiteAtingido)
                {
                    ViewBag.Erro = "Limite diário da API Alpha Vantage atingido. Exibindo os últimos dados disponíveis ou dados fictícios para simulação.";
                    // Tenta mostrar os dados do cache anterior (se houver)
                    acoes = _cache.Get(CacheKey) as List<Acao>;
                }
                else if (erroApi || acoes.Count == 0)
                {
                    ViewBag.Erro = "Problema ao buscar dados na API. Exibindo dados de exemplo.";
                    // Dados de exemplo para apresentação
                    acoes = new List<Acao>
                    {
                        new Acao { Nome = "PETR4.SA", Preco = 28.50M, Variacao = "+1.25%" },
                        new Acao { Nome = "VALE3.SA", Preco = 65.40M, Variacao = "-0.85%" },
                        new Acao { Nome = "ITUB4.SA", Preco = 30.10M, Variacao = "+0.55%" }
                    };
                }
            }

            // Se não houver dados de cache, retorna mock
            if (acoes == null || acoes.Count == 0)
            {
                acoes = new List<Acao>
                {
                    new Acao { Nome = "PETR4.SA", Preco = 28.50M, Variacao = "+1.25%" },
                    new Acao { Nome = "VALE3.SA", Preco = 65.40M, Variacao = "-0.85%" },
                    new Acao { Nome = "ITUB4.SA", Preco = 30.10M, Variacao = "+0.55%" }
                };
            }

            return View(acoes);
        }
    }
}