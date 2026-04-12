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

    public class OmdbMovieDetails
    {
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Year")]
        public string Year { get; set; }

        [JsonPropertyName("Genre")]
        public string Genre { get; set; }

        [JsonPropertyName("Director")]
        public string Director { get; set; }

        [JsonPropertyName("Plot")]
        public string Plot { get; set; }

        [JsonPropertyName("Poster")]
        public string Poster { get; set; }

        [JsonPropertyName("imdbID")]
        public string ImdbID { get; set; }
    }
}
