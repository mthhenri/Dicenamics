using System.Text.Json.Serialization;

namespace BackEnd.Models;

    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Username { get; set; }
        public string? Nickname { get; set; }
        public string? Senha { get; set; }
        public List<DadoSimples>? DadosSimplesPessoais { get; set; }
        public List<DadoComposto>? DadosCompostosPessoais { get; set; }
        [JsonIgnore]
        public List<SalaUsuario>? Salas { get; set; }
        
        public bool CompararUsername(string outroUsername)
        {
            return string.Equals(Username, outroUsername, StringComparison.OrdinalIgnoreCase);
        }

        public bool VerificarSenha(string senhaDigitada)
        {
            return string.Equals(Senha, senhaDigitada);
        }
    }
