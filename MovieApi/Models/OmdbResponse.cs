using System.Text.Json.Serialization;

namespace MovieApi.Models
{
    public class OmdbResponse
    {
        [JsonPropertyName("Search")]
        public List<OmdbMovie> Search { get; set; }
    }

    public class OmdbMovie
    {
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("imdbID")]
        public string ImdbID { get; set; }
        [JsonPropertyName("Year")]
        public string Year { get; set; }
        [JsonPropertyName("Poster")]
        public string Poster { get; set; }
    }
}
