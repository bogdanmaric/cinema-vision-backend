using Microsoft.AspNetCore.Mvc;
using MovieApi.DTOs;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly OmdbService _omdbService;

        public MoviesController(IConfiguration config, HttpClient httpClient, OmdbService omdbService)
        {
            _omdbService = omdbService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string title)
        {
            if (title.Length < 3)
            {
                return BadRequest(new { message = "Title required minimum 3 characters" });
            }

            var movies = await _omdbService.SearchMoviesAsync(title);

            if (movies.Count == 0)
            {
                return Ok(new List<MovieDto>());
            }

            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetDetails(string movieId)
        {
            var movie = await _omdbService.GetMovieDetailsAsync(movieId);

            if (movie == null)
            {
                return NotFound(new ApiError
                {
                    Message = "Movie not found",
                    Code = "NOT_FOUND"
                });
            }

            return Ok(movie);
        }

    }
}
