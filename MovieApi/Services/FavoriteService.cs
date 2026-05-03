using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FavoriteService> _logger;
        public FavoriteService(AppDbContext context, ILogger<FavoriteService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<FavoriteMovie>> GetFavorites(string userId)
        {
            _logger.LogInformation("Getting favorites for user {UserId}", userId);

            return await _context.FavoriteMovies
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
        public async Task<bool> AddFavorite(string userId, AddFavoriteDto dto)
        {
            _logger.LogInformation("Adding favorite movie {MovieId} for user {UserId}", dto.MovieId, userId);

            var exists = await _context.FavoriteMovies
                .AnyAsync(f => f.UserId == userId && f.MovieId == dto.MovieId);

            if (exists)
            {
                return false;
            }

            var favorite = new FavoriteMovie
            {
                UserId = userId,
                MovieId = dto.MovieId,
                Title = dto.Title,
                Year = dto.Year,
                PosterUrl = dto.PosterUrl,
            };

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
                    return false;
                }

                throw;
            }

            return true;
        }
        public async Task<bool> DeleteFavorite(string userId, int movieId)
        {
            _logger.LogInformation("User {UserId} deleting favorite {FavoriteId}", userId, movieId);

            var favorite = await _context.FavoriteMovies
                .FirstOrDefaultAsync(f => f.UserId == userId && f.Id == movieId);

            if (favorite == null)
            {
                return false;
            }

            _context.FavoriteMovies.Remove(favorite);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
