using System.Text.RegularExpressions;

namespace Dicenamics.Models;
public class DadoSimples
{
    public int DadoSimplesId { get; set; }
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }

    public DadoSimples()
    {
        // Construtor vazio
    }

    public List<List<int>> RolarDado()
    {
        List<int> resultados = new();
        List<int> escolhidos = new();
        List<List<int>> eResultados = new();
        Random random = new();
        //Usado para a criação da base desse método https://chat.openai.com/share/caa70bd7-58ee-4616-9768-a3330c09732d

        if(string.Equals(Condicao, "", StringComparison.OrdinalIgnoreCase) || Condicao == null)
        {
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            eResultados.Add(resultados);

            return eResultados;

        } else if (string.Equals(Condicao, "std", StringComparison.OrdinalIgnoreCase))
        {

            for (int i = 0; i < Quantidade; i++)
            {
            int resultadoLancamento = random.Next(1, Faces + 1);
            resultados.Add(resultadoLancamento);
            }
            int total = 0;
            foreach (var valor in resultados)
            {
                total += valor;
            }
            escolhidos.Add(total);
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            return eResultados;

        } else if(string.Equals(Condicao, "mrv", StringComparison.OrdinalIgnoreCase))
        {

            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            int maiorV = 0;
            foreach (var valor in resultados)
            {
                if(valor > maiorV)
                {
                    maiorV = valor;
                }
            }
            escolhidos.Add(maiorV);
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            return eResultados;

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
            escolhidos.Add(menorV);
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            return eResultados;

        } else if(string.Equals(Condicao[..3], "acd", StringComparison.OrdinalIgnoreCase))
        {

            var vDito = Condicao[3..];
            int valorDito = int.Parse(vDito);
            
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            foreach (var valor in resultados)
            {
                if(valor >= valorDito)
                {
                    escolhidos.Add(valor);
                }
            }
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            return eResultados;

        } else if(string.Equals(Condicao[..3], "abd", StringComparison.OrdinalIgnoreCase))
        {

            var vDito = Condicao[3..];
            int valorDito = int.Parse(vDito);
            
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            foreach (var valor in resultados)
            {
                if(valor <= valorDito)
                {
                    escolhidos.Add(valor);
                }
            }
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            return eResultados;

        } else if(string.Equals(Condicao[..3], "ved", StringComparison.OrdinalIgnoreCase))
        {

            var vDito = Condicao[3..];
            int valorDito = int.Parse(vDito);
            
            for (int i = 0; i < Quantidade; i++)
            {
                int resultadoLancamento = random.Next(1, Faces + 1);
                resultados.Add(resultadoLancamento);
            }
            foreach (var valor in resultados)
            {
                if(valor == valorDito)
                {
                    escolhidos.Add(valor);
                }
            }
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            return eResultados;

        } else if(string.Equals(Condicao[..2], "de", StringComparison.OrdinalIgnoreCase))
        {

            Match match = Regex.Match(Condicao, @"de(\d+)a(\d+)", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                int valorMinimo = int.Parse(match.Groups[1].Value);
                int valorMaximo = int.Parse(match.Groups[2].Value);

                for (int i = 0; i < Quantidade; i++)
                {
                    int resultadoLancamento = random.Next(1, Faces + 1);
                    resultados.Add(resultadoLancamento);
                }
                foreach (var valor in resultados)
                {
                    if(valor <= valorMaximo && valor >= valorMinimo)
                    {
                        escolhidos.Add(valor);
                    }
                }
                eResultados.Add(resultados);
                eResultados.Add(escolhidos);
            }
            return eResultados;
        } else {
            return eResultados;
        }
    }

}
