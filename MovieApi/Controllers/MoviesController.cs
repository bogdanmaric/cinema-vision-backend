using Microsoft.AspNetCore.Mvc;
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
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title is required");
            }

            var movies = await _omdbService.SearchMoviesAsync(title);

            if (movies.Count == 0)
            {
                return NotFound($"No movies found with title: {title}");
            }

            return Ok(movies);
        }

    }
}
