using CopaFilmes.API.Models;
using CopaFilmes.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.API.Services
{
    public class ConfrontosService : CFBaseService, IConfrontosService
    {
        private readonly IOriginalListService _originalListService;
        public ConfrontosService(IOriginalListService originalListService)
        {
            _originalListService = originalListService;
        }

        public async Task<IEnumerable<Filme>> DefinirClassificacaoFinal(FilmesSelecionadosRequest request)
        {
            var listaOriginal = await _originalListService.CarregarListaOriginal();

            var oitoFilmes = listaOriginal.Where(f => request.Selecao.Contains(f.Id)).OrderBy(o => o.Titulo);

            var quatroFilmes = DefinirVencedoresQuartasDeFinal(oitoFilmes);
            var doisFilmesFinalistas = DefinirVencedoresSemiFinais(quatroFilmes);

            return OrdenacaoNotasTitulos(doisFilmesFinalistas);
        }

        private IEnumerable<Filme> DefinirVencedoresQuartasDeFinal(IEnumerable<Filme> listaOitoFilmes)
        {
            var vencedores = new List<Filme>(4);
            for (int i = 0; i < 4; i++)
            {
                var filmeA = listaOitoFilmes.ElementAt(i);
                var filmeB = listaOitoFilmes.ElementAt(7 - i);

                vencedores.Add(DefineMelhorFilme(filmeA, filmeB));
            }

            return vencedores;
        }

        private IEnumerable<Filme> DefinirVencedoresSemiFinais(IEnumerable<Filme> listaQuatroFilmes)
        {
            var vencedores = new List<Filme>(2);
            for (int i = 0; i < 4; i += 2)
            {
                var filmeA = listaQuatroFilmes.ElementAt(i);
                var filmeB = listaQuatroFilmes.ElementAt(i + 1);

                vencedores.Add(DefineMelhorFilme(filmeA, filmeB));
            }

            return vencedores;
        }

        private Filme DefineMelhorFilme(Filme filmeA, Filme filmeB)
        {
            var filmes = new List<Filme>() { filmeA, filmeB };
            return OrdenacaoNotasTitulos(filmes).First();
        }

        private IEnumerable<Filme> OrdenacaoNotasTitulos(IEnumerable<Filme> doisFilmes)
        {
            return doisFilmes.OrderByDescending(f => f.Nota).ThenBy(f => f.Titulo);
        }
    }
}
