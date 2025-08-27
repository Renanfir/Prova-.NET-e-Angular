using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back.Domains
{
    [Table ("endereco")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long id { get; set; }

        public string cep { get; set; }

        public string estado { get; set; }

        public string rua { get; set; }

        public string bairro { get; set; }

        public int numero { get; set; }

        public string complemento { get; set; }

        public int idusuario { get; set; }
    }
}
