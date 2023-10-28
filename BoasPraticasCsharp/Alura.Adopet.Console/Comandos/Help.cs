using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console.Comandos
{

    [DocComando("help", "Execute 'adopet.exe help [comando]' para obter mais informações sobre um comando.")]
    internal class Help : IComando
    {
        private readonly  Dictionary<string, DocComando> docs;

        public Help()
        {
            docs = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(t => t.GetCustomAttribute<DocComando>() != null)
                 .Select(t => t.GetCustomAttribute<DocComando>()!)
                 .ToDictionary(d => d.Instrucao);
        }

        public Task ExecutarAsync(string[] args)
        {
            ShowHelp(args);

            return Task.CompletedTask;

        }

        private void ShowHelp(string[] help)
        {
            System.Console.WriteLine("Lista de comandos.");
            // se não passou mais nenhum argumento mostra help de todos os comandos
            if (help.Length == 1)
            {
                System.Console.WriteLine("adopet help <parametro> ous simplemente adopet help  " +
                     "comando que exibe informações de ajuda dos comandos.");
                System.Console.WriteLine("Adopet (1.0) - Aplicativo de linha de comando (CLI).");
                System.Console.WriteLine("Realiza a importação em lote de um arquivos de pets.");
                System.Console.WriteLine("Comando possíveis: ");
                foreach (var doc in docs.Values)
                {
                    System.Console.WriteLine(doc.Documentacao);
                }

            }
            else if (help.Length == 2)
            {
                string comandoASerExibido = help[1].Trim();
                if (docs.ContainsKey(comandoASerExibido))
                {
                    var comando = docs[comandoASerExibido];

                    System.Console.WriteLine(comando.Documentacao);
                }
                else
                {
                    System.Console.WriteLine("Comando desconhecido, verifique!");
                }

                
            }
        }
    }
}
