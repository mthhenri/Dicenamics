using Dicenamics.Models;

namespace Dicenamics.DTOs;
public class SalaDTO
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public int UsuarioMestreId { get; set; }
    public List<Usuario>? Convidados{ get; set; }
    public List<DadoBasico>? DadosCriados { get; set; }
}
