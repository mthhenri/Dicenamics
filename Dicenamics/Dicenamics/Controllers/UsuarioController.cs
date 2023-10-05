using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dicenamics.Data;
using Dicenamics.Models;


namespace Dicenamics.Controllers
{
    //https://chat.openai.com/share/e420026c-4fb4-4ed2-9355-97b2dd429214 usado para criar as bases do controller
    [ApiController]
    [Route("dicenamics/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDatabase _ctx;

        public UsuarioController(AppDatabase context)
        {
            _ctx = context;
        }

        [HttpPost("criar")]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _ctx.Usuarios.Add(usuario);
                _ctx.SaveChanges();
                return Created("", usuario);
                //return CreatedAtRoute("", new { id = usuario.UsuarioId }, usuario);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listar")]
        public IActionResult BuscarTodosUsuarios()
        {
            //Faltou o trycatch
            List<Usuario>? usuarios = _ctx.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult BuscarUsuarioPorId([FromRoute] int id)
        {
            //Faltou o trycatch
            //Usuario? usuario = _ctx.Usuarios.Find(id);
            Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet("buscar/{username}")]
        public IActionResult BuscarUsuarioPorUsername([FromRoute] string username)
        {   
            //Faltou o trycatch    
            Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.Username == username);

            if (usuario == null)
            {
                return NotFound("Usuario não encontrado.");
            }

            return Ok(usuario);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarUsuario([FromRoute] int id, [FromBody] Usuario usuarioAtualizado)
        {
            //Faltou o trycatch
            /*
            if (id != usuarioAtualizado.UsuarioId)
            {
                return BadRequest();
            }
            */

            _ctx.Entry(usuarioAtualizado).State = EntityState.Modified;

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            if (!UsuarioExiste(id))
                {
                    return NotFound(new { mensagem = "Usuário não encontrado." });
                } else
                {
                    return BadRequest(new { mensagem = "Ocorreu um erro ao atualizar o usuário." });
                }
            }

            return Ok(usuarioAtualizado);
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirUsuario([FromRoute] int id)
        {
            //Faltou o try catch
            //var usuario = await _ctx.Usuarios.FindAsync(id);
            Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound();
            }

            _ctx.Usuarios.Remove(usuario);
            _ctx.SaveChanges();

            return Ok(usuario);
        }

        private bool UsuarioExiste(int id)
        {
            return _ctx.Usuarios.Any(u => (int)u.UsuarioId == id);
        }
    }
}
