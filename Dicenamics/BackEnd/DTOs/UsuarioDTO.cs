namespace BackEnd.DTOs;
public class UsuarioDTO
{
    public string Username { get; set; }
    public string? Nickname { get; set; }
    public string Senha { get; set; }
    public List<int> DadosSimplesPessoaisIds { get; set; }
    public List<int> DadosCompostosPessoaisIds { get; set; }
}
