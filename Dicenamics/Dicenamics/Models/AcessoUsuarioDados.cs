using System.ComponentModel.DataAnnotations;

namespace Dicenamics.Models;

public class AcessoUsuarioDados
{
    [Key]
    public int AcessoUsuarioId { get; set; }   
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public List<DadoSimples> DadosSimples { get; set; }
    public List<DadoComposto> DadosCompostos { get; set; }
}