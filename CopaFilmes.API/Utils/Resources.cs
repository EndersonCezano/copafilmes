using System;

namespace CopaFilmes.API.Utils
{
    public class Resources
    {
        public const string ErroModeloFilmesSelecionados = "É necessário informar exatamente 8 filmes para gerar o campeonato.";
        public const string ErroItemNaoExisteFilmesSelecionados = "Um ou mais filmes não foram identificados.";
        public static string ErroObterListaOficial = $"Não foi possível obter o catálogo oficial de filmes. {Environment.NewLine}{Environment.NewLine}Os seguintes erros foram reportados pelo servidor:{Environment.NewLine}";
    }
}
