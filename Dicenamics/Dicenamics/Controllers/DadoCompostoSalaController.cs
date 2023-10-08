using Dicenamics.Data;
using Dicenamics.DTO;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/dados/salas/simples")]

public class DadoCompostoSalaController : ControllerBase
{
    private readonly AppDatabase _ctx;

    public DadoCompostoSalaController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    // Create
    [HttpPost("criar")]
    public IActionResult CriarDadoCompostoSala([FromBody] DadoCompostoSalaDTO dadoSalaDTO)
    {
        try
        {
            Usuario? Criador = new();
            Usuario? criador = _ctx.Usuarios.FirstOrDefault(x => x.Username == Criador.Username);
            DadoCompostoSala? dadoSala = new()
            {
                AcessoPrivado = dadoSalaDTO.AcessoPrivado,
                Criador = criador,
                Faces = dadoSalaDTO.Faces,
                Nome = dadoSalaDTO.Nome,
                Quantidade = dadoSalaDTO.Quantidade,
                Condicao = dadoSalaDTO.Condicao,
                Fixos = dadoSalaDTO.Fixos,
                Variaveis = dadoSalaDTO.Variaveis
            };
            if (dadoSala == null)
            {
                return NotFound();
            }

            _ctx.DadosCompostosSalas.Add(dadoSala);
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
    public IActionResult GetDadoCompostoSala(int id)
    {
        try
        {
            DadoCompostoSala? dadoSala = _ctx.DadosCompostosSalas.FirstOrDefault(x => x.DadoCompostoSalaId == id);

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
    public IActionResult ListarDadoCompostoSala()
    {
        try
        {
            List<DadoCompostoSala>? dadosSala = _ctx.DadosCompostosSalas.ToList();
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
    public IActionResult UpdateDadoCompostoSala([FromRoute] int id, [FromBody] DadoCompostoSalaDTO dadoSalaDTO)
    {
        try
        {
            Usuario? Criador = new();
            Usuario? criador = _ctx.Usuarios.FirstOrDefault(x => x.Username == Criador.Username);
            DadoCompostoSala? dadoSala = _ctx.DadosCompostosSalas.FirstOrDefault(x => x.DadoCompostoSalaId == id);
            if (dadoSala == null)
            {
                return NotFound();
            }
            dadoSala.AcessoPrivado = dadoSalaDTO.AcessoPrivado;
            dadoSala.Criador = criador;
            dadoSala.Nome = dadoSalaDTO.Nome;
            dadoSala.Faces = dadoSalaDTO.Faces;
            dadoSala.Quantidade = dadoSalaDTO.Quantidade;
            dadoSala.Condicao = dadoSalaDTO.Condicao;
            dadoSala.Fixos = dadoSalaDTO.Fixos;
            dadoSala.Variaveis = dadoSalaDTO.Variaveis;
            if (dadoSala == null)
            {
                return NotFound();
            }
            _ctx.DadosCompostosSalas.Update(dadoSala);
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
    public IActionResult DeleteDadoCompostoSala([FromRoute] int id)
    {
        try
        {
            DadoCompostoSala? dadoSala = _ctx.DadosCompostosSalas.FirstOrDefault(x => x.DadoCompostoSalaId == id);
            if (dadoSala == null)
            {
                return NotFound();
            }
            _ctx.DadosCompostosSalas.Remove(dadoSala);
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