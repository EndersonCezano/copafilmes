using CopaFilmes.API.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CopaFilmes.API.Models
{
    public class FilmesSelecionadosRequest
    {
        [Required(ErrorMessage = CFResources.ErroModeloFilmesSelecionados)]
        public IEnumerable<string> Selecao { get; set; }
    }
}
