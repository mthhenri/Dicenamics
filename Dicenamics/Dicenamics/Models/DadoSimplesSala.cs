namespace Dicenamics.Models;

public class DadoSimplesSala : DadoSimples
{
    public int DadoSimplesSalaId { get; set; }
    public Boolean AcessoPrivado { get; set; } 
    public Usuario Criador { get; set; }
    public int CriadorId { get; set; }   
}