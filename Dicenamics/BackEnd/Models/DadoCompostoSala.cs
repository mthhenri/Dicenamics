using System.Text.RegularExpressions;

namespace BackEnd.Models;

public class DadoCompostoSala
{
    public int DadoCompostoSalaId { get; set; }  
    public bool AcessoPrivado { get; set; } 
    public Usuario Criador { get; set; }
    public int CriadorId { get; set; }
    public int Faces { get; set; }
    public string? Nome { get; set; }
    public int Quantidade { get; set; }
    public string? Condicao { get; set; }
    public List<DadoCompostoSalaModFixo>? Fixos { get; set; }
    public List<DadoCompostoSalaModVar>? Variaveis { get; set; }

    public List<List<int>> RolarDado()
    {
        List<int> resultados = new();
        List<int> escolhidos = new();
        List<List<int>> dadosVar = new();
        List<int> tempFixos = new();
        List<int> tempVar = new();
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

            if(Fixos != null)
            {
                foreach (var valor in resultados)
                {
                    var novoValor = valor;
                    for (int i = 0; i < Fixos.Count; i++)
                    {
                        novoValor += Fixos[i].ModificadorFixo.Valor;
                    }               
                    tempFixos.Add(novoValor);
                }
            }
            
            if(Variaveis != null)
            {
                if (Fixos != null)
                {
                    List<int> dados = new();  // Rolar dados uma vez para todos os valores

                    foreach (var valor in tempFixos)
                    {
                        var novoValor = valor;

                        // Rolar dados variáveis apenas uma vez
                        if (Variaveis != null && dados.Count == 0)
                        {
                            for (int i = 0; i < Variaveis.Count; i++)
                            {
                                dados.AddRange(Variaveis[i].ModificadorVariavel.Dado.RolarDado());
                            }
                            dadosVar.Add(dados);
                        }

                        // Somar os dados variáveis a cada novo valor
                        foreach (var valorDado in dados)
                        {
                            novoValor += valorDado;
                        }

                        tempVar.Add(novoValor);
                    }
                } else {
                    List<int> dados = new();  // Rolar dados uma vez para todos os valores

                    var novoValor = 0;

                    // Rolar dados variáveis apenas uma vez
                    if (Variaveis != null && dados.Count == 0)
                    {
                        for (int i = 0; i < Variaveis.Count; i++)
                        {
                            dados.AddRange(Variaveis[i].ModificadorVariavel.Dado.RolarDado());
                        }
                        dadosVar.Add(dados);
                    }

                    // Somar os dados variáveis a cada novo valor
                    foreach (var valorDado in dados)
                    {
                        novoValor += valorDado;
                    }

                    tempVar.Add(novoValor);
                }

                // if(Fixos != null){
                //     for (int i = 0; i < Variaveis.Count; i++)
                //     {
                //         var novoValor = tempFixos[i];
                //         List<int> dados = Variaveis[i].ModificadorVariavel.Dado.RolarDado();
                //         dadosVar.Add(dados);
                //         foreach (var valorDado in dados)
                //         {
                //             novoValor += valorDado;       
                //         }
                //         tempVar.Add(novoValor);
                //     }
                // } else {
                //     for (int i = 0; i < Variaveis.Count; i++)
                //     {
                //         var novoValor = 0;
                //         List<int> dados = Variaveis[i].ModificadorVariavel.Dado.RolarDado();
                //         dadosVar.Add(dados);
                //         foreach (var valorDado in dados)
                //         {
                //             novoValor += valorDado;       
                //         }
                //         tempVar.Add(novoValor);
                //     }
                // }

                // if(Fixos != null){
                //     foreach (var valor in tempFixos)
                //     {
                //         var novoValor = valor;
                //         for (int i = 0; i < Variaveis.Count; i++)
                //         {
                //             List<int> dados = Variaveis[i].ModificadorVariavel.Dado.RolarDado();
                //             dadosVar.Add(dados);
                //             foreach (var valorDado in dados)
                //             {
                //                 novoValor += valorDado;
                //             }
                //         }
                //         tempVar.Add(novoValor);
                //     }
                // } else {
                //     foreach (var valor in resultados)
                //     {
                //         var novoValor = valor;
                        
                //         tempVar.Add(novoValor);
                //     }
                // }
            }

            if(Fixos != null && Variaveis == null){
                escolhidos = tempFixos;
            } else if (Fixos == null && Variaveis != null) {
                escolhidos = tempVar;
            } else if (Fixos == null && Variaveis == null) {
                escolhidos = resultados;
            } else {
                escolhidos = tempVar;
            }

            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
            return eResultados;
        
        }else if (string.Equals(Condicao, "std", StringComparison.OrdinalIgnoreCase))
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

