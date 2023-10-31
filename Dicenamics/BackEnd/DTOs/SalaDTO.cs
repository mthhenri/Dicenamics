using BackEnd.Models;

namespace BackEnd.DTOs;
public class SalaDTO
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public int UsuarioMestreId { get; set; }
    public List<int>? ConvidadosId { get; set; }
    public List<int> DadosSimplesSalaId { get; set; }
    public List<int> DadosCompostosSalaId { get; set; }
}
