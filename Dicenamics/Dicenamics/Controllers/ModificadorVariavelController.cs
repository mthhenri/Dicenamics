using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/modificadores/variavel")]

public class ModificadorVariavelController : ControllerBase
{
    private readonly AppDatabase _ctx;
    public ModificadorVariavelController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    // Create
    [HttpPost("criar")]
    public IActionResult AdicionarModificadorVar([FromBody] ModificadorVariavelDTO modificadorVariavelDTO)
    {
        try
        {
            ModificadorVariavel? modificadorVariavel = new()
            {
                Nome = modificadorVariavelDTO.Nome,
                Dado = modificadorVariavelDTO.Dado
            };

            _ctx.ModificadoresVariaveis.Add(modificadorVariavel);
            _ctx.SaveChanges();
            return Created("", modificadorVariavel);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Read
    [HttpGet("buscar/{id}")]
    public IActionResult ObterModificadorVarPorId([FromRoute] int id)
    {
        try
        {
            ModificadorVariavel? modificadorVariavel = _ctx.ModificadoresVariaveis.FirstOrDefault(x => x.ModificadorVariavelId == id);
            if(modificadorVariavel == null)
            {
                return NotFound();
            }
            return Ok(modificadorVariavel);
        } catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar")]
    public IActionResult ListarModificadoresVar()
    {
        try
        {
            List<ModificadorVariavel> modificadoresVariaveis = _ctx.ModificadoresVariaveis.ToList();
            if(modificadoresVariaveis == null)
            {
                return  NotFound();
            }
            return Ok(modificadoresVariaveis);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}
