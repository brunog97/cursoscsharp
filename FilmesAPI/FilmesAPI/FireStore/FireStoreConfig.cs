using Google.Cloud.Firestore;

namespace FilmesAPI.FireStore
{
    public class FireStoreConfig
    {
        private readonly FirestoreDb _database;
        public FireStoreConfig()
        {
            _database = FirestoreDb.Create("decisive-cinema-403501");
        }

        public CollectionReference FilmeCollection(string collection)
        {
            try
            {
                return _database.Collection(collection);
            }
            catch
            {
                throw;
            }
        }
    }
}
