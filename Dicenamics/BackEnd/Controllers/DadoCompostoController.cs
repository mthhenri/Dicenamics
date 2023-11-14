using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dicenamics.Controllers;
[ApiController]
[Route("dicenamics/dados/composto")]
public class DadoCompostoController : ControllerBase
{
    private readonly AppDatabase _ctx;
    public DadoCompostoController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    [HttpPost("criar")]
    public IActionResult CriarDado([FromBody] DadoCompostoDTO dadoCompostoDTO)
    {
        try
        {
            List<DadoCompostoModFixo> fixos = new();
            List<DadoCompostoModVar> variaveis = new();
            List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoCompostoDTO.Fixos.Contains(mf.ModificadorFixoId)).ToList();
            List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoCompostoDTO.Variaveis.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
            
            foreach (var item in fixo)
            {
                DadoCompostoModFixo mods = new()
                {
                    ModificadorFixo = item
                };
                fixos.Add(mods);
            }
            foreach (var item in variavel)
            {
                DadoCompostoModVar mods = new()
                {
                    ModificadorVariavel = item
                };
                variaveis.Add(mods);
            }
            
            DadoComposto? dadoComposto = new()
            {
                Nome = dadoCompostoDTO.Nome,
                Faces = dadoCompostoDTO.Faces,
                Quantidade = dadoCompostoDTO.Quantidade,
                Condicao = dadoCompostoDTO.Condicao,
                Fixos = fixos,
                Variaveis = variaveis,
            };
            if(dadoComposto == null)
            {
                return NotFound();
            }
            _ctx.DadosCompostos.Add(dadoComposto);
            _ctx.SaveChanges();
            return Created("", dadoComposto);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listar")]
    public IActionResult ListarDados()
    {
        try
        {
            List<DadoComposto>? dados = 
                _ctx.DadosCompostos
                    .Include(d => d.Fixos)
                        .ThenInclude(f => f.ModificadorFixo)
                    .Include(d => d.Variaveis)
                        .ThenInclude(f => f.ModificadorVariavel)
                            .ThenInclude(f => f.Dado)
                    .ToList();
            if (dados == null)
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

    [HttpGet("buscar/{id}")]
    public IActionResult BuscaDado([FromRoute] int id)
    {
        try
        {
            DadoComposto? dado = _ctx.DadosCompostos
                                    .Include(d => d.Fixos)
                                        .ThenInclude(f => f.ModificadorFixo)
                                    .Include(d => d.Variaveis)
                                        .ThenInclude(f => f.ModificadorVariavel)
                                            .ThenInclude(d => d.Dado)
                                    .FirstOrDefault(x => x.DadoId == id);
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

    [HttpPut("atualizar/{id}")]
    public IActionResult AtualizarDado([FromRoute] int id, [FromBody] DadoCompostoDTO dadoCompostoDTO)
    {
        try
        {
            DadoComposto? dado = _ctx.DadosCompostos
                                    .Include(d => d.Fixos)
                                        .ThenInclude(f => f.ModificadorFixo)
                                    .Include(d => d.Variaveis)
                                        .ThenInclude(f => f.ModificadorVariavel)
                                            .ThenInclude(d => d.Dado)
                                    .FirstOrDefault(x => x.DadoId == id);
            dado.Nome = dadoCompostoDTO.Nome;
            dado.Faces = dadoCompostoDTO.Faces;
            dado.Quantidade = dadoCompostoDTO.Quantidade;
            dado.Condicao = dadoCompostoDTO.Condicao;

            List<DadoCompostoModFixo> fixos = new();
            List<DadoCompostoModVar> variaveis = new();
            List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoCompostoDTO.Fixos.Contains(mf.ModificadorFixoId)).ToList();
            List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoCompostoDTO.Variaveis.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
            
            foreach (var item in fixo)
            {
                DadoCompostoModFixo mods = new()
                {
                    ModificadorFixo = item
                };
                fixos.Add(mods);
            }
            foreach (var item in variavel)
            {
                DadoCompostoModVar mods = new()
                {
                    ModificadorVariavel = item
                };
                variaveis.Add(mods);
            }

            dado.Fixos = fixos;
            dado.Variaveis = variaveis;

            _ctx.DadosCompostos.Update(dado);
            _ctx.SaveChanges();

            return Ok(dado);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("deletar/{id}")]
    public IActionResult DeletarDado([FromRoute] int id)
    {
        try
        {
            DadoComposto? dado = _ctx.DadosCompostos.Include(d => d.Fixos).ThenInclude(f => f.ModificadorFixo).Include(d => d.Variaveis).ThenInclude(f => f.ModificadorVariavel).ThenInclude(d => d.Dado).FirstOrDefault(x => x.DadoId == id);
            if (dado == null)
            {
                return NotFound();
            }
            _ctx.DadosCompostos.Remove(dado);
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
    public IActionResult RolarDado([FromRoute] int id)
    {
        try
        {
            DadoComposto? dado = _ctx.DadosCompostos
                .Include(d => d.Fixos)
                    .ThenInclude(f => f.ModificadorFixo)
                .Include(d => d.Variaveis)
                    .ThenInclude(f => f.ModificadorVariavel)
                        .ThenInclude(d => d.Dado)
                .FirstOrDefault(x => x.DadoId == id);
            if (dado == null)
            {
                return NotFound();
            }
            
            List<ModificadorFixo> fixo = new();
            List<ModificadorVariavel> variavel = new();
            List<DadoCompostoModFixo> fixos = new();
            List<DadoCompostoModVar> variaveis = new();

            if(dado.Fixos != null)
            {
                for (int i = 0; i < dado.Fixos.Count; i++)
                {
                    ModificadorFixo? mod = _ctx.ModificadoresFixos.FirstOrDefault(m => m.ModificadorFixoId == dado.Fixos[i].ModificadorId);
                    fixo.Add(mod);
                }
                foreach (var item in fixo)
                {
                    DadoCompostoModFixo mods = new()
                    {
                        ModificadorFixo = item
                    };
                    fixos.Add(mods);
                }
                dado.Fixos = fixos;
            }

            if(dado.Variaveis != null)
            {
                for (int i = 0; i < dado.Fixos.Count; i++)
                {
                    ModificadorVariavel? mod = _ctx.ModificadoresVariaveis.FirstOrDefault(m => m.ModificadorVariavelId == dado.Variaveis[i].ModificadorId);
                    variavel.Add(mod);
                }            
                foreach (var item in variavel)
                {
                    DadoCompostoModVar mods = new()
                    {
                        ModificadorVariavel = item
                    };
                    variaveis.Add(mods);
                }
                dado.Variaveis = variaveis;
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
