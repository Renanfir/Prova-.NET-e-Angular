using back.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteVertrau.Domains;
using TesteVertrau.DTOs;

namespace TesteVertrau.Controller
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public UsuarioController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "usuario")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await dbContext.Usuarios.ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(usuarios);
        }


        [HttpGet("{id}", Name = "UsuarioId")]
        public async Task<IActionResult> GetUsuarioId(long id)
        {
            var usuario = await dbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
            return Ok(usuario);
        }


        [HttpPost(Name = "usuario")]
        public async Task<IActionResult> CriaUsuario([FromBody] userRegisterRequestDTO usuarioDTO)
        {
            if (usuarioDTO.datanascimento > DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("A data de nascimento não pode ser no futuro.");
            }

            if(await dbContext.Usuarios.AnyAsync(u => u.email == usuarioDTO.email))
            {
                return BadRequest("O email já está em uso.");
            }

            var usuario = new Usuario
            {

                nome = usuarioDTO.nome,
                sobrenome = usuarioDTO.sobrenome,
                email = usuarioDTO.email,
                genero = usuarioDTO.genero,
                datanascimento = usuarioDTO.datanascimento
            };

            //try 
            //{
                dbContext.Usuarios.Add(usuario);
                await dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(CriaUsuario), new { id = usuario.id }, usuario);
            //}
            //catch (Exception ex) 
            //{ 
            //    return BadRequest($"Erro ao criar o usuário: {ex.Message}");
            //}


        }

        [HttpDelete("{id}", Name = "Deletausuario")]
        public async Task<ActionResult<Usuario>> DeletarUsuario(long id)
        {
            var usuario = await dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return BadRequest($"Usuário com ID {id} não encontrado.");
            }

            try 
            {
                dbContext.Usuarios.Remove(usuario);
                await dbContext.SaveChangesAsync();

                return Ok(usuario);
            } 
            
            catch 
            { 
                return BadRequest("Erro ao deletar o usuário.");
            }


        }


        [HttpPut("{id}", Name = "AtualizaUsuario")]
        public async Task<IActionResult> AtualizaUsuario(long id, [FromBody] userRegisterRequestDTO usuarioDTO)
        {

            var usuario = await dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            usuario.nome = usuarioDTO.nome;
            usuario.sobrenome = usuarioDTO.sobrenome;
            usuario.email = usuarioDTO.email;
            usuario.genero = usuarioDTO.genero;
            usuario.datanascimento = usuarioDTO.datanascimento;

            try
            {
                dbContext.Usuarios.Update(usuario);
                await dbContext.SaveChangesAsync();
                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex) {
                return BadRequest($"Erro ao atualizar o usuário: {ex.Message}");
            }
        }


    }
}
