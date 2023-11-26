using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;
[Table("RolagensDadosSalas")]
public class RolagemDadoSala
{
    [Key]
    public int? RolagemDadoSalaId { get; set; }
    public DateTime RoladoEm { get; set; }
    public string UsuarioUsername { get; set; }
    public string Resultados { get; set; }
    public string TipoRolagem { get; set; }
    [JsonIgnore]
    public int? DadoId { get; set; }
    public DadoCompostoSala DadoComposto { get; set; }
    [JsonIgnore]
    public int? SalaId { get; set; }
    public Sala Sala { get; set; }

    public RolagemDadoSala()
    {
        RoladoEm = DateTime.Now;
    }

}
