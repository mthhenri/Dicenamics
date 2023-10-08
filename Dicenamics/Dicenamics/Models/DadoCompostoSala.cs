namespace Dicenamics.Models;

public class DadoCompostoSala
{
    public int DadoCompostoSalaId { get; set; }  
    public bool AcessoPrivado { get; set; } 
    public Usuario Criador { get; set; }
    public int CriadorId { get; set; }
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }
    public List<DadoCompostoModFixo>? Fixos { get; set; }
    public List<DadoCompostoModVar>? Variaveis { get; set; }

    public List<List<int>> RolarDado()
    {
        return null;
    }
}