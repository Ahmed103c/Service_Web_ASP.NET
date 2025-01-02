using System.Text.Json;
using PlateformeFilm.Models;


namespace PlateformeFilm.Services
{

    public class Omdbservice
    {
        private readonly HttpClient _httpClient;
        private const string apiKey ="4ef767cd"; //my key délivré par Omdb
        private const string baseUrl="https://www.omdbapi.com/";
        public Omdbservice(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }




        public async Task<List<OmdbFilm>> SearchByTitle(string title)
        {
            var url=$"{baseUrl}?apikey={apiKey}&s={title}";
            var response = await _httpClient.GetAsync(url);
            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erreur lors de la rcherche du Film .");
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var result = JsonSerializer.Deserialize<OmdbSearchResponse>(content);

            return result?.Search ?? new List<OmdbFilm>();
        }



    }


}