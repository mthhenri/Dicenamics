using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/dados/rolagens")]
public class RolagemDadoSalaController : ControllerBase
{
    private readonly AppDatabase _ctx;
    public RolagemDadoSalaController(AppDatabase ctx)
    {
        _ctx = ctx;
    }

    [HttpPost("gerar/{dadoId}/{salaId}/{tipoRolagem}/{usuarioRoll}")]
    public IActionResult GerarResultado([FromRoute] int dadoId, [FromRoute] int salaId, [FromRoute] string tipoRolagem, [FromRoute] string usuarioRoll)
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
                    .FirstOrDefault(x => x.DadoCompostoSalaId == dadoId);
            
            if(dadoSala == null)
            {
                return NotFound();
            }

            Sala? salaEncontrada = _ctx.Salas
                                        .Include(s => s.DadosCompostosSala)
                                        .Include(s => s.DadosSimplesSala)
                                        .Include(s => s.UsuarioMestre)
                                        .Include(s => s.Convidados)
                                        .FirstOrDefault(x => x.SalaId == salaId);

            if(salaEncontrada == null)
            {
                return NotFound();
            }

            RolagemDadoSala rolagem = new();
            rolagem.UsuarioUsername = usuarioRoll;
            rolagem.Resultados = JsonConvert.SerializeObject(dadoSala.RolarDado());
            rolagem.TipoRolagem = tipoRolagem;
            rolagem.DadoComposto = dadoSala;
            rolagem.DadoId = dadoSala.DadoCompostoSalaId;
            rolagem.Sala = salaEncontrada;

            _ctx.RolagemsDadosSalas.Add(rolagem);
            _ctx.SaveChanges();

            return Created("", rolagem);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest();
        }
    }

    [HttpGet("listar/sala/{salaId}")]
    public IActionResult ListarResultadosSala([FromRoute] int salaId){
        try
        {
            List<RolagemDadoSala> rolagensSala = _ctx.RolagemsDadosSalas
                .Include(r => r.DadoComposto)
                    .ThenInclude(r => r.Criador)
                .Include(r => r.DadoComposto)
                    .ThenInclude(r => r.Fixos)
                .Include(r => r.DadoComposto)
                    .ThenInclude(r => r.Variaveis)
                        .ThenInclude(r => r.ModificadorVariavel)
                            .ThenInclude(r => r.Dado)
                .Include(r => r.Sala)
                .Where(d => d.SalaId == salaId)
                .ToList();

            return Ok(rolagensSala);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest();
        }
    }
}
