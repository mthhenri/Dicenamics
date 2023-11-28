namespace BackEnd.Models;
public class Sala
{
    public int SalaId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public int IdSimples { get; set; }
    public string IdLink { get; set; }
    public int UsuarioMestreId { get; set; }
    public Usuario UsuarioMestre { get; set; }
    public List<SalaUsuario>? Convidados{ get; set; }
    public List<DadoSimplesSala> DadosSimplesSala { get; set; }
    public List<DadoCompostoSala> DadosCompostosSala { get; set; }

        
    public Sala()
    {
        GerarIdLink();
        GerarIdSimples();
    }

    public void GerarIdLink()
    {
        Guid guid = Guid.NewGuid();
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