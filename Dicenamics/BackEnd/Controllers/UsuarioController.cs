using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.DTOs;
using BackEnd.DTO;


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
                List<DadoComposto> vazioC = new();
                List<DadoSimples> vazioS = new();
                Usuario? usuario = new()
                {
                    Nickname = usuarioDTO.Nickname,
                    Username = usuarioDTO.Username,
                    Senha = usuarioDTO.Senha,
                    DadosSimplesPessoais = vazioS,
                    DadosCompostosPessoais = vazioC
                };

                if(usuario == null)
                {
                    return NotFound();
                }

                if(_ctx.Usuarios.FirstOrDefault(u => u.Username == usuario.Username) != null)
                {
                    return BadRequest("Usuario já existente");
                }
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
                List<Usuario>? usuarios = 
                    _ctx.Usuarios
                        .Include(d => d.DadosSimplesPessoais)
                        .Include(d => d.DadosCompostosPessoais)
                            .ThenInclude(d => d.Fixos)
                                .ThenInclude(d => d.ModificadorFixo)
                        .Include(d => d.DadosCompostosPessoais)
                            .ThenInclude(d => d.Variaveis)
                                .ThenInclude(d => d.ModificadorVariavel)
                                    .ThenInclude(d => d.Dado)
                        .ToList();
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
                Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.UsuarioId == id);
                
                if (usuario == null)
                {
                    return NotFound();
                }
                
                List<DadoSimples> ds = new();
                foreach (var dadoS in usuario.DadosSimplesPessoais)
                {
                    DadoSimples dado = _ctx.DadosSimples.FirstOrDefault(d => d.DadoId == dadoS.DadoId);
                    ds.Add(dado);
                }
                usuario.DadosSimplesPessoais = ds;

                List<DadoComposto> dc = new();
                foreach (var dadoS in usuario.DadosCompostosPessoais)
                {
                    DadoComposto dado = _ctx.DadosCompostos.Include(d => d.Variaveis).ThenInclude(v => v.ModificadorVariavel).ThenInclude(d => d.Dado).Include(d => d.Fixos).ThenInclude(f => f.ModificadorFixo).FirstOrDefault(d => d.DadoId == dadoS.DadoId);
                    dc.Add(dado);
                }
                usuario.DadosCompostosPessoais = dc;          

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
                Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.Username == username);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                List<DadoSimples> ds = new();
                foreach (var dadoS in usuario.DadosSimplesPessoais)
                {
                    DadoSimples dado = _ctx.DadosSimples.FirstOrDefault(d => d.DadoId == dadoS.DadoId);
                    ds.Add(dado);
                }
                usuario.DadosSimplesPessoais = ds;

                List<DadoComposto> dc = new();
                foreach (var dadoS in usuario.DadosCompostosPessoais)
                {
                    DadoComposto dado = _ctx.DadosCompostos.Include(d => d.Variaveis).ThenInclude(v => v.ModificadorVariavel).ThenInclude(d => d.Dado).Include(d => d.Fixos).ThenInclude(f => f.ModificadorFixo).FirstOrDefault(d => d.DadoId == dadoS.DadoId);
                    dc.Add(dado);
                }
                usuario.DadosCompostosPessoais = dc;    

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
                List<DadoSimples> dadosSimples = _ctx.DadosSimples.Where(d => usuarioAtualizadoDTO.DadosCompostosPessoaisIds.Contains(d.DadoId)).ToList();
                List<DadoComposto> dadosCompostos = _ctx.DadosCompostos.Where(d => usuarioAtualizadoDTO.DadosCompostosPessoaisIds.Contains(d.DadoId)).ToList();

                usuario.Username = usuarioAtualizadoDTO.Username;
                usuario.Nickname = usuarioAtualizadoDTO.Nickname;
                usuario.Senha = usuarioAtualizadoDTO.Senha;
                usuario.DadosSimplesPessoais = dadosSimples;
                usuario.DadosCompostosPessoais = dadosCompostos;

                _ctx.Usuarios.Update(usuario);
                _ctx.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("adicionarDado/simples/{id}")]
        public IActionResult AdicionarDadoS([FromRoute] int id)
        {
            try
            {
                Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.UsuarioId == id);

                if (usuario == null)
                {
                    return NotFound();
                }

                // DadoSimples? dadoNovo = new()
                // {
                //     Nome = dadoAdd.Nome,
                //     Faces = dadoAdd.Faces,
                //     Quantidade = dadoAdd.Quantidade
                // };
                DadoSimples? dadoNovo = _ctx.DadosSimples.FirstOrDefault(d => d.DadoId == id);
                _ctx.DadosSimples.Add(dadoNovo);
                usuario.DadosSimplesPessoais.Add(dadoNovo);
                _ctx.Usuarios.Update(usuario);
                _ctx.SaveChanges();
                
                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("adicionarDado/composto/{id}")]
        public IActionResult AdicionarDadoC([FromRoute] int id, [FromBody] DadoCompostoDTO dadoAdd)
        {
            try
            {
                Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.UsuarioId == id);

                if (usuario == null)
                {
                    return NotFound();
                }

                List<DadoCompostoModFixo> fixos = new();
                List<DadoCompostoModVar> variaveis = new();
                List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoAdd.FixosId.Contains(mf.ModificadorFixoId)).ToList();
                List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoAdd.Variaveis.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
                
                foreach (var item in fixo)
                {
                    DadoCompostoModFixo mods = new()
                    {
                        ModificadorFixo = item
                    };
                    fixos.Add(mods);
                }
                foreach (var item in variavel)
                {
                    DadoCompostoModVar mods = new()
                    {
                        ModificadorVariavel = item
                    };
                    variaveis.Add(mods);
                }

                DadoComposto? dadoNovo = new()
                {
                    Nome = dadoAdd.Nome,
                    Faces = dadoAdd.Faces,
                    Quantidade = dadoAdd.Quantidade,
                    Condicao = dadoAdd.Condicao,
                    Fixos = fixos,
                    Variaveis = variaveis
                };

                // DadoComposto? dadoNovo = _ctx.DadosCompostos
                //     .Include(d => d.Fixos)
                //     .ThenInclude(f => f.ModificadorFixo)
                // .Include(d => d.Variaveis)
                //     .ThenInclude(f => f.ModificadorVariavel)
                //         .ThenInclude(d => d.Dado)
                //     .FirstOrDefault(d => d.DadoId == id);

                _ctx.DadosCompostos.Add(dadoNovo);
                usuario.DadosCompostosPessoais.Add(dadoNovo);
                _ctx.Usuarios.Update(usuario);
                _ctx.SaveChanges();
                
                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("buscar/dados/compostos/{id}")]
        public IActionResult BuscarDadosCompostos([FromRoute] int id)
        {
            try
            {
                Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.UsuarioId == id);
                List<int> dadosIds = new();

                foreach (var item in usuario.DadosCompostosPessoais)
                {
                    dadosIds.Add(item.DadoId);
                }

                List<DadoComposto> dadosAchados = 
                    _ctx.DadosCompostos
                        .Include(d => d.Fixos)
                            .ThenInclude(f => f.ModificadorFixo)
                        .Include(d => d.Variaveis)
                            .ThenInclude(f => f.ModificadorVariavel)
                                .ThenInclude(f => f.Dado)
                        .Where(d => dadosIds.Contains(d.DadoId))
                        .ToList();

                // List<DadoComposto> dadosAchados = 
                //     _ctx.DadosCompostos
                //         .Include(d => d.Fixos)
                //             .ThenInclude(f => f.ModificadorFixo)
                //         .Include(d => d.Variaveis)
                //             .ThenInclude(f => f.ModificadorVariavel)
                //                 .ThenInclude(f => f.Dado)
                //         .Where()
                //         .ToList();
                return Ok(dadosAchados);
            }
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
                Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.UsuarioId == id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _ctx.DadosSimples.RemoveRange(usuario.DadosSimplesPessoais);
                _ctx.DadosCompostos.RemoveRange(usuario.DadosCompostosPessoais);
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
    }
}
