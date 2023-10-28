using System.Net.Http.Headers;

namespace Alura.Adopet.Console.Services
{
    internal class HttpClientPet
    {
        public readonly HttpClient client;

        public HttpClientPet()
        {

            client = ConfiguraHttpClient("http://localhost:5057");
        }

        private HttpClient ConfiguraHttpClient(string url)
        {
            HttpClient _client = new ();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.BaseAddress = new Uri(url);
            return _client;
        }
    }
}