            if(Fixos != null)
            {
                foreach (var val in Fixos)
                {
                    total += val.ModificadorFixo.Valor;
                }
            }
            
            if(Variaveis != null)
            {
                for (int i = 0; i < Variaveis.Count; i++)
                {
                    List<int> dados = Variaveis[i].ModificadorVariavel.Dado.RolarDado();
                    dadosVar.Add(dados);
                    foreach (var valorDado in dados)
                    {
                        total += valorDado;       
                    }
                }
            }

            escolhidos.Add(total);
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
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

            if(Fixos != null)
            {
                foreach (var val in Fixos)
                {
                    maiorV += val.ModificadorFixo.Valor;
                }
            }
            
            if(Variaveis != null)
            {
                for (int i = 0; i < Variaveis.Count; i++)
                {
                    List<int> dados = Variaveis[i].ModificadorVariavel.Dado.RolarDado();
                    dadosVar.Add(dados);
                    foreach (var valorDado in dados)
                    {
                        maiorV += valorDado;       
                    }
                }
            }

            escolhidos.Add(maiorV);
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
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

            if(Fixos != null)
            {
                foreach (var val in Fixos)
                {
                    menorV += val.ModificadorFixo.Valor;
                }
            }
            
            if(Variaveis != null)
            {
                for (int i = 0; i < Variaveis.Count; i++)
                {
                    List<int> dados = Variaveis[i].ModificadorVariavel.Dado.RolarDado();
                    dadosVar.Add(dados);
                    foreach (var valorDado in dados)
                    {
                        menorV += valorDado;       
                    }
                }
            }
            
