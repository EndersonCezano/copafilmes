using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using CopaFilmes.API.Models;
using Microsoft.Extensions.DependencyInjection;
using CopaFilmes.API.Services;
using System.Linq;

namespace CopaFilmes.Test.Services
{
    public class ConfrontosServiceTest
    {
        private IConfrontosService _confrontosService;

        public ConfrontosServiceTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            services.AddScoped<IConfrontosService, ConfrontosService>();
            services.AddSingleton<IOriginalListService, OriginalListService>();

            var serviceProvider = services.BuildServiceProvider();
            _confrontosService = serviceProvider.GetRequiredService<IConfrontosService>();
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamente8primeiros()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>(8)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt4154756", campeao.Id);
            Assert.Equal("Vingadores: Guerra Infinita", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal((float)8.8, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt3606756", vice.Id);
            Assert.Equal("Os Incríveis 2", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal((float)8.5, vice.Nota);
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamente8ultimos()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>(8)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt0317705", campeao.Id);
            Assert.Equal("Os Incríveis", campeao.Titulo);
            Assert.Equal(2004, campeao.Ano);
            Assert.Equal((float)8, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt6499752", vice.Id);
            Assert.Equal("Upgrade", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal((float)7.8, vice.Nota);
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamentePosicoesImpares()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>(8)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt4154756", campeao.Id);
            Assert.Equal("Vingadores: Guerra Infinita", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal((float)8.8, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt3606756", vice.Id);
            Assert.Equal("Os Incríveis 2", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal((float)8.5, vice.Nota);
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamentePosicoesPares()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>(8)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt5463162", campeao.Id);
            Assert.Equal("Deadpool 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal((float)8.1, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt0317705", vice.Id);
            Assert.Equal("Os Incríveis", vice.Titulo);
            Assert.Equal(2004, vice.Ano);
            Assert.Equal((float)8, vice.Nota);
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamenteComEmpateNotaQuartasFinal()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>(8)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt5463162", campeao.Id);
            Assert.Equal("Deadpool 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal((float)8.1, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt7784604", vice.Id);
            Assert.Equal("Hereditário", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal((float)7.8, vice.Nota);
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamenteComEmpateNotaSemiFinal()
        {
            var request = new FilmesSelecionadosRequest()
            {
                Selecao = new List<string>(8)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt3606756", campeao.Id);
            Assert.Equal("Os Incríveis 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal((float)8.5, campeao.Nota);

            var vice = vencedores.Last();
            Assert.Equal("tt7784604", vice.Id);
            Assert.Equal("Hereditário", vice.Titulo);
            Assert.Equal(2018, vice.Ano);
            Assert.Equal((float)7.8, vice.Nota);
        }
    }
}
