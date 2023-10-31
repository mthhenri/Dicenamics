
using System.Text.Json.Serialization;

namespace BackEnd.Models;
public class ModificadorFixo
{
    public int ModificadorFixoId { get; set; }
    public string? Nome { get; set; }
    public int Valor { get; set; }
    [JsonIgnore]
    public List<DadoCompostoModFixo> Fixos { get; set; }
    [JsonIgnore]
    public List<DadoCompostoSalaModFixo> FixosSala { get; set; }

    public ModificadorFixo()
    {
        // Construtor vazio
    }
}
