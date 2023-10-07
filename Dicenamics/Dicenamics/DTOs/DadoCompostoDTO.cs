namespace Dicenamics.DTOs;
public class DadoCompostoDTO
{
    public string? Nome { get; set; }
    public int Faces { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }
    public List<int>? Fixos { get; set; }
    public List<int>? Variaveis { get; set; }
}
