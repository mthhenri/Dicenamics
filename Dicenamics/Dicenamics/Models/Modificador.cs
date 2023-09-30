
namespace Dicenamics.Models;
public class Modificador
{
    public int ModificadorId { get; set; }
    public string? Nome { get; set; }
    public int Valor { get; set; }

    public Modificador()
    {
        // Construtor vazio
    }
}
