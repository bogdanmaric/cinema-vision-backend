using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
