using BackEnd.Models;

namespace BackEnd.DTO;

public class DadoCompostoSalaDTO
{
    public bool AcessoPrivado { get; set; } 
    public string CriadorUsername { get; set; }
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }
    public List<int>? FixosId { get; set; }
    public List<int>? VariaveisId { get; set; }
    public DadoCompostoSalaDTO()
    {
        // Construtor vazio
    }
    
}