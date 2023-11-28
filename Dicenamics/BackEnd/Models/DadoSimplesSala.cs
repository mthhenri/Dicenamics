namespace BackEnd.Models;

public class DadoSimplesSala
{
    public int DadoSimplesSalaId { get; set; }
    public bool AcessoPrivado { get; set; } 
    public Usuario Criador { get; set; }
    public int CriadorId { get; set; }  
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }

    public List<int> RolarDado()
    {
        List<int> resultados = new();
        Random random = new();
        //Usado para a criação da base desse método https://chat.openai.com/share/caa70bd7-58ee-4616-9768-a3330c09732d
        for (int i = 0; i < Quantidade; i++)
        {
            int resultadoLancamento = random.Next(1, Faces + 1);
            resultados.Add(resultadoLancamento);
        }
        return resultados;
    }
}