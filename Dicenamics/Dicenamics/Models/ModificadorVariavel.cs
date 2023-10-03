namespace Dicenamics.Models;
public class ModificadorVariavel
{
    public int ModificadorVariavelId { get; set; }
    public string? Nome { get; set; }
    public DadoSimples? Dado { get; set; }

    public ModificadorVariavel()
    {
        // Construtor vazio
    }
}
