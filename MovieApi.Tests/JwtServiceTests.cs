using Microsoft.Extensions.Configuration;
using MovieApi.Models;
using MovieApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Tests
{
    public class JwtServiceTests
    {
        [Fact]
        public void GenerateToken_Should_Return_Valid_Token() {

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "supersecretkeysupersecretkey1234" },
                    { "Jwt:Issuer", "testIssuer" },
                    { "Jwt:Audience", "testAudience" },
                    { "Jwt:DurationInMinutes", "60" }
                }).Build();

            var jwtService = new JwtService(config);

            var user = new User
            {
                Id = "1",
                UserName = "testuser"
            };

            var token = jwtService.GenerateToken(user);

            Assert.False(string.IsNullOrEmpty(token));
        }

    }
}
