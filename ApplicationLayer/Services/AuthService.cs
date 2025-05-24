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

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
        }

        public async Task<string?> RegisterAsync(RegisterUserDto dto)
        {
            var userExists = await _userRepository.GetByEmailAsync(dto.Email);
            if (userExists != null)
                return null;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateAsync(newUser);
            return _jwtTokenGenerator.GenerateToken(newUser);
        }

        public async Task<string?> LoginAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}