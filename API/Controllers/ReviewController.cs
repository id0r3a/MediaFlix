using ApplicationLayer.DTOs;
using ApplicationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("reviews")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // POST /reviews
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _reviewService.CreateAsync(dto, userId);

            if (result == null)
                return Forbid();

            return Ok(result);
        }

        // GET /reviews/media/{id}
        [HttpGet("media/{id}")]
        public async Task<IActionResult> GetByMediaId(int id)
        {
            var reviews = await _reviewService.GetByMediaIdAsync(id);
            return Ok(reviews);
        }

        // DELETE /reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var success = await _reviewService.DeleteAsync(id, userId);

            if (!success)
                return Forbid("You can only delete your own reviews.");

            return NoContent();
        }
    }
}
