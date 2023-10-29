using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    [FirestoreData]
    public class Filme
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty(Name ="titulo")]
        [Required(ErrorMessage ="O título é obrigatório")]
        [MinLength(10,ErrorMessage ="O título deve conter no mínimo 10 caracteres")]
        public string? Titulo { get; set; }
        [FirestoreProperty(Name = "genero")]
        [Required(ErrorMessage = "O genero é obrigatório")]
        [MaxLength(50,ErrorMessage ="O tamanho do genero não pode exceder 50 caracteres")]
        public string? Genero { get; set; }
        [FirestoreProperty(Name = "duracao")]
        [Required(ErrorMessage ="A duração é obrigatória")]
        [Range(60,600, ErrorMessage ="A duração deve ser entre 60 e 600 minutos")]
        public int Duracao { get; set; }

        public Filme()
        {
            
        }

        public override string ToString()
        {
            return $"Id: \"{Id}\" \n" +
                $"Titulo: \"{Titulo}\"\n" +
                $"Genero: \"{Genero}\"\n" +
                $"Duracao: \"{Duracao}\"\n";
        }
    }
}
