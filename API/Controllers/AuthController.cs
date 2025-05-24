using ApplicationLayer.DTOs;
using ApplicationLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        // Konstruktor med injicering av AuthService
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            // Kontrollera att inkommande data är giltig (via valideringsattribut)
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Skickar valideringsfel till klient

            // Försök registrera användare
            var token = await _authService.RegisterAsync(dto);

            // Om token är null betyder det att e-post redan finns
            if (token == null)
                return BadRequest("E-postadressen är redan registrerad.");

            // Returnera JWT-token
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            // Kontrollera att indata är giltig
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Försök logga in användaren
            var token = await _authService.LoginAsync(dto);

            // Returnera 401 om inloggningen misslyckas
            if (token == null)
                return Unauthorized("Fel e-post eller lösenord.");

            // Returnera JWT-token
            return Ok(new { token });
        }

    }
}
