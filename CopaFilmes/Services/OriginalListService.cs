using CopaFilmes.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CopaFilmes.API.Services
{
    public class OriginalListService : CFBaseService, IOriginalListService
    {
        private const string urlAPI = "http://copadosfilmes.azurewebsites.net/api/filmes";
        private const string acceptAPI = "application/json";
        private const string thisAgent = "CopaFilmes.Cezano/0.1Dev";

        private List<Filme> filmes;
        private readonly IHttpClientFactory _clientFactory;

        public OriginalListService(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }

        public async Task<List<Filme>> CarregarListaOriginal()
        {
            if (filmes?.Count > 0) return filmes;

            var request = new HttpRequestMessage(HttpMethod.Get, urlAPI);
            request.Headers.Add("Accept", acceptAPI);
            request.Headers.Add("User-Agent", thisAgent);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                filmes = await response.Content.ReadAsAsync<List<Filme>>();
            }
            else
            {
                filmes = new List<Filme>(0);
            }

            return filmes;
        }

        public bool FilmeExists(int id)
        {
            return false;
            //return this.filmes.Any(m => m.Id == id);
        }

        public Filme GetFilme(int id)
        {
            return null;
            //return this.filmes.Where(m => m.Id == id).FirstOrDefault();
        }

        public List<Filme> GetFilmes()
        {
            return null;
            //return this.filmes.ToList();
        }
    }
}
