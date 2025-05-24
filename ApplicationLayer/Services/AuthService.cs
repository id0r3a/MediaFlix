using ApplicationLayer.DTOs;
using ApplicationLayer.Helper;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        // Konstruktor som injicerar användar-repository och konfigurationsinställningar.
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
        }
        // Returnerar en JWT-token om registreringen lyckas, annars null (t.ex. vid redan registrerad e-post).
        public async Task<string?> RegisterAsync(RegisterUserDto dto)
        {
            // Kontrollera om användaren redan finns baserat på e-post
            var userExists = await _userRepository.GetByEmailAsync(dto.Email);
            if (userExists != null)
                return null;

            // Hasha lösenordet innan det sparas
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Skapa ett nytt användarobjekt
            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateAsync(newUser);

            // Generera och returnera JWT-token för den nya användaren
            return _jwtTokenGenerator.GenerateToken(newUser);
        }

        public async Task<string?> LoginAsync(LoginUserDto dto)
        {
            // Hämta användare baserat på e-post
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            // Om ingen användare hittades eller lösenordet är fel
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            // Generera och returnera JWT-token
            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}