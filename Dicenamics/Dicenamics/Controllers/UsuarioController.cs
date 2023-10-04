using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dicenamics.Data;
using Dicenamics.Models;


namespace Dicenamics.Controllers
{
    [Route("Controllers/UsuarioController")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDatabase _ctx;

        public UsuarioController(AppDatabase context)
        {
            _ctx = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            _ctx.Usuarios.Add(usuario);
            await _ctx.SaveChangesAsync();

            return CreatedAtRoute("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosUsuarios()
        {
            var usuarios = await _ctx.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _ctx.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarUsuarioPorUsername([FromBody] string username)
        {
            var usuario = _ctx.Usuarios.FirstOrDefault(u => u.Username == username);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
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
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
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

        private bool UsuarioExists(int id)
        {
            return _ctx.Usuarios.Any(u => (int)u.UsuarioId == id);
        }
    }
}
