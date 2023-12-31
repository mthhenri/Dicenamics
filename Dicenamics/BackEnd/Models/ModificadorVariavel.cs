using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models;
public class ModificadorVariavel
{
    public int ModificadorVariavelId { get; set; }
    public string? Nome { get; set; }
    public int DadoSimplesId { get; set; }
    public DadoSimples Dado { get; set; }
    [JsonIgnore]
    public List<DadoCompostoModVar>? Variaveis { get; set; }
    [JsonIgnore]
    public List<DadoCompostoSalaModVar>? VariaveisSala { get; set; }

    public ModificadorVariavel()
    {
        // Construtor vazio
    }
}
