using CopaFilmes.API.Models;
using CopaFilmes.API.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CopaFilmes.API.Services
{
    public class ListaOficialFilmesService : BaseService, IListaOficialFilmesService
    {
        private const string urlAPI = "http://copadosfilmes.azurewebsites.net/api/filmes";
        private const string acceptAPI = "application/json";
        private const string thisAgent = "CopaFilmes.Cezano/0.3Dev";
        private readonly IHttpClientFactory _clientFactory;

        private IEnumerable<Filme> _filmes;
        /// <summary>
        /// O interessante aqui é que essa classe foi setada no startup.cs como um serviço
        /// singleton, que faz com que o objeto 'filmes' seja carregado apenas uma vez por 
        /// instância da api
        /// </summary>
        public Task<IEnumerable<Filme>> Filmes => GetFilmes();

        public ListaOficialFilmesService(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }

        private async Task<IEnumerable<Filme>> CarregarLista()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, urlAPI);
            request.Headers.Add("Accept", acceptAPI);
            request.Headers.Add("User-Agent", thisAgent);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsAsync<string>();
                AddError("Catalogo", string.Concat(Resources.ErroObterListaOficial, err));
                return null;
            }

            return await response.Content.ReadAsAsync<IEnumerable<Filme>>();
        }
        
        private async Task<IEnumerable<Filme>> GetFilmes()
        {
            if ((_filmes?.Count() ?? 0) == 0)
            {
                _filmes = await CarregarLista();
            }

            return _filmes;
        }
    }
}
