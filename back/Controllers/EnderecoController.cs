using back.Data;
using back.Domains;
using Microsoft.AspNetCore.Mvc;
using back.DTOs;
using Microsoft.EntityFrameworkCore;
using TesteVertrau.Domains;

namespace back.Controllers
{
    [ApiController]
    [Route("endereco")]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EnderecoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost(Name = "endereco")]
        public async Task<IActionResult> CriaEndereco([FromBody] enderecoRegisterRequestDTO enderecoDTO)
        {
            if (!await dbContext.Usuarios.AnyAsync(u => u.id == enderecoDTO.idusuario)) 
            {
                return BadRequest("O id do usuario não existe");
            }
            
            var endereco = new Endereco
            {
                rua = enderecoDTO.rua,
                numero = enderecoDTO.numero,
                complemento = enderecoDTO.complemento,
                bairro = enderecoDTO.bairro,
                estado = enderecoDTO.estado,
                cep = enderecoDTO.cep,
                idusuario = enderecoDTO.idusuario
            };

            try
            {
                dbContext.Enderecos.Add(endereco);
                await dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(CriaEndereco), new { id = endereco.id }, endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
