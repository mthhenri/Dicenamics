namespace Dicenamics.Models;
public abstract class DadoBasico
{
    public int Faces { get; set; }
    
    abstract public List<int> RolarDado();
}
