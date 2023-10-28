using Alura.Adopet.Console.Utils;

namespace Alura.Adopet.Console.Comandos
{
    [DocComando("show",
        "adopet show   <arquivo> comando que exibe no terminal o conteúdo do arquivo importado.")]
    internal class Show : IComando
    {
        public Show()
        {

        }

        public Task ExecutarAsync(string[] args)
        {
            ExibirArquivo(args[1]);

            return Task.CompletedTask;
        }

        private void ExibirArquivo(string caminhoArquivoASerExibido)
        {
            var listaPet = new LeitorArquivo().RealizaLeitura(caminhoArquivoASerExibido);
            foreach (var pet in listaPet)
            {
                System.Console.WriteLine(pet);
            }
        }
    }
}
