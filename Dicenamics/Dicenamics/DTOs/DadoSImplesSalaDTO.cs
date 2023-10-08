namespace Dicenamics.DTO;

public class DadoSimplesSalaDTO
{
    public bool AcessoPrivado { get; set; } 
    public string? CriadorUsername { get; set; }
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }

    public DadoSimplesSalaDTO()
    {
        // Construtor vazio
    }
    
}