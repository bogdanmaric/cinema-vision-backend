using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FavoritesController> _logger;

        public FavoritesController(AppDbContext context, ILogger<FavoritesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            _logger.LogInformation("Getting favorites movies for user {UserId}", userId);

            var favorites = await _context.FavoriteMovies
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(AddFavoriteDto dto)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            _logger.LogInformation("Adding favorite movie {MovieId} for user {UserId}", dto.MovieId, userId);

            if (userId == null)
            {
                return Unauthorized();
            }

            var favorite = new FavoriteMovie
            {
                UserId = userId,
                MovieId = dto.MovieId,
                Title = dto.Title,
                Year = dto.Year,
                PosterUrl = dto.PosterUrl
            };

            var exists = await _context.FavoriteMovies
                .AnyAsync(f => f.UserId == userId && f.MovieId == dto.MovieId);

            if (exists)
            {
                _logger.LogWarning("Duplicate favorite attempt: user {UserId} already has movie {MovieId} in favorites", userId, dto.MovieId);
                return BadRequest("Movie already in favorites");
            }

            try
            {
                _context.FavoriteMovies.Add(favorite);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) 
            {
                _logger.LogError(ex, "Error adding favorite movie {MovieId} for user {UserId}", dto.MovieId, userId);

                if (ex.InnerException?.Message.Contains("duplicate") == true)
                {
                    return BadRequest("Movie already in favorites");
                }
                return StatusCode(500, "An error occurred while adding favorite movie");
            }

            return Ok(favorite);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            _logger.LogInformation("User {UserId} deleting favorite movie {FavoriteId}", userId, id);

            var favorite = await _context.FavoriteMovies
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (favorite == null)
            {
                _logger.LogWarning("Favorite movie {FavoriteId} not found for user {UserId}", id, userId);
                return NotFound();
            }

            _context.FavoriteMovies.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
