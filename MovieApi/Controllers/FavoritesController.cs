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

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var favorites = await _context.FavoriteMovies
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(AddFavoriteDto dto)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                return Unauthorized();

            var favorite = new FavoriteMovie
            {
                UserId = userId,
                MovieId = dto.MovieId,
                Title = dto.Title,
                PosterUrl = dto.PosterUrl
            };

            _context.FavoriteMovies.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(favorite);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var favorite = await _context.FavoriteMovies
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (favorite == null)
                return NotFound();

            _context.FavoriteMovies.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
