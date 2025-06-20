using System;
using System.Linq;

class Program
{
    static readonly Random random = new Random();
    
    static void Main(string[] args)
    {
        // Configurações
        const int totalMinimoAtributos = 70;
        const int numeroAtributos = 6;
        string[] nomesAtributos = { "Força", "Destreza", "Constituição", 
                                   "Inteligência", "Sabedoria", "Carisma" };

        while (true) // Loop principal
        {
            // Geração dos atributos
            int[] atributos;
            do
            {
                atributos = new int[numeroAtributos];
                Console.WriteLine("\nRolagens (método 4d6 drop lowest):");
                for (int i = 0; i < numeroAtributos; i++)
                {
                    atributos[i] = RolarAtributo(out int somaMantida);
                    Console.WriteLine($"Atributo {nomesAtributos[i]}: {somaMantida}");
                }
            } while (atributos.Sum() < totalMinimoAtributos);

            // Exibição dos resultados
            ExibirAtributos(nomesAtributos, atributos);
            ExibirResumo(atributos);

            // Pergunta ao usuário se deseja continuar ou sair
            Console.WriteLine("\nPressione Enter para gerar novos atributos ou 'X' para sair.");
            var input = Console.ReadKey(true).Key; // Lê a tecla pressionada
            if (input == ConsoleKey.X) // Verifica se a tecla 'X' foi pressionada
            {
                break; // Sai do loop
            }
        }
    }

    static int RolarAtributo(out int somaMantida)
    {
        int[] dados = new int[4];
        for (int i = 0; i < 4; i++)
        {
            dados[i] = random.Next(1, 7);
        }

        Array.Sort(dados);
        int descartado = dados[0]; // menor valor
        somaMantida = dados[1] + dados[2] + dados[3]; // soma dos 3 maiores

        Console.WriteLine($"{string.Join(", ", dados)} - descartado o valor {descartado}");
        return somaMantida;
    }

    static int CalcularModificador(int atributo)
    {
        return (atributo - 10) / 2;
    }

    static void ExibirAtributos(string[] nomes, int[] valores)
    {
        Console.WriteLine("\nAtributos do Personagem:");
        Console.WriteLine("-----------------------");

        for (int i = 0; i < valores.Length; i++)
        {
            int modificador = CalcularModificador(valores[i]);
            Console.WriteLine($"{nomes[i],-12}: {valores[i],2} (Modificador: {modificador.ToString("+0;-#")})");
        }
    }

    static void ExibirResumo(int[] atributos)
    {
        Console.WriteLine("\nResumo:");
        Console.WriteLine($"Maior atributo: {atributos.Max()}");
        Console.WriteLine($"Menor atributo: {atributos.Min()}");
        Console.WriteLine($"Total de pontos: {atributos.Sum()}");
    }
}
