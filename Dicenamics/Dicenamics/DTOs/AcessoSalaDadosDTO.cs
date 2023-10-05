using Dicenamics.Models;

namespace Dicenamics.DTOs;

public class AcessoSalaDadosDTO
{
    public int SalaId { get; set; }
    public List<int> DadosSimplesSala { get; set; }
    public List<int> DadosCompostosSala { get; set; }
}