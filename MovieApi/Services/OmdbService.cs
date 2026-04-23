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

        public async Task<List<MovieDto>> SearchMoviesAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length < 3)
            {
                return new List<MovieDto>();
            }

            var apiKey = _config["Omdb:ApiKey"];
            var url = $"http://www.omdbapi.com/?apikey={apiKey}&s={title}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<MovieDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var omdbResponse = JsonSerializer.Deserialize<OmdbResponse>(content);

            if (omdbResponse?.Search == null)
            {
                return new List<MovieDto>();
            }

            return omdbResponse.Search.Select(m => new MovieDto
            {
                MovieId = m.ImdbID,
                Title = m.Title,
                Year = m.Year,
                PosterUrl = m.Poster
            }).ToList();
        }

        public async Task<MovieDetailsDto?> GetMovieDetailsAsync(string movieId)
        {
            var apiKey = _config["Omdb:ApiKey"];
            var url = $"http://www.omdbapi.com/?apikey={apiKey}&i={movieId}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new MovieDetailsDto();
            }

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<OmdbMovieDetails>(content);

            return new MovieDetailsDto
            {
                MovieId = movie.ImdbID,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Director = movie.Director,
                Plot = movie.Plot,
                PosterUrl = movie.Poster
            };

        }
    }
}
