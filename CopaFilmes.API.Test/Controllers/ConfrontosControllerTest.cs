using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using CopaFilmes.API.Test.Services;
using Microsoft.Extensions.DependencyInjection;
using CopaFilmes.API.Services;
using CopaFilmes.API.Models;
using System.Linq;
using CopaFilmes.API.Utils;

namespace CopaFilmes.API.Test.Controllers
{
    public class ConfrontosControllerTest
    {
        private const string baseURL = "http://localhost:5000/api/confrontos";
        private readonly string actionFilmes = $"{baseURL}/filmes";
        private readonly string actionGerarCampeonato = $"{baseURL}/gerarcampeonato";

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ConfrontosControllerTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<CopaFilmes.API.Startup>()
                .ConfigureTestServices(services =>
                    {
                        services.AddSingleton<IListaOficialFilmesService, ListaOficialFilmesServiceMock>();
                    })
                );

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task DeveRetornarTodosOsFilmesDoCatalogo()
        {
            var response = await _client.GetAsync(actionFilmes);            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Filme>>(body);

            Assert.Equal(16, result.Count);
        }

        [Fact]
        public async Task DeveRetornarErroSeVerboDiferenteDeGet()
        {
            var response = await _client.PostAsync(actionFilmes, null);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }



        [Fact]
        public async Task GerarCampeonatoDeveClassificarCorretamente8primeiros()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt3606756", // os incriveis
                    "tt4881806", // jurassic world
                    "tt5164214", // oito mulheres
                    "tt7784604", // hereditario
                    "tt4154756", // vingadores
                    "tt5463162", // deadpool
                    "tt3778644", // han solo
                    "tt3501632"  // thor
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vencedores = await response.Content.ReadAsAsync<IEnumerable<Filme>>();

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt4154756", campeao.Id);
            Assert.Equal("Vingadores: Guerra Infinita", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal(8.8f, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt3606756", vice.Id);
            Assert.Equal("Os Incríveis 2", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal(8.5f, vice.Nota);
        }

        [Fact]
        public async Task GerarCampeonatoDeveClassificarCorretamente8ultimos()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt2854926", // te peguei
                    "tt0317705", // incriveis 1
                    "tt3799232", // barraca beijo
                    "tt1365519", // tomb raider
                    "tt1825683", // pantera negra
                    "tt5834262", // hotel artemis
                    "tt7690670", // superfly
                    "tt6499752"  // upgrade
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vencedores = await response.Content.ReadAsAsync<IEnumerable<Filme>>();

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt0317705", campeao.Id);
            Assert.Equal("Os Incríveis", campeao.Titulo);
            Assert.Equal(2004, campeao.Ano);
            Assert.Equal(8f, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt6499752", vice.Id);
            Assert.Equal("Upgrade", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal(7.8f, vice.Nota);
        }

        [Fact]
        public async Task GerarCampeonatoDeveClassificarCorretamentePosicoesImpares()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt3606756", // incriveis 2
                    "tt5164214", // oito mulheres
                    "tt4154756", // vingadores
                    "tt3778644", // han solo
                    "tt2854926", // te peguei
                    "tt3799232", // barraca beijo
                    "tt1825683", // pantera negra
                    "tt7690670"  // superfly
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vencedores = await response.Content.ReadAsAsync<IEnumerable<Filme>>();

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt4154756", campeao.Id);
            Assert.Equal("Vingadores: Guerra Infinita", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal(8.8f, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt3606756", vice.Id);
            Assert.Equal("Os Incríveis 2", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal(8.5f, vice.Nota);
        }

        [Fact]
        public async Task GerarCampeonatoDeveClassificarCorretamentePosicoesPares()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt4881806", // jurassic world
                    "tt7784604", // hereditario
                    "tt5463162", // deadpool
                    "tt3501632", // thor
                    "tt0317705", // incriveis 1
                    "tt1365519", // tomb raider
                    "tt5834262", // hotel artemis
                    "tt6499752"  // upgrade
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vencedores = await response.Content.ReadAsAsync<IEnumerable<Filme>>();

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt5463162", campeao.Id);
            Assert.Equal("Deadpool 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal(8.1f, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt0317705", vice.Id);
            Assert.Equal("Os Incríveis", vice.Titulo);
            Assert.Equal(2004, vice.Ano);
            Assert.Equal(8f, vice.Nota);
        }

        [Fact]
        public async Task GerarCampeonatoDeveClassificarCorretamenteComEmpateNotaQuartasFinal()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt5164214", // oito mulheres ==> 6.3 na posição 5
                    "tt7784604", // hereditario
                    "tt5463162", // deadpool
                    "tt3778644", // han solo
                    "tt3501632", // thor
                    "tt1825683", // pantera negra
                    "tt5834262", // hotel artemis ==> 6.3 na posição 4
                    "tt6499752"  // upgrade
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vencedores = await response.Content.ReadAsAsync<IEnumerable<Filme>>();

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt5463162", campeao.Id);
            Assert.Equal("Deadpool 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal(8.1f, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt7784604", vice.Id);
            Assert.Equal("Hereditário", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal(7.8f, vice.Nota);
        }

        [Fact]
        public async Task GerarCampeonatoDeveClassificarCorretamenteComEmpateNotaSemiFinal()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt3606756", // incriveis 2
                    "tt4881806", // jurassic world
                    "tt7784604", // hereditario ==> 7.8 posicao 2 vai passar para semi no jogo 1
                    "tt0317705", // incriveis 1
                    "tt3799232", // barraca beijo
                    "tt1365519", // tomb raider
                    "tt7690670", // superfly
                    "tt6499752"  // upgrade == 7.8 posicao 8 vai passar para semi no jogo 1
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vencedores = await response.Content.ReadAsAsync<IEnumerable<Filme>>();

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt3606756", campeao.Id);
            Assert.Equal("Os Incríveis 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal(8.5f, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt7784604", vice.Id);
            Assert.Equal("Hereditário", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal(7.8f, vice.Nota);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        public async Task GerarCampeonatoDeveRetornarErroSeHouverMenosOuMaisDe8Filmes(int qtde)
        {
            var lista16Filmes = new List<string>(16)
                {
                    "tt3606756", // incriveis 2
                    "tt4154756", // vingadores
                    "tt4881806", // jurassic world
                    "tt7784604", // hereditario
                    "tt0317705", // incriveis 1
                    "tt3799232", // barraca beijo
                    "tt1365519", // tomb raider
                    "tt7690670", // superfly
                    "tt6499752", // upgrade 
                    "tt5463162", // deadpool
                    "tt3778644", // han solo
                    "tt7690670", // superfly
                    "tt3501632", // thor
                    "tt2854926", // te peguei
                    "tt5164214", // oito mulheres
                    "tt1825683", // pantera negra
                };

            var request = new FilmesSelecionadosRequest()
            {
                Selecao = lista16Filmes.Take(qtde)
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            Assert.Contains(Resources.ErroModeloFilmesSelecionados, jsonResult);
        }

        [Fact]
        public async Task GerarCampeonatoDeveRetornarErroSeHouverUmPaisInvalido()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
                {
                    "tt3606756", // incriveis 2
                    "tt4154756", // vingadores
                    "ee4881806", 
                    "tt7784604", // hereditario
                    "tt0317705", // incriveis 1
                    "tt3799232", // barraca beijo
                    "tt1365519", // tomb raider
                    "tt7690670", // superfly
                }
            };

            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            Assert.Contains(Resources.ErroItemNaoExisteFilmesSelecionados, jsonResult);
        }

        [Fact]
        public async Task GerarCampeonatoDeveRetornarErroSeNaoPassarParametro()
        {
            var response = await _client.PostAsync(actionGerarCampeonato, null);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task GerarCampeonatoDeveRetornarErroSePassarParametroVazio()
        {
            var request = new FilmesSelecionadosRequest();
            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            Assert.Contains(Resources.ErroModeloFilmesSelecionados, jsonResult);
        }

        [Fact]
        public async Task GerarCampeonatoDeveRetornarErroSePassarParametroSelecaoVazio()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>()
            };
            
            var response = await _client.PostAsJsonAsync(actionGerarCampeonato, request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            Assert.Contains(Resources.ErroModeloFilmesSelecionados, jsonResult);
        }
    }
}
