using System.Linq;
using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
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
            //Precisa buscar os DadosSimplesPessoais e DadosCompostosPessoais do UsuárioMestre e Convidados
            Sala? sala = _ctx.Salas.Include(s => s.DadosCompostosSala).Include(s => s.DadosSimplesSala).Include(s => s.UsuarioMestre).Include(s => s.Convidados).FirstOrDefault(x => x.SalaId == id);

            if(sala == null)
            {
                return NotFound();
            } 

            return Ok(sala);
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
            //Precisa buscar os DadosSimplesPessoais e DadosCompostosPessoais do UsuárioMestre e Convidados
            Sala? SalaEncontrada = _ctx.Salas.Include(s => s.DadosCompostosSala).Include(s => s.DadosSimplesSala).Include(s => s.UsuarioMestre).Include(s => s.Convidados).FirstOrDefault(x => x.IdLink == idLink);
            if(SalaEncontrada == null)
            {
                return NotFound();
            }
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
            //Precisa buscar os DadosSimplesPessoais e DadosCompostosPessoais do UsuárioMestre e Convidados
            Sala? SalaEncontrada = _ctx.Salas.Include(s => s.DadosCompostosSala).Include(s => s.DadosSimplesSala).Include(s => s.UsuarioMestre).Include(s => s.Convidados).FirstOrDefault(x => x.IdSimples == idSimples);
            if(SalaEncontrada == null)
            {
                return NotFound();
            }
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
            //Precisa buscar os DadosSimplesPessoais e DadosCompostosPessoais do UsuárioMestre e Convidados
            List<Sala> salas = _ctx.Salas.Include(s => s.DadosCompostosSala).Include(s => s.DadosSimplesSala).Include(s => s.UsuarioMestre).Include(s => s.Convidados).ToList();
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

    //Update
    [HttpPut("atualizar/{id}")]
    public IActionResult AtualizarSala([FromBody] SalaDTO salaDTO, [FromRoute] int id)
    {
        try
        {
            Sala? salaEncontrada = _ctx.Salas.FirstOrDefault(x => x.SalaId == id);
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

    // Delete
    [HttpDelete("deletar/{id}")]
    public IActionResult ExcluirSala([FromRoute] int id)
    {
        try
        {
            //Arrumar
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
}
