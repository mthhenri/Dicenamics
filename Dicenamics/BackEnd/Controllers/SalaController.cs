using System.Linq;
using BackEnd.Data;
using BackEnd.DTO;
using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/sala")]
public class SalaController : ControllerBase
{

    private readonly AppDatabase _ctx;

    public SalaController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    // Create 
    [HttpPost("criar")]
    public IActionResult CriarSala([FromBody] SalaDTO salaDTO)
    {
        try
        {
            Usuario? UsuarioEncontrado = _ctx.Usuarios.Include(u => u.DadosCompostosPessoais).Include(u => u.DadosSimplesPessoais).FirstOrDefault(x => x.UsuarioId == salaDTO.UsuarioMestreId);
            List<Usuario> convidado = _ctx.Usuarios.Where(s => salaDTO.ConvidadosId.Contains(s.UsuarioId)).Include(u => u.DadosCompostosPessoais).Include(u => u.DadosSimplesPessoais).ToList();
            List<SalaUsuario> convidados = new();
            foreach (var item in convidado)
            {
                SalaUsuario salaU = new()
                {
                    Usuario = item
                };
                convidados.Add(salaU);
            }
            Sala sala = new()
            {
                Nome = salaDTO.Nome,
                Descricao = salaDTO.Descricao,
                UsuarioMestreId = salaDTO.UsuarioMestreId,
                UsuarioMestre = UsuarioEncontrado,
                Convidados = convidados
            };
            _ctx.Usuarios.UpdateRange(sala.UsuarioMestre);
            _ctx.Usuarios.UpdateRange(convidado);
            _ctx.Salas.Add(sala);
            _ctx.SaveChanges();
            return Created("", sala);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Read
    [HttpGet("buscar/id/{id}")]
    public IActionResult ObterSalaPorId([FromRoute] int id)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas
                                        .Include(s => s.DadosCompostosSala)
                                        .Include(s => s.DadosSimplesSala)
                                        .Include(s => s.UsuarioMestre)
                                        .Include(s => s.Convidados)
                                        .FirstOrDefault(x => x.SalaId == id);
            
            if(SalaEncontrada == null)
            {
                return NotFound();
            }
            
            SalaEncontrada.UsuarioMestre = 
                _ctx.Usuarios
                    .Include(u => u.DadosCompostosPessoais)
                        .ThenInclude(d => d.Fixos)
                            .ThenInclude(d => d.ModificadorFixo)
                    .Include(d => d.DadosCompostosPessoais)
                        .ThenInclude(d => d.Variaveis)
                        .ThenInclude(d => d.ModificadorVariavel)
                        .ThenInclude(d => d.Dado)
                    .Include(u => u.DadosSimplesPessoais)
                    .FirstOrDefault(u => u.UsuarioId == SalaEncontrada.UsuarioMestreId);
            List<Usuario> convidado = _ctx.Salas
                .Where(s => s.SalaId == SalaEncontrada.SalaId)
                .SelectMany(s => s.Convidados)                
                .Select(su => su.Usuario)                
                .ToList();

            for (int i = 0; i < convidado.Count; i++)
            {
                convidado[i] = 
                    _ctx.Usuarios
                        .Include(u => u.DadosCompostosPessoais)
                        .ThenInclude(d => d.Fixos)
                            .ThenInclude(d => d.ModificadorFixo)
                        .Include(d => d.DadosCompostosPessoais)
                            .ThenInclude(d => d.Variaveis)
                            .ThenInclude(d => d.ModificadorVariavel)
                            .ThenInclude(d => d.Dado)
                        .FirstOrDefault(u => u.UsuarioId == SalaEncontrada.Convidados[i].UsuarioId);
            }
            List<SalaUsuario> convidados = new();
            foreach (var user in convidado)
            {
                SalaUsuario temp = new()
                {
                    SalaId = SalaEncontrada.SalaId,
                    Sala = SalaEncontrada,
                    UsuarioId = user.UsuarioId,
                    Usuario = user
                };
                convidados.Add(temp);
            }
            SalaEncontrada.Convidados = convidados;             
            return Ok(SalaEncontrada);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Busca por IdLink
    [HttpGet("buscar/link/{idLink}")]

    public IActionResult BuscarSalaPorIdLink([FromRoute] string idLink)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas
                .Include(s => s.DadosCompostosSala)
                    .ThenInclude(s => s.Fixos)
                .Include(s => s.DadosCompostosSala)
                    .ThenInclude(s => s.Variaveis)
                        .ThenInclude(s => s.ModificadorVariavel)
                            .ThenInclude(s => s.Dado)                        
                .Include(s => s.DadosSimplesSala)
                .Include(s => s.UsuarioMestre)
                .Include(s => s.Convidados)
                .FirstOrDefault(x => x.IdLink == idLink);
            
            if(SalaEncontrada == null)
            {
                return NotFound();
            }
            
            SalaEncontrada.UsuarioMestre = 
                _ctx.Usuarios
                    .Include(u => u.DadosCompostosPessoais)
                        .ThenInclude(d => d.Fixos)
                            .ThenInclude(d => d.ModificadorFixo)
                    .Include(d => d.DadosCompostosPessoais)
                        .ThenInclude(d => d.Variaveis)
                        .ThenInclude(d => d.ModificadorVariavel)
                        .ThenInclude(d => d.Dado)
                    .Include(u => u.DadosSimplesPessoais)
                    .FirstOrDefault(u => u.UsuarioId == SalaEncontrada.UsuarioMestreId);
            List<Usuario> convidado = _ctx.Salas
                .Where(s => s.SalaId == SalaEncontrada.SalaId)
                .SelectMany(s => s.Convidados)                
                .Select(su => su.Usuario)                
                .ToList();

            for (int i = 0; i < convidado.Count; i++)
            {
                convidado[i] = 
                    _ctx.Usuarios
                        .Include(u => u.DadosCompostosPessoais)
                        .ThenInclude(d => d.Fixos)
                            .ThenInclude(d => d.ModificadorFixo)
                        .Include(d => d.DadosCompostosPessoais)
                            .ThenInclude(d => d.Variaveis)
                            .ThenInclude(d => d.ModificadorVariavel)
                            .ThenInclude(d => d.Dado)
                        .FirstOrDefault(u => u.UsuarioId == SalaEncontrada.Convidados[i].UsuarioId);
            }
            List<SalaUsuario> convidados = new();
            foreach (var user in convidado)
            {
                SalaUsuario temp = new()
                {
                    SalaId = SalaEncontrada.SalaId,
                    Sala = SalaEncontrada,
                    UsuarioId = user.UsuarioId,
                    Usuario = user
                };
                convidados.Add(temp);
            }
            SalaEncontrada.Convidados = convidados;             
            return Ok(SalaEncontrada);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Busca por IdSimples
    [HttpGet("buscar/six/{idSimples}")]

    public IActionResult BuscarSalaPorIdSimples([FromRoute] int idSimples)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas.Include(s => s.DadosCompostosSala).Include(s => s.DadosSimplesSala).Include(s => s.UsuarioMestre).Include(s => s.Convidados).FirstOrDefault(x => x.IdSimples == idSimples);
            
            if(SalaEncontrada == null)
            {
                return NotFound();
            }
            
            SalaEncontrada.UsuarioMestre = 
                _ctx.Usuarios
                    .Include(u => u.DadosCompostosPessoais)
                        .ThenInclude(d => d.Fixos)
                            .ThenInclude(d => d.ModificadorFixo)
                    .Include(d => d.DadosCompostosPessoais)
                        .ThenInclude(d => d.Variaveis)
                        .ThenInclude(d => d.ModificadorVariavel)
                        .ThenInclude(d => d.Dado)
                    .Include(u => u.DadosSimplesPessoais)
                    .FirstOrDefault(u => u.UsuarioId == SalaEncontrada.UsuarioMestreId);
            List<Usuario> convidado = _ctx.Salas
                .Where(s => s.SalaId == SalaEncontrada.SalaId)
                .SelectMany(s => s.Convidados)                
                .Select(su => su.Usuario)                
                .ToList();

            for (int i = 0; i < convidado.Count; i++)
            {
                convidado[i] = 
                    _ctx.Usuarios
                        .Include(u => u.DadosCompostosPessoais)
                        .ThenInclude(d => d.Fixos)
                            .ThenInclude(d => d.ModificadorFixo)
                        .Include(d => d.DadosCompostosPessoais)
                            .ThenInclude(d => d.Variaveis)
                            .ThenInclude(d => d.ModificadorVariavel)
                            .ThenInclude(d => d.Dado)
                        .FirstOrDefault(u => u.UsuarioId == SalaEncontrada.Convidados[i].UsuarioId);
            }
            List<SalaUsuario> convidados = new();
            foreach (var user in convidado)
            {
                SalaUsuario temp = new()
                {
                    SalaId = SalaEncontrada.SalaId,
                    Sala = SalaEncontrada,
                    UsuarioId = user.UsuarioId,
                    Usuario = user
                };
                convidados.Add(temp);
            }
            SalaEncontrada.Convidados = convidados;             
            return Ok(SalaEncontrada);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // List
    [HttpGet("listar")]
    public IActionResult ListarSalas()
    {
        try
        {
            List<Sala> salas = 
                _ctx.Salas
                    .Include(s => s.DadosCompostosSala)
                    .Include(s => s.DadosSimplesSala)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosCompostosPessoais)
                            .ThenInclude(um => um.Fixos)
                                .ThenInclude(um => um.ModificadorFixo)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosCompostosPessoais)
                            .ThenInclude(um => um.Variaveis)
                                .ThenInclude(um => um.ModificadorVariavel)
                                    .ThenInclude(um => um.Dado)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosSimplesPessoais)
                    .Include(s => s.Convidados)
                    .ToList();
            
            if(salas == null)
            {
                return NotFound();
            }

            return Ok(salas);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar/usuario/mestre/{username}")]
    public IActionResult ListarSalasUsuarioMestre([FromRoute] string username)
    {
        try
        {
            Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.Username == username);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            List<Sala> salas = 
                _ctx.Salas
                    .Include(s => s.DadosCompostosSala)
                    .Include(s => s.DadosSimplesSala)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosCompostosPessoais)
                            .ThenInclude(um => um.Fixos)
                                .ThenInclude(um => um.ModificadorFixo)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosCompostosPessoais)
                            .ThenInclude(um => um.Variaveis)
                                .ThenInclude(um => um.ModificadorVariavel)
                                    .ThenInclude(um => um.Dado)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosSimplesPessoais)
                    .Include(s => s.Convidados)
                        .ThenInclude(s => s.Usuario)
                    .Where(u => u.UsuarioMestreId == usuario.UsuarioId)
                    .ToList();
            
            if(salas == null)
            {
                return NotFound();
            }

            return Ok(salas);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar/usuario/jogador/{username}")]
    public IActionResult ListarSalasUsuarioJogador([FromRoute] string username)
    {
        try
        {
            Usuario? usuario = _ctx.Usuarios.Include(d => d.DadosCompostosPessoais).Include(d => d.DadosSimplesPessoais).FirstOrDefault(u => u.Username == username);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            List<Sala> salas = 
                _ctx.Salas
                    .Include(s => s.DadosCompostosSala)
                    .Include(s => s.DadosSimplesSala)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosCompostosPessoais)
                            .ThenInclude(um => um.Fixos)
                                .ThenInclude(um => um.ModificadorFixo)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosCompostosPessoais)
                            .ThenInclude(um => um.Variaveis)
                                .ThenInclude(um => um.ModificadorVariavel)
                                    .ThenInclude(um => um.Dado)
                    .Include(s => s.UsuarioMestre)
                        .ThenInclude(um => um.DadosSimplesPessoais)
                    .Include(s => s.Convidados)
                    .ToList();
            
            List<Sala> salasJog = new();

            foreach (var sala in salas)
            {
                foreach (var convidado in sala.Convidados)
                {
                    if(convidado.UsuarioId == usuario.UsuarioId){
                        salasJog.Add(sala);
                    }
                }
            }
            
            if(salas == null)
            {
                return NotFound();
            }

            return Ok(salasJog);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    //Update
    [HttpPut("atualizar/{id}")]
    public IActionResult AtualizarSala([FromBody] SalaDTO salaDTO, [FromRoute] int id)
    {
        try
        {
            Sala? salaEncontrada = _ctx.Salas
                                        .Include(s => s.DadosCompostosSala)
                                        .Include(s => s.DadosSimplesSala)
                                        .Include(s => s.UsuarioMestre)
                                        .Include(s => s.Convidados)
                                        .FirstOrDefault(x => x.SalaId == id);

            if(salaEncontrada == null)
            {
                return NotFound();
            }

            List<Usuario> convidado = _ctx.Usuarios.Where(s => salaDTO.ConvidadosId.Contains(s.UsuarioId)).ToList();
            List<SalaUsuario> convidados = new();
            foreach (var item in convidado)
            {
                SalaUsuario salaU = new()
                {
                    Usuario = item
                };
                convidados.Add(salaU);
            }

            List<DadoSimplesSala>? dadosSimples = _ctx.DadosSimplesSalas.Where(d => salaDTO.DadosSimplesSalaId.Contains(salaEncontrada.SalaId)).ToList();
            List<DadoCompostoSala>? dadosCompostos = _ctx.DadosCompostosSalas.Where(d => salaDTO.DadosCompostosSalaId.Contains(salaEncontrada.SalaId)).ToList();

            salaEncontrada.Nome = salaDTO.Nome;
            salaEncontrada.Descricao = salaDTO.Descricao;
            salaEncontrada.Convidados = convidados;
            salaEncontrada.DadosSimplesSala = dadosSimples;
            salaEncontrada.DadosCompostosSala = dadosCompostos;


            _ctx.Salas.Update(salaEncontrada);
            _ctx.SaveChanges();

            return Ok(salaEncontrada);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("adicionar/user/{idSala}/{usernameUser}")]
    public IActionResult AdicionarUsuarioSala([FromRoute] int idSala, [FromRoute] string usernameUser)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas.Include(s => s.DadosCompostosSala).Include(s => s.DadosSimplesSala).Include(s => s.UsuarioMestre).Include(s => s.Convidados).FirstOrDefault(x => x.SalaId == idSala);
            
            if(SalaEncontrada == null)
            {
                return NotFound();
            }

            Usuario usuario = _ctx.Usuarios.FirstOrDefault(u => u.Username.Equals(usernameUser));

            if(usuario == null){
                return NotFound();
            }

            SalaUsuario user = new()
            {
                Usuario = usuario,
                UsuarioId = usuario.UsuarioId,
                Sala = SalaEncontrada,
                SalaId = SalaEncontrada.SalaId
            };

            SalaEncontrada.Convidados.Add(user);
            _ctx.SaveChanges();

            return Ok(SalaEncontrada);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("remover/user/{idSala}/{usernameUser}")]
    public IActionResult RemoverUsuarioSala([FromRoute] int idSala, [FromRoute] string usernameUser)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas
                                        .Include(s => s.DadosCompostosSala)
                                        .Include(s => s.DadosSimplesSala)
                                        .Include(s => s.UsuarioMestre)
                                        .Include(s => s.Convidados)
                                        .FirstOrDefault(x => x.SalaId == idSala);
            
            if(SalaEncontrada == null)
            {
                return NotFound();
            }

            SalaUsuario userSala = new();

            Usuario usuario = _ctx.Usuarios.FirstOrDefault(u => u.Username.Equals(usernameUser));

            if(usuario == null){
                return NotFound();
            }
            
            foreach (var user in SalaEncontrada.Convidados)
            {
                if(user.Usuario == usuario){
                    SalaEncontrada.Convidados.Remove(user);
                    _ctx.Usuarios.UpdateRange(user.Usuario);
                    _ctx.SaveChanges();

                    return Ok(SalaEncontrada);
                }
            }

            return NotFound();
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Delete
    [HttpDelete("deletar/{id}")]
    public IActionResult ExcluirSala([FromRoute] int id)
    {
        try
        {
            Sala? sala = _ctx.Salas.Include(s => s.UsuarioMestre).FirstOrDefault(b => b.SalaId == id);
            if(sala == null)
            {
                return NotFound();
            }

            _ctx.Usuarios.UpdateRange(sala.UsuarioMestre);
            _ctx.Salas.Remove(sala);
            _ctx.SaveChanges();
            return Ok(sala);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("adicionar/dado/composto/{id}")]
    public IActionResult AdicionarDadoComposto([FromRoute] int id, [FromBody] DadoCompostoSalaDTO dadoCompostoSalaDTO)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas
                .Include(s => s.DadosCompostosSala)
                    .ThenInclude(d => d.Fixos)
                .Include(s => s.DadosCompostosSala)
                    .ThenInclude(d => d.Variaveis)
                .Include(s => s.DadosSimplesSala)
                .Include(s => s.UsuarioMestre)
                .Include(s => s.Convidados).
                FirstOrDefault(x => x.SalaId == id);

            if(SalaEncontrada == null){
                return NotFound();
            }

            Usuario? criador = 
                _ctx.Usuarios
                .Include(c => c.DadosSimplesPessoais)
                .Include(c => c.DadosCompostosPessoais)
                    .ThenInclude(c => c.Fixos)
                        .ThenInclude(x => x.ModificadorFixo)
                .Include(c => c.DadosCompostosPessoais)
                    .ThenInclude(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                .FirstOrDefault(x => x.Username == dadoCompostoSalaDTO.CriadorUsername);

            List<DadoCompostoSalaModFixo> fixos = new();
            List<DadoCompostoSalaModVar> variaveis = new();
            List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoCompostoSalaDTO.FixosId.Contains(mf.ModificadorFixoId)).ToList();
            List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoCompostoSalaDTO.VariaveisId.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
            
            foreach (var item in fixo)
            {
                DadoCompostoSalaModFixo mods = new()
                {
                    ModificadorFixo = item
                };
                fixos.Add(mods);
            }
            foreach (var item in variavel)
            {
                DadoCompostoSalaModVar mods = new()
                {
                    ModificadorVariavel = item
                };
                variaveis.Add(mods);
            }
            
            DadoCompostoSala? dadoSala = new()
            {
                AcessoPrivado = dadoCompostoSalaDTO.AcessoPrivado,
                Criador = criador,
                Faces = dadoCompostoSalaDTO.Faces,
                Nome = dadoCompostoSalaDTO.Nome,
                Quantidade = dadoCompostoSalaDTO.Quantidade,
                Condicao = dadoCompostoSalaDTO.Condicao,
                Fixos = fixos,
                Variaveis = variaveis
            };
            if (dadoSala == null)
            {
                return NotFound();
            }

            _ctx.DadosCompostosSalas.Add(dadoSala);
            _ctx.SaveChanges();
            
            SalaEncontrada.DadosCompostosSala.Add(dadoSala);
            _ctx.Salas.UpdateRange(SalaEncontrada);
            _ctx.SaveChanges();
            
            return Created("", dadoSala);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("atualizar/dado/composto/{idSala}/{idDado}")]
    public IActionResult AtualizarDadoComposto([FromRoute] int idDado,[FromRoute] int idSala, [FromBody] DadoCompostoSalaDTO dadoCompostoSalaDTO)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas
                .Include(s => s.DadosCompostosSala)
                    .ThenInclude(d => d.Fixos)
                .Include(s => s.DadosCompostosSala)
                    .ThenInclude(d => d.Variaveis)
                .Include(s => s.DadosSimplesSala)
                .Include(s => s.UsuarioMestre)
                .Include(s => s.Convidados).
                FirstOrDefault(x => x.SalaId == idSala);

            if(SalaEncontrada == null){
                return NotFound();
            }

            List<DadoCompostoSalaModFixo> fixos = new();
            List<DadoCompostoSalaModVar> variaveis = new();
            List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoCompostoSalaDTO.FixosId.Contains(mf.ModificadorFixoId)).ToList();
            List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoCompostoSalaDTO.VariaveisId.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
            
            foreach (var item in fixo)
            {
                DadoCompostoSalaModFixo mods = new()
                {
                    ModificadorFixo = item
                };
                fixos.Add(mods);
            }
            foreach (var item in variavel)
            {
                DadoCompostoSalaModVar mods = new()
                {
                    ModificadorVariavel = item
                };
                variaveis.Add(mods);
            }

            DadoCompostoSala? dadoSala = _ctx.DadosCompostosSalas.FirstOrDefault(d => d.DadoCompostoSalaId == idDado);
            
            if (dadoSala == null)
            {
                return NotFound();
            }

            dadoSala.AcessoPrivado = dadoCompostoSalaDTO.AcessoPrivado;
            dadoSala.Faces = dadoCompostoSalaDTO.Faces;
            dadoSala.Nome = dadoCompostoSalaDTO.Nome;
            dadoSala.Quantidade = dadoCompostoSalaDTO.Quantidade;
            dadoSala.Condicao = dadoCompostoSalaDTO.Condicao;
            dadoSala.Fixos = fixos;
            dadoSala.Variaveis = variaveis;

            _ctx.DadosCompostosSalas.Update(dadoSala);
            _ctx.SaveChanges();
            
            for (int i = 0; i < SalaEncontrada.DadosCompostosSala.Count; i++)
            {
                if(SalaEncontrada.DadosCompostosSala[i].DadoCompostoSalaId == dadoSala.DadoCompostoSalaId){
                    SalaEncontrada.DadosCompostosSala[i] = dadoSala;
                }
            }

            _ctx.Salas.UpdateRange(SalaEncontrada);
            _ctx.SaveChanges();
            
            return Ok(dadoSala);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("adicionar/dado/simples/{id}")]
    public IActionResult AdicionarDadoSimples([FromRoute] int id, [FromBody] DadoSimplesSalaDTO dadoSimplesSalaDTO)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas
                .Include(s => s.DadosCompostosSala)
                .Include(s => s.DadosSimplesSala)
                .Include(s => s.UsuarioMestre)
                .Include(s => s.Convidados).
                FirstOrDefault(x => x.SalaId == id);

            if(SalaEncontrada == null){
                return NotFound();
            }

            Usuario? criador = 
                _ctx.Usuarios
                .Include(c => c.DadosSimplesPessoais)
                .Include(c => c.DadosCompostosPessoais)
                    .ThenInclude(c => c.Fixos)
                        .ThenInclude(x => x.ModificadorFixo)
                .Include(c => c.DadosCompostosPessoais)
                    .ThenInclude(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                .FirstOrDefault(x => x.Username == dadoSimplesSalaDTO.CriadorUsername);
            DadoSimplesSala? dadoSala = new()
            {
                AcessoPrivado = dadoSimplesSalaDTO.AcessoPrivado,
                Criador = criador,
                Faces = dadoSimplesSalaDTO.Faces,
                Nome = dadoSimplesSalaDTO.Nome,
                Quantidade = dadoSimplesSalaDTO.Quantidade
            };
            
            if (dadoSala == null)
            {
                return NotFound();
            }

            _ctx.DadosSimplesSalas.Add(dadoSala);
            _ctx.SaveChanges();

            SalaEncontrada.DadosSimplesSala.Add(dadoSala);
            _ctx.Salas.UpdateRange(SalaEncontrada);
            _ctx.SaveChanges();
            
            return Created("", dadoSala);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}
