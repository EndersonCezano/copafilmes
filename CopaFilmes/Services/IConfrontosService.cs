using CopaFilmes.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.API.Services
{
    public interface IConfrontosService: IBaseService
    {
        Task<IEnumerable<Filme>> DefinirClassificacaoFinal(FilmesSelecionadosRequest request);
    }
}