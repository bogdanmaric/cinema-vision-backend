using Microsoft.AspNetCore.Mvc;
using MovieApi.DTOs;
using MovieApi.Models;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieApi.Services
{
    public class OmdbService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public OmdbService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        [HttpGet("search")]
        public async Task<List<MovieDto>> SearchMoviesAsync(string title)
        {

            var apiKey = _config["Omdb:ApiKey"];
            var url = $"http://www.omdbapi.com/?apikey={apiKey}&s={title}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<MovieDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var omdbResponse = JsonSerializer.Deserialize<OmdbResponse>(content);

            return omdbResponse.Search.Select(m => new MovieDto
            {
                MovieId = m.ImdbID,
                Title = m.Title,
                Year = m.Year,
                PosterUrl = m.Poster
            }).ToList();
        }
    }
}
