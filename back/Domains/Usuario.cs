using back.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteVertrau.Domains
{
    [Table ("usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public long id { get; set; }

        public string nome { get; set; }

        public string sobrenome { get; set; }

        public string email { get; set; }

        public enumGenero genero { get; set; }

        public DateOnly datanascimento { get; set; }

        


    }
}
