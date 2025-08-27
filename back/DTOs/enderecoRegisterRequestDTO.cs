namespace back.DTOs
{
    public class enderecoRegisterRequestDTO
    {

        public string cep { get; set; }

        public string estado { get; set; }

        public string rua { get; set; }

        public string bairro { get; set; }

        public int numero { get; set; }

        public string complemento { get; set; }

        public int idusuario { get; set; }
    }
}
