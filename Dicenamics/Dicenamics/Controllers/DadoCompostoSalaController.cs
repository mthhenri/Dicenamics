using Dicenamics.Data;
using Dicenamics.DTO;
using Dicenamics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/dados/salas/composto")]

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
            Usuario? criador = 
                _ctx.Usuarios
                .Include(c => c.DadosSimplesPessoais)
                .Include(c => c.DadosCompostosPessoais)
                    .ThenInclude(c => c.Fixos)
                        .ThenInclude(x => x.ModificadorFixo)
                .Include(c => c.DadosCompostosPessoais)
                    .ThenInclude(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                .FirstOrDefault(x => x.Username == dadoSalaDTO.CriadorUsername);

            List<DadoCompostoSalaModFixo> fixos = new();
            List<DadoCompostoSalaModVar> variaveis = new();
            List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoSalaDTO.Fixos.Contains(mf.ModificadorFixoId)).ToList();
            List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoSalaDTO.Variaveis.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
            
            foreach (var item in fixo)
            {
                DadoCompostoSalaModFixo mods = new()
                {
                    ModificadorFixo = item
                };
                fixos.Add(mods);
            }
            foreach (var item in variavel)
            {
                DadoCompostoSalaModVar mods = new()
                {
                    ModificadorVariavel = item
                };
                variaveis.Add(mods);
            }
            
            DadoCompostoSala? dadoSala = new()
            {
                AcessoPrivado = dadoSalaDTO.AcessoPrivado,
                Criador = criador,
                Faces = dadoSalaDTO.Faces,
                Nome = dadoSalaDTO.Nome,
                Quantidade = dadoSalaDTO.Quantidade,
                Condicao = dadoSalaDTO.Condicao,
                Fixos = fixos,
                Variaveis = variaveis
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
            DadoCompostoSala? dadoSala = 
                _ctx.DadosCompostosSalas
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Fixos)
                                .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Variaveis)
                                .ThenInclude(c => c.ModificadorVariavel)
                                    .ThenInclude(c => c.Dado)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosSimplesPessoais)
                    .Include(c => c.Fixos)
                        .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                    .FirstOrDefault(x => x.DadoCompostoSalaId == id);

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
            List<DadoCompostoSala>? dadosSala = 
                _ctx.DadosCompostosSalas
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Fixos)
                                .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Variaveis)
                                .ThenInclude(c => c.ModificadorVariavel)
                                    .ThenInclude(c => c.Dado)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosSimplesPessoais)
                    .Include(c => c.Fixos)
                        .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                    .ToList();

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
            DadoCompostoSala? dadoSala = 
                _ctx.DadosCompostosSalas
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Fixos)
                                .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Variaveis)
                                .ThenInclude(c => c.ModificadorVariavel)
                                    .ThenInclude(c => c.Dado)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosSimplesPessoais)
                    .Include(c => c.Fixos)
                        .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                    .FirstOrDefault(x => x.DadoCompostoSalaId == id);

            if (dadoSala == null)
            {
                return NotFound();
            }

            List<DadoCompostoSalaModFixo> fixos = new();
            List<DadoCompostoSalaModVar> variaveis = new();
            List<ModificadorFixo> fixo = _ctx.ModificadoresFixos.Where(mf => dadoSalaDTO.Fixos.Contains(mf.ModificadorFixoId)).ToList();
            List<ModificadorVariavel> variavel = _ctx.ModificadoresVariaveis.Where(mf => dadoSalaDTO.Variaveis.Contains(mf.ModificadorVariavelId)).Include(d => d.Dado).ToList();
            
            foreach (var item in fixo)
            {
                DadoCompostoSalaModFixo mods = new()
                {
                    ModificadorFixo = item
                };
                fixos.Add(mods);
            }
            foreach (var item in variavel)
            {
                DadoCompostoSalaModVar mods = new()
                {
                    ModificadorVariavel = item
                };
                variaveis.Add(mods);
            }

            dadoSala.AcessoPrivado = dadoSalaDTO.AcessoPrivado;
            dadoSala.Criador = criador;
            dadoSala.Nome = dadoSalaDTO.Nome;
            dadoSala.Faces = dadoSalaDTO.Faces;
            dadoSala.Quantidade = dadoSalaDTO.Quantidade;
            dadoSala.Condicao = dadoSalaDTO.Condicao;
            dadoSala.Fixos = fixos;
            dadoSala.Variaveis = variaveis;

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
            DadoCompostoSala? dadoSala = 
                _ctx.DadosCompostosSalas
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Fixos)
                                .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Variaveis)
                                .ThenInclude(c => c.ModificadorVariavel)
                                    .ThenInclude(c => c.Dado)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosSimplesPessoais)
                    .Include(c => c.Fixos)
                        .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                    .FirstOrDefault(x => x.DadoCompostoSalaId == id);

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

    [HttpGet("rolar/{id}")]
    public IActionResult RolarDado([FromRoute] int id)
    {
        try
        {
            DadoCompostoSala? dadoSala = 
                _ctx.DadosCompostosSalas
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Fixos)
                                .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosCompostosPessoais)
                            .ThenInclude(c => c.Variaveis)
                                .ThenInclude(c => c.ModificadorVariavel)
                                    .ThenInclude(c => c.Dado)
                    .Include(c => c.Criador)
                        .ThenInclude(c => c.DadosSimplesPessoais)
                    .Include(c => c.Fixos)
                        .ThenInclude(c => c.ModificadorFixo)
                    .Include(c => c.Variaveis)
                        .ThenInclude(c => c.ModificadorVariavel)
                            .ThenInclude(c => c.Dado)
                    .FirstOrDefault(x => x.DadoCompostoSalaId == id);
            
            if(dadoSala == null)
            {
                return NotFound();
            }

            return Ok(dadoSala.RolarDado());
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }


}