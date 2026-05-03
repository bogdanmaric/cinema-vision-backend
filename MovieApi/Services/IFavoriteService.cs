using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IFavoriteService
    {
        Task<List<FavoriteMovie>> GetFavorites(string userId);
        Task<bool> AddFavorite(string userId, AddFavoriteDto dto);
        Task<bool> DeleteFavorite(string userId, int movieId);
    }
}