            escolhidos.Add(menorV);
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
            return eResultados;

        } else if(string.Equals(Condicao[..3], "acd", StringComparison.OrdinalIgnoreCase))
        {
            List<int> temp = new();
            List<int> temp2 = new();
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
                    temp.Add(valor);
                }
            }

            if(Fixos != null)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    for (int j = 0; j < Fixos.Count; j++)
                    {
                        temp2.Add(temp[i] += Fixos[j].ModificadorFixo.Valor);
                    }
                }
            }
            
            if(Variaveis != null)
            {
                if(Fixos != null){
                    for (int j = 0; j < Variaveis.Count; j++)
                    {
                        List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                        dadosVar.Add(dados);
                        for (int i = 0; i < temp2.Count; i++)
                        {
                            for (int k = 0; k < dados.Count; k++)
                            {
                                temp2[i] += dados[k];
                            }
                        }
                    }
                    
                } else {
                    for (int j = 0; j < Variaveis.Count; j++)
                    {
                        List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                        dadosVar.Add(dados);
                        for (int i = 0; i < resultados.Count; i++)
                        {
                            for (int k = 0; k < dados.Count; k++)
                            {
                                temp2.Add(resultados[i] += dados[k]);
                            }
                        }
                    }
                }
            }

            escolhidos = temp2;
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
            return eResultados;

        } else if(string.Equals(Condicao[..3], "abd", StringComparison.OrdinalIgnoreCase))
        {
            List<int> temp = new();
            List<int> temp2 = new();
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
                    temp.Add(valor);
                }
            }     

            if(Fixos != null)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    for (int j = 0; j < Fixos.Count; j++)
                    {
                        temp2.Add(temp[i] += Fixos[j].ModificadorFixo.Valor);
                    }
                }
            }
            
            if(Variaveis != null)
            {
                if(Fixos != null){
                    for (int j = 0; j < Variaveis.Count; j++)
                    {
                        List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                        dadosVar.Add(dados);
                        for (int i = 0; i < temp2.Count; i++)
                        {
                            for (int k = 0; k < dados.Count; k++)
                            {
                                temp2[i] += dados[k];
                            }
                        }
                    }
                    
                } else {
                    for (int j = 0; j < Variaveis.Count; j++)
                    {
                        List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                        dadosVar.Add(dados);
                        for (int i = 0; i < resultados.Count; i++)
                        {
                            for (int k = 0; k < dados.Count; k++)
                            {
                                temp2.Add(resultados[i] += dados[k]);
                            }
                        }
                    }
                }
            }

            escolhidos = temp2;
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
            return eResultados;

        } else if(string.Equals(Condicao[..3], "ved", StringComparison.OrdinalIgnoreCase))
        {
            List<int> temp = new();
            List<int> temp2 = new();
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
                    temp.Add(valor);
                }
            }     

            if(Fixos != null)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    for (int j = 0; j < Fixos.Count; j++)
                    {
                        temp2.Add(temp[i] += Fixos[j].ModificadorFixo.Valor);
                    }
                }
            }
            
            if(Variaveis != null)
            {
                if(Fixos != null){
                    for (int j = 0; j < Variaveis.Count; j++)
                    {
                        List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                        dadosVar.Add(dados);
                        for (int i = 0; i < temp2.Count; i++)
                        {
                            for (int k = 0; k < dados.Count; k++)
                            {
                                temp2[i] += dados[k];
                            }
                        }
                    }
                    
                } else {
                    for (int j = 0; j < Variaveis.Count; j++)
                    {
                        List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                        dadosVar.Add(dados);
                        for (int i = 0; i < resultados.Count; i++)
                        {
                            for (int k = 0; k < dados.Count; k++)
                            {
                                temp2.Add(resultados[i] += dados[k]);
                            }
                        }
                    }
                }
            }

            escolhidos = temp2;
            eResultados.Add(resultados);
            eResultados.Add(escolhidos);
            eResultados.AddRange(dadosVar);
            return eResultados;

        } else if(string.Equals(Condicao[..2], "de", StringComparison.OrdinalIgnoreCase))
        {

            Match match = Regex.Match(Condicao, @"de(\d+)a(\d+)", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                List<int> temp = new();
                List<int> temp2 = new();
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
                        temp.Add(valor);
                    }
                }     

                if(Fixos != null)
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        for (int j = 0; j < Fixos.Count; j++)
                        {
                            temp2.Add(temp[i] += Fixos[j].ModificadorFixo.Valor);
                        }
                    }
                }
                
                if(Variaveis != null)
                {
                    if(Fixos != null){
                        for (int j = 0; j < Variaveis.Count; j++)
                        {
                            List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                            dadosVar.Add(dados);
                            for (int i = 0; i < temp2.Count; i++)
                            {
                                for (int k = 0; k < dados.Count; k++)
                                {
                                    temp2[i] += dados[k];
                                }
                            }
                        }
                        
                    } else {
                        for (int j = 0; j < Variaveis.Count; j++)
                        {
                            List<int> dados = Variaveis[j].ModificadorVariavel.Dado.RolarDado();
                            dadosVar.Add(dados);
                            for (int i = 0; i < resultados.Count; i++)
                            {
                                for (int k = 0; k < dados.Count; k++)
                                {
                                    temp2.Add(resultados[i] += dados[k]);
                                }
                            }
                        }
                    }
                }

                escolhidos = temp2;
                eResultados.Add(resultados);
                eResultados.Add(escolhidos);
                eResultados.AddRange(dadosVar);
            }
            return eResultados;
        } else {
            return eResultados;
        }
    }
}