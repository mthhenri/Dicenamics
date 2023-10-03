namespace Dicenamics.DTOs;
public class UsuarioDTO
{
    public string Username { get; set; }
    public string? Nickname { get; set; }
    public string Senha { get; set; }
    public List<int> DadosPessoaisIds { get; set; }
}
