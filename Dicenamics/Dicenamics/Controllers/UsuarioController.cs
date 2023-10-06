using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dicenamics.Data;
using Dicenamics.Models;
using Dicenamics.DTOs;


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
        public IActionResult CriarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                Usuario? usuario = new()
                {
                    Nickname = usuarioDTO.Nickname,
                    Username = usuarioDTO.Username,
                    Senha = usuarioDTO.Senha             
                };
                _ctx.Usuarios.Add(usuario);
                _ctx.SaveChanges(); 

                return Created("", usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listar")]
        public IActionResult BuscarTodosUsuarios()
        {
            try
            {
                List<Usuario>? usuarios = _ctx.Usuarios.ToList();
                if(usuarios == null)
                {
                    return NotFound();
                }
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("buscar/i/{id}")]
        public IActionResult BuscarUsuarioPorId([FromRoute] int id)
        {       
            try
            {
                Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

                if (usuario == null)
                {
                    return NotFound();
                }

                return Ok(usuario);
            }       
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("buscar/u/{username}")]
        public IActionResult BuscarUsuarioPorUsername([FromRoute] string username)
        {
            try
            {
                Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.Username == username);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarUsuario([FromRoute] int id, [FromBody] UsuarioDTO usuarioAtualizadoDTO)
        {
            try
            {
                Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
                List<DadoSimples> dadosSimples = _ctx.DadosSimples.Where(d => usuarioAtualizadoDTO.DadosCompostosPessoaisIds.Contains(d.DadoSimplesId)).ToList();
                List<DadoComposto> dadosCompostos = _ctx.DadosCompostos.Where(d => usuarioAtualizadoDTO.DadosCompostosPessoaisIds.Contains(d.DadoCompostoId)).ToList();
                //_ctx.Entry(usuarioAtualizadoDTO).State = EntityState.Modified;

                usuario.Username = usuarioAtualizadoDTO.Username;
                usuario.Nickname = usuarioAtualizadoDTO.Nickname;
                usuario.Senha = usuarioAtualizadoDTO.Senha;
                usuario.DadosSimplesPessoais = dadosSimples;
                usuario.DadosCompostosPessoais = dadosCompostos;

                _ctx.Usuarios.Update(usuario);
                _ctx.SaveChanges();
                return Ok(usuario);
            }/*
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
            }*/
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirUsuario([FromRoute] int id)
        {
            try
            {
                Usuario? usuario = _ctx.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _ctx.Usuarios.Remove(usuario);
                _ctx.SaveChanges();

                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        private bool UsuarioExiste(int id)
        {
            return _ctx.Usuarios.Any(u => (int)u.UsuarioId == id);
        }
    }
}
