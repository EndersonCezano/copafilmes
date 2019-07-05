using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using CopaFilmes.API.Models;
using Microsoft.Extensions.DependencyInjection;
using CopaFilmes.API.Services;
using System.Linq;
using System;

namespace CopaFilmes.API.Test.Services
{
    public class ListaOficialFilmesServiceTest
    {
        private IListaOficialFilmesService _listaOficialFilmesService;

        public ListaOficialFilmesServiceTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            services.AddScoped<IConfrontosService, ConfrontosService>();
            services.AddSingleton<IListaOficialFilmesService, ListaOficialFilmesService>();

            var serviceProvider = services.BuildServiceProvider();
            _listaOficialFilmesService = serviceProvider.GetRequiredService<IListaOficialFilmesService>();
        }

        [Fact]
        public async Task FilmesDeveRealizarIntegracaoComAPIExternaCorretamente()
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

            var filmes = await _listaOficialFilmesService.Filmes;
            Assert.NotEmpty(filmes);
        }

    }
}
