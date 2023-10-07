using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dicenamics.Models;
public class DadoCompostoModVar
{
    [Key]
    public int ConectDadoVarId { get; set; }
    public int DadoId { get; set; }
    [JsonIgnore]
    public DadoComposto DadoComposto { get; set; }
    public int ModificadorId { get; set; }
    public ModificadorVariavel ModificadorVariavel { get; set; }
}
