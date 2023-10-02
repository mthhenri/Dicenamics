namespace Dicenamics.Models;
public class DadoSimples : DadoBasico
{
    public int DadoSimplesId { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }

    public DadoSimples()
    {
        // Construtor vazio
    }

    public override List<int> RolarDado()
    {
        List<int> resultados = new();
        Random random = new();
        //Usado para a criação da base desse método https://chat.openai.com/share/caa70bd7-58ee-4616-9768-a3330c09732d

        if(string.Equals(Condicao, "", StringComparison.OrdinalIgnoreCase) || Condicao == null)
        {
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }

            return resultados;

        } else if (string.Equals(Condicao, "std", StringComparison.OrdinalIgnoreCase))
        {

            for (int i = 0; i < Quantidade; i++)
            {
            int resultadoLancamento = random.Next(1, Faces + 1);
            resultados.Add(resultadoLancamento);
            }
            int total = 0;
            resultados.Add(total);
            foreach (var valor in resultados)
            {
                total += valor;
            }
            resultados.Add(total);
            return resultados;

        } else if(string.Equals(Condicao, "mrv", StringComparison.OrdinalIgnoreCase))
        {

            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            int maiorV = 0;
            resultados.Add(maiorV);
            foreach (var valor in resultados)
            {
                if(valor > maiorV)
                {
                    maiorV = valor;
                }
            }
            resultados.Add(maiorV);
            return resultados;

        } else if(string.Equals(Condicao, "mnv", StringComparison.OrdinalIgnoreCase))
        {

            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            int menorV = Faces + 1;
            foreach (var valor in resultados)
            {
                if(valor < menorV)
                {
                    menorV = valor;
                }
            }
            resultados.Add(0);
            resultados.Add(menorV);
            return resultados;

        } else if(string.Equals(Condicao[..3], "acd", StringComparison.OrdinalIgnoreCase))
        {

            var vDito = Condicao[3..];
            int valorDito = int.Parse(vDito);
            
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            List<int> escolhidos = new();
            foreach (var valor in resultados)
            {
                if(valor > valorDito)
                {
                    escolhidos.Add(valor);
                }
            }

            return escolhidos;

        } else if(string.Equals(Condicao[..3], "abd", StringComparison.OrdinalIgnoreCase))
        {

            var vDito = Condicao[3..];
            int valorDito = int.Parse(vDito);
            
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            List<int> escolhidos = new();
            foreach (var valor in resultados)
            {
                if(valor < valorDito)
                {
                    escolhidos.Add(valor);
                }
            }

            return escolhidos;

        } else if(string.Equals(Condicao[..3], "ved", StringComparison.OrdinalIgnoreCase))
        {

            var vDito = Condicao[3..];
            int valorDito = int.Parse(vDito);
            
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            List<int> escolhidos = new();
            foreach (var valor in resultados)
            {
                if(valor == valorDito)
                {
                    escolhidos.Add(valor);
                }
            }

            return escolhidos;

        } else {
            return resultados;
        }
    }

}
