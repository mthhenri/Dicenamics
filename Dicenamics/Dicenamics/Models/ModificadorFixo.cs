
using System.Text.Json.Serialization;

namespace Dicenamics.Models;
public class ModificadorFixo
{
    public int ModificadorFixoId { get; set; }
    public string? Nome { get; set; }
    public int Valor { get; set; }
    [JsonIgnore]
    public List<DadoCompostoModFixo> Fixos { get; set; }

    public ModificadorFixo()
    {
        // Construtor vazio
    }
}
