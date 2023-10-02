using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

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
            Sala sala = new()
            {
                Nome = salaDTO.Nome,
                Descricao = salaDTO.Descricao,
                IdLink = salaDTO.IdLink,
                UsuarioMestreId = salaDTO.UsuarioMestreId,
                UsuarioMestre = UsuarioEncontrado,
                Convidados = salaDTO.Convidados,
                DadosCriados = salaDTO.DadosCriados
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

    // Adicionar busca por IdLink e por IdSimples

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
                salaEncontrada.Nome = salaDTO.Nome;
                salaEncontrada.Descricao = salaDTO.Descricao;
                salaEncontrada.IdLink = salaDTO.IdLink;
                salaEncontrada.Convidados = salaDTO.Convidados;
                salaEncontrada.DadosCriados = salaDTO.DadosCriados;

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
