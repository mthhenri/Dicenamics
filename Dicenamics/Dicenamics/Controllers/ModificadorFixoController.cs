using Dicenamics.Data;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;
[ApiController]
[Route("dicenamics/modificadores/fixo")]
public class ModificadorFixoController : ControllerBase
{
    private readonly AppDatabase _ctx;
    public ModificadorFixoController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    // Create
    [HttpPost("criar")]
    public IActionResult AdicionarModificador([FromBody] ModificadorFixoDTO modificadorFixoDTO)
    {
        try
        {
            ModificadorFixo? modificadorFixo = new()
            {
                Nome = modificadorFixoDTO.Nome,
                Valor = modificadorFixoDTO.Valor
            };
            
            _ctx.ModificadoresFixos.Add(modificadorFixo);
            _ctx.SaveChanges();
            return Created("", modificadorFixo);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Read
    [HttpGet("buscar/{id}")]
    public IActionResult ObterModificadorPorId([FromRoute] int id)
    {
        try
        {
            ModificadorFixo? modificadorFixo = _ctx.ModificadoresFixos.FirstOrDefault(x => x.ModificadorFixoId == id);
            if(modificadorFixo == null){
                return NotFound();
            }
            return Ok(modificadorFixo);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar")]
    public IActionResult ListarModificadores()
    {
        try
        {
            List<ModificadorFixo> modificadoresFixos = _ctx.ModificadoresFixos.ToList();
            if(modificadoresFixos == null)
            {
                return NotFound();
            }
            return Ok(modificadoresFixos);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Update
    [HttpPut("atualizar/{id}")]
    public IActionResult AtualizarModificador([FromBody] ModificadorFixoDTO modificadorFixoDTO, [FromRoute] int id)
    {
        try
        {
            ModificadorFixo? modificadorFixoEncontrado = _ctx.ModificadoresFixos.FirstOrDefault(x => x.ModificadorFixoId == id);
            if(modificadorFixoEncontrado == null){
                return NotFound();
            }
            modificadorFixoEncontrado.Nome = modificadorFixoDTO.Nome;
            modificadorFixoEncontrado.Valor = modificadorFixoDTO.Valor;

            _ctx.ModificadoresFixos.Update(modificadorFixoEncontrado);
            _ctx.SaveChanges();
            return Ok(modificadorFixoEncontrado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Delete
    [HttpDelete("deletar/{id}")]
    public IActionResult ExcluirModificador([FromRoute] int id)
    {
        try
        {
            ModificadorFixo? modificadorFixo = _ctx.ModificadoresFixos.FirstOrDefault(x => x.ModificadorFixoId == id);
            if (modificadorFixo == null)
            {
                return NotFound();
            }
            _ctx.ModificadoresFixos.Remove(modificadorFixo);
            _ctx.SaveChanges();
            return Ok(modificadorFixo);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}
