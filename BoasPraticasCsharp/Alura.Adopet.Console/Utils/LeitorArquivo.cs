using Alura.Adopet.Console.Modelos;

namespace Alura.Adopet.Console.Utils
{
    internal class LeitorArquivo
    {
        public List<Pet> RealizaLeitura(string caminhoArquivo)
        {
            List<Pet> listPet = new();

            using (StreamReader sr = new(caminhoArquivo))
            {
                while (!sr.EndOfStream)
                {
                    // separa linha usando ponto e vírgula
                    string[] propriedades = sr.ReadLine()!.Split(';');
                    // cria objeto Pet a partir da separação
                    Pet pet = new(Guid.Parse(propriedades[0]),
                    propriedades[1],
                    TipoPet.Cachorro
                    );

                    listPet.Add(pet);
                }
            }

            return listPet;
        }
    }
}
