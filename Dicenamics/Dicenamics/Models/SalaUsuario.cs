using System.Text.Json.Serialization;

namespace Dicenamics.Models;
public class SalaUsuario
{
    [JsonIgnore]
    public int SalaUsuarioId { get; set; }
    [JsonIgnore]
    public int SalaId { get; set; }
    [JsonIgnore]
    public Sala Sala { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}
