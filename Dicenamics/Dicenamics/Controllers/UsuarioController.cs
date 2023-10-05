using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dicenamics.Data;
using Dicenamics.Models;


namespace Dicenamics.Controllers
{
    [ApiController]
    [Route("dicenamics/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDatabase _ctx;

        public UsuarioController(AppDatabase context)
        {
            _ctx = context;
        }

        [HttpPost("criarUsuario")]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            try
            {
                _ctx.Usuarios.Add(usuario);
                _ctx.SaveChanges(); 

                return CreatedAtRoute("", new { id = usuario.UsuarioId }, usuario);
            }
                catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro ao criar o usuário." });
            }
        }

        [HttpGet("buscarUsuario")]
        public async Task<IActionResult> BuscarTodosUsuarios()
        {
            var usuarios = await _ctx.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("buscarUsuario/{id}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _ctx.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet("buscarUsuario/{username}")]
        public async Task<IActionResult> BuscarUsuarioPorUsername(string username)
        {       
        var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(u => u.Username == username);

        if (usuario == null)
        {
        return NotFound("Usuario não encontrado.");
        }

        return Ok(usuario);
        }

        [HttpPut("atualizarUsuario/{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, Usuario usuarioAtualizado)
        {
            if (id != (int)usuarioAtualizado.UsuarioId)
            {
                return BadRequest();
            }  

            _ctx.Entry(usuarioAtualizado).State = EntityState.Modified;

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            if (!UsuarioExiste(id))
                {
                    return NotFound(new { mensagem = "Usuário não encontrado." });
                }
                    else
                {
                    return BadRequest(new { mensagem = "Ocorreu um erro ao atualizar o usuário." });
                }
            }

            return NoContent();
        }

        [HttpDelete("excluirUsuario")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var usuario = await _ctx.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _ctx.Usuarios.Remove(usuario);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExiste(int id)
        {
            return _ctx.Usuarios.Any(u => (int)u.UsuarioId == id);
        }
    }
}
