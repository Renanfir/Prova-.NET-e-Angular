using back.Domains;

namespace TesteVertrau.DTOs
{
    public class userRegisterRequestDTO
    {
        public string nome { get; set; }

        public string sobrenome { get; set; }

        public string email { get; set; }

        public enumGenero genero { get; set; }

        public DateOnly datanascimento { get; set; }
    }
}
