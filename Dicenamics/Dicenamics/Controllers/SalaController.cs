using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;
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
            Usuario? UsuarioEncontrado = _ctx.Usuarios.FirstOrDefault(x => x.UsuarioId == salaDTO.UsuarioMestreId);
            List<Usuario>? convidados = _ctx.Usuarios.Where(u => salaDTO.ConvidadosId.Contains(u.UsuarioId)).ToList();
            Sala sala = new()
            {
                Nome = salaDTO.Nome,
                Descricao = salaDTO.Descricao,
                UsuarioMestreId = salaDTO.UsuarioMestreId,
                UsuarioMestre = UsuarioEncontrado,
                Convidados = convidados
            };
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
    [HttpGet("buscar/{id}")]
    public IActionResult ObterSalaPorId([FromRoute] int id)
    {
        try
        {
            Sala? sala = _ctx.Salas.FirstOrDefault(x => x.SalaId == id);
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
    [HttpGet("buscar/{idLink}")]

    public IActionResult BuscarSalaPorIdLink([FromRoute] string idLink)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas.FirstOrDefault(x => x.IdLink == idLink);
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
    [HttpGet("buscar/{idSimples}")]

    public IActionResult BuscarSalaPorIdSimples([FromRoute] int idSimples)
    {
        try
        {
            Sala? SalaEncontrada = _ctx.Salas.FirstOrDefault(x => x.IdSimples == idSimples);
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
            List<Sala> salas = _ctx.Salas.ToList();
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

            List<Usuario>? convidados = _ctx.Usuarios.Where(u => salaDTO.ConvidadosId.Contains(u.UsuarioId)).ToList();
            // Arrumar DTOs
            List<DadoSimplesSala> dadosSimples = _ctx.DadosSimplesSalas.Where(d => salaDTO.DadosSimplesSalaId.Contains(salaEncontrada.SalaId)).ToList();
            List<DadoCompostoSala> dadosCompostos = _ctx.DadosCompostosSalas.Where(d => salaDTO.DadosCompostosSalaId.Contains(salaEncontrada.SalaId)).ToList();

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
            Sala? sala = _ctx.Salas.FirstOrDefault(x => x.SalaId == id);
            if(sala == null)
            {
                return NotFound();
            }
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
