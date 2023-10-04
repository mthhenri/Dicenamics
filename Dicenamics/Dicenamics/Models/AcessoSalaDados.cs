using System.ComponentModel.DataAnnotations;

namespace Dicenamics.Models;

public class AcessoSalaDados
{
    [Key]
    public int AcessoSalaId { get; set; }   
    public int SalaId { get; set; }
    public Sala Sala { get; set; }
    public List<DadoSimplesSala> DadosSimplesSala { get; set; }
    public List<DadoCompostoSala> DadosCompostosSala { get; set; }
}