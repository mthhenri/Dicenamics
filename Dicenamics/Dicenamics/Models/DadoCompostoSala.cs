namespace Dicenamics.Models;

public class DadoCompostoSala : DadoComposto
{
    public int DadoCompostoSalaId { get; set; }  
    public Boolean AcessoPrivado { get; set; } 
    public Usuario Criador { get; set; }
    public int CriadorId { get; set; }
}