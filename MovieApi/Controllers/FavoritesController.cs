using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;
using MovieApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var favorites = await _favoriteService.GetFavorites(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(AddFavoriteDto dto)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var success = await _favoriteService.AddFavorite(userId, dto);

            if (!success)
            {
                return BadRequest("Movie already in favorites");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var success = await _favoriteService.DeleteFavorite(userId, id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
