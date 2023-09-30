using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;
[ApiController]
[Route("/modificador")]
public class ModificadorController : ControllerBase
{
    private readonly AppDatabase _ctx;
    public ModificadorController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    // Create
    [HttpPost]
    [Route("criar")]
    public IActionResult AdicionarModificador([FromBody] ModificadorDTO modificadorDTO)
    {
        try
        {
            Modificador? modificador = new()
            {
                Nome = modificadorDTO.Nome,
                Valor = modificadorDTO.Valor
            };
            
            _ctx.Modificadores.Add(modificador);
            _ctx.SaveChanges();
            return Created("", modificador);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Read
    [HttpGet]
    [Route("buscar/{id}")]
    public IActionResult ObterModificadorPorId([FromRoute] int id)
    {
        try
        {
            Modificador? modificador = _ctx.Modificadores.Find(id);
            if(modificador == null){
                return NotFound();
            }
            return Ok(modificador);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    
}
