using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;
[ApiController]
[Route("dicenamics/modificador")]
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

    [HttpGet]
    [Route("listar")]
    public IActionResult ListarModificadores()
    {
        try
        {
            List<Modificador> modificadores = _ctx.Modificadores.ToList();
            if(modificadores == null)
            {
                return NotFound();
            }
            return Ok(modificadores);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Update
    [HttpPut]
    [Route("atualizar/{id}")]
    public IActionResult AtualizarModificador([FromBody] ModificadorDTO modificadorDTO, [FromRoute] int id)
    {
        try
        {
            Modificador? modificadorEncontrado = _ctx.Modificadores.Find(id);
            if(modificadorEncontrado == null){
                return NotFound();
            }
            modificadorEncontrado.Nome = modificadorDTO.Nome;
            modificadorEncontrado.Valor = modificadorDTO.Valor;

            _ctx.Modificadores.Update(modificadorEncontrado);
            _ctx.SaveChanges();
            return Ok(modificadorEncontrado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Delete
    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult ExcluirModificador([FromRoute] int id)
    {
        try
        {
            var modificador = _ctx.Modificadores.Find(id);
            if (modificador == null)
            {
                return NotFound();
            }
            _ctx.Modificadores.Remove(modificador);
            _ctx.SaveChanges();
            return Ok(modificador);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}
