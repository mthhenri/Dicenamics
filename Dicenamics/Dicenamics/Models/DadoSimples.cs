namespace Dicenamics.Models;
public class DadoSimples
{
    public int DadoSimplesId { get; set; }
    public int Faces { get; set; }
    public int Quantidade { get; set; }

    public List<int> RolarDado()
    {
        //Usado para a criação desse método https://chat.openai.com/share/caa70bd7-58ee-4616-9768-a3330c09732d

        List<int> resultados = new();
        Random random = new();

        for (int i = 0; i < Quantidade; i++)
        {
            int resultadoLancamento = random.Next(1, Faces + 1);
            resultados.Add(resultadoLancamento);
        }

        return resultados;
    }
}
