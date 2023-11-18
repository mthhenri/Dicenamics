namespace BackEnd.DTOs;
public class DadoCompostoDTO
{
    public string? Nome { get; set; }
    public int Faces { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }
    public List<int>? FixosId { get; set; }
    public List<int>? VariaveisId { get; set; }
}
