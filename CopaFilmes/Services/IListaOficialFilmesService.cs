using System.Collections.Generic;
using System.Threading.Tasks;
using CopaFilmes.API.Models;

namespace CopaFilmes.API.Services
{
    public interface IListaOficialFilmesService : IBaseService
    {
        Task<IEnumerable<Filme>> Filmes { get; }
    }
}