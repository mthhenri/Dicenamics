using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models;
public class DadoCompostoSalaModVar
{
    [Key]
    [JsonIgnore]
    public int ConectDadoVarId { get; set; }
    [JsonIgnore]
    public int DadoId { get; set; }
    [JsonIgnore]
    public DadoCompostoSala DadoCompostoSala { get; set; }
    public int ModificadorId { get; set; }
    public ModificadorVariavel ModificadorVariavel { get; set; }
}