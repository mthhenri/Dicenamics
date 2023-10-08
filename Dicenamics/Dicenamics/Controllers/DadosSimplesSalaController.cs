using Dicenamics.Data;
using Dicenamics.DTO;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/dados/salas/simples")]

public class DadoSimplesSalaController : ControllerBase
{
    private readonly AppDatabase _ctx;

    public DadoSimplesSalaController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    // Create
    [HttpPost("criar")]
    public IActionResult CriarDadoSimplesSala([FromBody] DadoSimplesSalaDTO dadoSalaDTO)
    {
        try
        {
            Usuario? Criador = new();
            Usuario? criador = _ctx.Usuarios.FirstOrDefault(x => x.Username == Criador.Username);
            DadoSimplesSala? dadoSala = new()
            {
                AcessoPrivado = dadoSalaDTO.AcessoPrivado,
                Criador = criador,
                Faces = dadoSalaDTO.Faces,
                Nome = dadoSalaDTO.Nome,
                Quantidade = dadoSalaDTO.Quantidade
            };
            if (dadoSala == null)
            {
                return NotFound();
            }

            _ctx.DadosSimplesSalas.Add(dadoSala);
            _ctx.SaveChanges();
            return Created("", dadoSala);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Read
     [HttpGet("buscar/{id}")]
    public IActionResult GetDadoSimplesSala(int id)
    {
        try
        {
            DadoSimplesSala? dadoSala = _ctx.DadosSimplesSalas.FirstOrDefault(x => x.DadoSimplesSalaId == id);

            if ( dadoSala == null)
            {
                return NotFound();
            }

            return Ok(dadoSala);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar")]
    public IActionResult ListarDadoSimplesSala()
    {
        try
        {
            List<DadoSimplesSala>? dadosSala = _ctx.DadosSimplesSalas.ToList();
            if(dadosSala == null)
            {
                return NotFound();
            }
            return Ok(dadosSala);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

     // Update
    [HttpPut("atualizar/{id}")]
    public IActionResult UpdateDadoSimplesSala([FromRoute] int id, [FromBody] DadoSimplesSalaDTO dadoSalaDTO)
    {
        try
        {
            Usuario? Criador = new();
            Usuario? criador = _ctx.Usuarios.FirstOrDefault(x => x.Username == Criador.Username);
            DadoSimplesSala? dadoSala = _ctx.DadosSimplesSalas.FirstOrDefault(x => x.DadoSimplesSalaId == id);
            if (dadoSala == null)
            {
                return NotFound();
            }
            dadoSala.AcessoPrivado = dadoSalaDTO.AcessoPrivado;
            dadoSala.Criador = criador;
            dadoSala.Nome = dadoSalaDTO.Nome;
            dadoSala.Faces = dadoSalaDTO.Faces;
            dadoSala.Quantidade = dadoSalaDTO.Quantidade;
            if (dadoSala == null)
            {
                return NotFound();
            }
            _ctx.DadosSimplesSalas.Update(dadoSala);
            _ctx.SaveChanges();
            return Ok(dadoSala);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Delete
    [HttpDelete("deletar/{id}")]
    public IActionResult DeleteDadoSimplesSala([FromRoute] int id)
    {
        try
        {
            DadoSimplesSala? dadoSala = _ctx.DadosSimplesSalas.FirstOrDefault(x => x.DadoSimplesSalaId == id);
            if (dadoSala == null)
            {
                return NotFound();
            }
            _ctx.DadosSimplesSalas.Remove(dadoSala);
            _ctx.SaveChanges();
            return Ok(dadoSala);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }


}