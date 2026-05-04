using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Services;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Tests
{
    public class FavoriteServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddFavorite_Should_Add_When_Not_Exists()
        {
            var context = GetDbContext();
            var logger = new Mock<ILogger<FavoriteService>>();
            var service = new FavoriteService(context, logger.Object);

            var userId = "user1";
            var dto = new AddFavoriteDto
            {
                MovieId = "tt123",
                Title = "Batman",
                Year = "2005",
                PosterUrl = "url",

            };

            var result = await service.AddFavorite(userId, dto);

            Assert.True(result);
            Assert.Equal(1, context.FavoriteMovies.Count());
        }

        [Fact]
        public async Task AddFavorite_Should_Return_False_When_Duplicate()
        {
            var context = GetDbContext();
            var logger = new Mock<ILogger<FavoriteService>>();
            var service = new FavoriteService(context, logger.Object);

            var userId = "user1";
            var dto = new AddFavoriteDto
            {
                MovieId = "tt123",
                Title = "Batman",
                Year = "2005",
                PosterUrl = "url",
            };

            await service.AddFavorite(userId, dto);
            var result = await service.AddFavorite(userId, dto);

            Assert.False(result);
            Assert.Equal(1, context.FavoriteMovies.Count());
        }

        [Fact]
        public async Task DeleteFavorite_Should_Remove_When_Exists()
        {
            var context = GetDbContext();
            var logger = new Mock<ILogger<FavoriteService>>();
            var service = new FavoriteService(context, logger.Object);

            var userId = "user1";
            var favorite = new FavoriteMovie
            {
                UserId = userId,
                MovieId = "tt123",
                Title = "Batman",
                Year = "2005",
                PosterUrl = "url",
            };

            context.FavoriteMovies.Add(favorite);
            await context.SaveChangesAsync();

            var result = await service.DeleteFavorite(userId, favorite.Id);

            Assert.True(result);
            Assert.Empty(context.FavoriteMovies);
        }
    }
}