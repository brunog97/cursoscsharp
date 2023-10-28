using System.Net.Http.Json;
using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Services;
using Alura.Adopet.Console.Utils;

namespace Alura.Adopet.Console.Comandos
{
    [DocComando("import", "adopet import <arquivo> comando que realiza a importação do arquivo de pets.")]
    internal class Importacao : IComando
    {


        public Importacao()
        {
        }

        public async Task ExecutarAsync(string[] args)
        {
            await ImportacaoArquivoPetAsync(args[0]);
        }

        private static async Task ImportacaoArquivoPetAsync(string caminhoArquivoImportacao)
        {
            var listaPet = new LeitorArquivo().RealizaLeitura(caminhoArquivoImportacao);
            System.Console.WriteLine("----- Serão importados os dados abaixo -----");

            foreach (var pet in listaPet)
            {
                System.Console.WriteLine(pet);
                try
                {
                    var resposta = await CreatePetAsync(pet);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
            System.Console.WriteLine("Importação concluída!");

        }

        private static Task<HttpResponseMessage> CreatePetAsync(Pet pet)
        {
            HttpClientPet meuClient = new();
            using (_ = new HttpResponseMessage())
            {

                return meuClient.client.PostAsJsonAsync("pet/add", pet);
            }
        }

       
    }
}
