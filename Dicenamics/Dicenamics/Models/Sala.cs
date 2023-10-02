namespace Dicenamics.Models;
public class Sala
{
    public int SalaId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public int IdSimples { get; set; }
    public string IdLink { get; set; }
    public int UsuarioMestreId { get; set; }
    public Usuario UsuarioMestre { get; set; }
    public List<Usuario>? Convidados{ get; set; }
    public List<DadoBasico>? DadosCriados { get; set; }

    
public Sala()
{
    Random random = new();
    Guid guid = new();
    IdSimples = random.Next(111111, 999999);
    IdLink = guid.ToString();
}
}