using Dicenamics.Models;

namespace Dicenamics.DTO;

public class DadoCompostoSalaDTO
{
    public bool AcessoPrivado { get; set; } 
    public string CriadorUsername { get; set; }
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }
    public List<int>? Fixos { get; set; }
    public List<int>? Variaveis { get; set; }
    public DadoCompostoSalaDTO()
    {
        // Construtor vazio
    }
    
}