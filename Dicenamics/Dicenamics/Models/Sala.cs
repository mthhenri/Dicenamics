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
    public List<DadoSimples> DadosSimplesSala { get; set; }
    public List<DadoComposto> DadosCompostosSala { get; set; }

        
    public Sala()
    {
        
    }

    public void GerarIdLink()
    {
        Guid guid = new();
        IdLink = guid.ToString();
    }

    public Boolean CompararIdLink(Guid linkEnviado)
    {
        return string.Equals(IdLink, linkEnviado.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public void GerarIdSimples()
    {
        Random random = new();
        IdSimples = random.Next(100000, 1000000);
    }

    public Boolean CompararIdSimples(int sixEnviado)
    {
        return IdSimples == sixEnviado;
    }
}