using Dicenamics.Data;
using Dicenamics.DTO;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/dados/base")]
public class DadoSimplesController : ControllerBase
{
    private readonly AppDatabase _ctx;
    public DadoSimplesController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    //Usado para a criação da base dos CRUDs: https://chat.openai.com/share/5884bcec-e7e1-4a65-82e2-651c1efac12b

    // CRUD - Create
    [HttpPost("criar")]
    public IActionResult CreateDadoSimples([FromBody] DadoSimplesDTO dadoDTO)
    {
        try
        {
            DadoSimples? dado = new()
            {
                Nome = dadoDTO.Nome,
                Faces = dadoDTO.Faces,
                Quantidade = dadoDTO.Quantidade,
                Condicao = dadoDTO.Condicao
            };
            if (dado == null)
            {
                return NotFound();
            }

            _ctx.DadosSimples.Add(dado);
            _ctx.SaveChanges();
            return Created("", dado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // CRUD - Read
    [HttpGet("buscar/{id}")]
    public IActionResult GetDadoSimples(int id)
    {
        try
        {
            DadoSimples? dado = _ctx.DadosSimples.FirstOrDefault(x => x.DadoSimplesId == id);

            if (dado == null)
            {
                return NotFound();
            }

            return Ok(dado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar")]
    public IActionResult ListarDadoSimples()
    {
        try
        {
            List<DadoSimples>? dados = _ctx.DadosSimples.ToList();
            if(dados == null)
            {
                return NotFound();
            }
            return Ok(dados);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // CRUD - Update
    [HttpPut("atualizar/{id}")]
    public IActionResult UpdateDadoSimples([FromRoute] int id, [FromBody] DadoSimplesDTO dadoDTO)
    {
        try
        {
            DadoSimples? dado = _ctx.DadosSimples.FirstOrDefault(x => x.DadoSimplesId == id);
            if (dado == null)
            {
                return NotFound();
            }
            dado.Nome = dadoDTO.Nome;
            dado.Faces = dadoDTO.Faces;
            dado.Quantidade = dadoDTO.Quantidade;
            dado.Condicao = dadoDTO.Condicao;
            if (dado == null)
            {
                return NotFound();
            }
            _ctx.DadosSimples.Update(dado);
            _ctx.SaveChanges();
            return Ok(dado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    // CRUD - Delete
    [HttpDelete("deletar/{id}")]
    public IActionResult DeleteDadoSimples([FromRoute] int id)
    {
        try
        {
            DadoSimples? dado = _ctx.DadosSimples.FirstOrDefault(x => x.DadoSimplesId == id);
            if (dado == null)
            {
                return NotFound();
            }
            _ctx.DadosSimples.Remove(dado);
            _ctx.SaveChanges();
            return Ok(dado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("rolar/{id}")]
    public IActionResult RolarDados([FromRoute] int id)
    {
        try
        {
            DadoSimples? dado = _ctx.DadosSimples.FirstOrDefault(x => x.DadoSimplesId == id);
            if (dado == null)
            {
                return NotFound();
            }
            return Ok(dado.RolarDado());
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}
