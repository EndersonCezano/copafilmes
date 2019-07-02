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
using System;

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
            services.AddSingleton<IListaOficialFilmesService, ListaOficialFilmesServiceMock>();

            var serviceProvider = services.BuildServiceProvider();
            _confrontosService = serviceProvider.GetRequiredService<IConfrontosService>();
        }

        [Fact]
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamente8primeiros()
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

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
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamente8ultimos()
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

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
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamentePosicoesImpares()
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

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
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamentePosicoesPares()
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

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
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamenteComEmpateNotaQuartasFinal()
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

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
        public async Task DefinirClassificacaoFinalDeveClassificarCorretamenteComEmpateNotaSemiFinal()
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

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
        public async Task DefinirClassificacaoFinalDeveGerarExcecaoSeHouverMenosDe8Filmes(int qtde)
        {
            var lista8Filmes = new List<string>()
                {
                    "tt3606756", // os incriveis
                    "tt4881806", // jurassic world
                    "tt5164214", // oito mulheres
                    "tt7784604", // hereditario
                    "tt4154756", // vingadores
                    "tt5463162", // deadpool
                    "tt3778644", // han solo
                    "tt3501632"  // thor
                };

            var request = new FilmesSelecionadosRequest()
            {
                Selecao = lista8Filmes.Take(qtde)
            };


            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _confrontosService.DefinirClassificacaoFinal(request));
        }

        [Theory]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        public async Task DefinirClassificacaoFinalVaiConsiderar8PrimeirosOrdenadosMesmoQuandoHouverMaisDe8Filmes(int qtde)
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

            var vencedores = await _confrontosService.DefinirClassificacaoFinal(request);

            Assert.Equal(2, vencedores.Count());

            var campeao = vencedores.First();
            Assert.Equal("tt3606756", campeao.Id);
            Assert.Equal("Os Incríveis 2", campeao.Titulo);
            Assert.Equal(2018, campeao.Ano);
            Assert.Equal(8.5f, campeao.Nota);
        }
    }
}
