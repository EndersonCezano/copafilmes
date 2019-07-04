namespace CopaFilmes.API.Models
{
    public class Filme
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public ushort Ano { get; set; }
        public float Nota { get; set; }
    }
}
