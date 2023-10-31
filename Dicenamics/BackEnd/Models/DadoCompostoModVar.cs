using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models;
public class DadoCompostoModVar
{
    [Key]
    [JsonIgnore]
    public int ConectDadoVarId { get; set; }
    [JsonIgnore]
    public int DadoId { get; set; }
    [JsonIgnore]
    public DadoComposto DadoComposto { get; set; }
    [JsonIgnore]
    public int ModificadorId { get; set; }
    public ModificadorVariavel ModificadorVariavel { get; set; }
}
