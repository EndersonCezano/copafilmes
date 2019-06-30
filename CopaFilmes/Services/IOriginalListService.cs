using System.Collections.Generic;
using System.Threading.Tasks;
using CopaFilmes.API.Models;

namespace CopaFilmes.API.Services
{
    public interface IOriginalListService : ICFBaseService
    {
        bool FilmeExists(int id);
        Task<List<Filme>> CarregarListaOriginal();
        Filme GetFilme(int id);
        List<Filme> GetFilmes();
    }
}