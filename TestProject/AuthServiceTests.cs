using NUnit.Framework;
using ApplicationLayer.Services;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using MediaFlix.Tests.Fakes;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaFlix.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;
        private FakeUserRepository _fakeRepo;

        [SetUp]
        public void Setup()
        {
            _fakeRepo = new FakeUserRepository();

            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "TestOnly_NotARealSecret123456789!"},
                {"Jwt:Issuer", "MediaFlixAPI"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _authService = new AuthService(_fakeRepo, configuration);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnToken_WhenNewUserRegistered()
        {
            //Arrange
            var registerDto = new RegisterUserDto
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "password123"
            };

            //act
            var token = await _authService.RegisterAsync(registerDto);

            //assert
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnNull_WhenEmailAlreadyExists()
        {
            // Arrange: lägg till en användare i fake-repot
            var existingUser = new User
            {
                Username = "existinguser",
                Email = "test@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
            };
            await _fakeRepo.CreateAsync(existingUser);

            // Försök registrera med samma e-post
            var registerDto = new RegisterUserDto
            {
                Username = "newuser",
                Email = "test@example.com", // samma e-post som ovan!
                Password = "newpassword123"
            };

            // Act
            var result = await _authService.RegisterAsync(registerDto);

            // Assert
            Assert.IsNull(result);
        }


    }
}
