using System.Net.Http.Json;
using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Services;

namespace Alura.Adopet.Console.Comandos
{
    [DocComando("list", "adopet list comando que exibe no terminal o conteúdo que será importado.")]
    internal class Listar : IComando
    {
        public async Task ExecutarAsync(string[] args)
        {
            await ListarPetAsync();
        }

        private async Task ListarPetAsync()
        {
            var pets = await ListPetsAsync();
            if (pets != null)
            {
                foreach (var pet in pets)
                {
                    System.Console.WriteLine(pet);
                }
            }

        }


        private async Task<IEnumerable<Pet>?> ListPetsAsync()
        {
            HttpClientPet meu = new();
            HttpResponseMessage response = await meu.client.GetAsync("pet/list");
            return await response.Content.ReadFromJsonAsync<IEnumerable<Pet>>();
        }


    }
}
