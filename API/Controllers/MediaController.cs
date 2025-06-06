using ApplicationLayer.DTOs;
using ApplicationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class MediaController : ControllerBase
    {
        private readonly MediaService _mediaService;

        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        // GET /Hämta alla
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetCurrentUserId();
            var media = await _mediaService.GetAllAsync(userId);
            return Ok(media);
        }

        // GET /hämta med id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var media = await _mediaService.GetByIdAsync(id, userId);
            if (media == null)
                return NotFound();

            return Ok(media);
        }

        // POST /skapar ny media-post
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMediaDto dto)
        {
            var userId = GetCurrentUserId();
            dto.UserId = userId; // användar-ID från token
            Console.WriteLine("UserId från token: " + userId);


            var created = await _mediaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMediaDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _mediaService.UpdateAsync(id, dto, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            await _mediaService.DeleteAsync(id, userId);
            return NoContent();
        }
        //  Hämta korrekt användar-ID från JWT-token
        private int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token");

            return int.Parse(userIdClaim.Value);
        }
    }
}
