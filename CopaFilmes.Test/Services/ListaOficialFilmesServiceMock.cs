using CopaFilmes.API.Models;
using CopaFilmes.API.Services;
using CopaFilmes.API.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CopaFilmes.Test.Services
{
    public class ListaOficialFilmesServiceMock : IListaOficialFilmesService
    {
        public Task<IEnumerable<Filme>> Filmes => GetFilmes();

#pragma warning disable CS1998 // método async apenas para manter compatibilidade com a classe real ListaOficialFilmesService
        private async Task<IEnumerable<Filme>> GetFilmes() => new List<Filme>(16)
        {
                new Filme { Id = "tt3606756", Titulo = "Os Incríveis 2", Ano = 2018, Nota = 8.5f },
                new Filme { Id = "tt4881806", Titulo = "Jurassic World: Reino Ameaçado", Ano = 2018, Nota = 6.7f },
                new Filme { Id = "tt5164214", Titulo = "Oito Mulheres e um Segredo", Ano = 2018, Nota = 6.3f },
                new Filme { Id = "tt7784604", Titulo = "Hereditário", Ano = 2018, Nota = 7.8f },
                new Filme { Id = "tt4154756", Titulo = "Vingadores: Guerra Infinita", Ano = 2018, Nota = 8.8f },
                new Filme { Id = "tt5463162", Titulo = "Deadpool 2", Ano = 2018, Nota = 8.1f },
                new Filme { Id = "tt3778644", Titulo = "Han Solo: Uma História Star Wars", Ano = 2018, Nota = 7.2f },
                new Filme { Id = "tt3501632", Titulo = "Thor: Ragnarok", Ano = 2017, Nota = 7.9f },
                new Filme { Id = "tt2854926", Titulo = "Te Peguei!", Ano = 2018, Nota = 7.1f },
                new Filme { Id = "tt0317705", Titulo = "Os Incríveis", Ano = 2004, Nota = 8f },
                new Filme { Id = "tt3799232", Titulo = "A Barraca do Beijo", Ano = 2018, Nota = 6.4f },
                new Filme { Id = "tt1365519", Titulo = "Tomb Raider: A Origem", Ano = 2018, Nota = 6.5f },
                new Filme { Id = "tt1825683", Titulo = "Pantera Negra", Ano = 2018, Nota = 7.5f },
                new Filme { Id = "tt5834262", Titulo = "Hotel Artemis", Ano = 2018, Nota = 6.3f },
                new Filme { Id = "tt7690670", Titulo = "Superfly", Ano = 2018, Nota = 5.1f },
                new Filme { Id = "tt6499752", Titulo = "Upgrade", Ano = 2018, Nota = 7.8f },
            };
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously


        public IEnumerable<CFError> Errors => null;
        public void AddError(string message) { }
        public void AddError(string field, string message) { }
        public bool HasError() { return false; }
    }
}
