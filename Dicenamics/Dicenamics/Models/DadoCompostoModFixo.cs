using System.Text.Json.Serialization;

namespace Dicenamics.Models;
public class DadoCompostoModFixo
{
    [JsonIgnore]
    public int DadoId { get; set; }
    [JsonIgnore]
    public DadoComposto DadoComposto { get; set; }
    public int ModificadorId { get; set; }
    public ModificadorFixo ModificadorFixo { get; set; }
}
