using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using CopaFilmes.API.Models;
using CopaFilmes.API.Services;
using CopaFilmes.Test.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Test.Controllers
{
    public class ConfrontosControllerTest
    {
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
            var response = await _client.GetAsync("http://localhost:5000/api/confrontos/filmes");            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Filme>>(body);

            Assert.Equal(16, result.Count);
        }
    }
}
