namespace CopaFilmes.API.Utils
{
    public class Error
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public string ToJsonMode()
        {
            return $@" ""{Field}"": [""{Message}""]";
        }
    }

}
