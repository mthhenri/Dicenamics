using Dicenamics.Data;
using Dicenamics.DTO;
using Dicenamics.DTOs;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            DadoSimples? dadoMod = new()
            {
                Nome = modificadorVariavelDTO.Dado.Nome,
                Faces = modificadorVariavelDTO.Dado.Faces,
                Quantidade = modificadorVariavelDTO.Dado.Quantidade,
            };
            _ctx.DadosSimples.Add(dadoMod);
            _ctx.SaveChanges();
            ModificadorVariavel? modificadorVariavel = new()
            {
                Nome = modificadorVariavelDTO.Nome,
                Dado = dadoMod,
                DadoSimplesId = dadoMod.DadoId
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
            ModificadorVariavel? modificadorVariavel = _ctx.ModificadoresVariaveis.Include(d => d.Dado).FirstOrDefault(x => x.ModificadorVariavelId == id);
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
            List<ModificadorVariavel> modificadoresVariaveis = _ctx.ModificadoresVariaveis.Include(d => d.Dado).ToList();
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

    // Update 
    [HttpPut("atualizar/{id}")]
    public IActionResult AtualizarModificadorVar([FromBody] ModificadorVariavelDTO modificadorVariavelDTO, [FromRoute] int id)
    {
        try
        {
            ModificadorVariavel? modificadorVariavelEncontrado = _ctx.ModificadoresVariaveis.Include(d => d.Dado).FirstOrDefault(x => x.ModificadorVariavelId == id);
            if(modificadorVariavelEncontrado == null)
            {
                return NotFound();
            }

            modificadorVariavelEncontrado.Nome = modificadorVariavelDTO.Nome;
            modificadorVariavelEncontrado.Dado.Nome = modificadorVariavelDTO.Dado.Nome;
            modificadorVariavelEncontrado.Dado.Faces = modificadorVariavelDTO.Dado.Faces;
            modificadorVariavelEncontrado.Dado.Quantidade = modificadorVariavelDTO.Dado.Quantidade;
            
            _ctx.ModificadoresVariaveis.Update(modificadorVariavelEncontrado);
            _ctx.SaveChanges();
            return Ok(modificadorVariavelEncontrado);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // Delete
    [HttpDelete("deletar/{id}")]
    public IActionResult ExcluirModificadorVar([FromRoute] int id)
    {
        try
        {
            ModificadorVariavel? modificadorVariavel = _ctx.ModificadoresVariaveis.Include(d => d.Dado).FirstOrDefault(x => x.ModificadorVariavelId == id);
            if(modificadorVariavel == null)
            {
                return NotFound();
            }

            _ctx.DadosSimples.RemoveRange(modificadorVariavel.Dado);
            _ctx.ModificadoresVariaveis.Remove(modificadorVariavel);
            _ctx.SaveChanges();
            return Ok(modificadorVariavel);
        } 
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    //Rolar dado de modificador
    [HttpGet("rolar/{id}")]
    public IActionResult RolarDadoModificadorVar([FromRoute] int id)
    {
        try
        {
            ModificadorVariavel? modificadorVariavel = _ctx.ModificadoresVariaveis.Include(d => d.Dado).FirstOrDefault(x => x.ModificadorVariavelId == id);
            if(modificadorVariavel == null)
            {
                return NotFound();
            }
            DadoSimples? dadoMod = _ctx.DadosSimples.FirstOrDefault(x => x.DadoId == modificadorVariavel.DadoSimplesId);
            List<int> resultado = dadoMod.RolarDado();
            return Ok(resultado);
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}