using FilmesAPI.FireStore;
using FilmesAPI.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FilmesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly static List<Filme> _filmes = new ();
        private readonly FireStoreConfig _firestoreDb;
        private readonly CollectionReference _collectionReference;

        public FilmeController(FireStoreConfig firestoreDb)
        {
            _firestoreDb = firestoreDb;
            _collectionReference = _firestoreDb.FilmeCollection("filmes");
        }

        

        [HttpPost]
        public async Task<IActionResult> AdicionaFilme([FromBody]Filme filme)
        {
            //_filmes.Add(filme);
            //Console.WriteLine(filme.ToString());
            DocumentReference docRef = _collectionReference.Document(filme.Id!.ToString());

            Dictionary<string, object> filmeG = new ()
            {
                {"titulo", filme.Titulo! },
                {"genero", filme.Genero! },
                {"duracao", filme.Duracao! }
            };

            await docRef.SetAsync(filmeG);

            return Ok(filme);   

        }

        [HttpGet]
        public async Task<IEnumerable<Filme>> ObtemFilmes()
        {
            Query allFilmesQuery = _collectionReference;
            QuerySnapshot allFIlmesSnapshot = await allFilmesQuery.GetSnapshotAsync();

            foreach(DocumentSnapshot documentSnapshot in allFIlmesSnapshot)
            {


                var filme = documentSnapshot.ConvertTo<Filme>();


                _filmes.Add(filme); 

            }

            return _filmes;
        }

        [HttpGet("{id}")]
        public Filme? ObtemFilme(int id)
        {
            Filme? filme = _filmes.Where(f => f.Id!.Equals(id)).FirstOrDefault();  
            return filme;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id )
        {

            DocumentReference docRef = _collectionReference.Document(id.ToString());
            await docRef.DeleteAsync();

            return NoContent();
        }

        /*
                     DocumentReference cityRef = db.Collection("cities").Document("new-city-id");
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "Capital", false }
            };
            await cityRef.UpdateAsync(updates);

            // You can also update a single field with: await cityRef.UpdateAsync("Capital", false);
         
         */
    }
}
