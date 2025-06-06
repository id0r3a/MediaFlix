using ApplicationLayer.DTOs;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMediaRepository _mediaRepository;

        public ReviewService(IReviewRepository reviewRepository, IMediaRepository mediaRepository)
        {
            _reviewRepository = reviewRepository;
            _mediaRepository = mediaRepository;
        }

        //Skapar en ny recension för angiven media som tillhör användaren.
        public async Task<ReviewDto?> CreateAsync(CreateReviewDto input, int userId)
        {
            //  Hämta tillhörande media (bok)
            var media = await _mediaRepository.GetByIdForUserAsync(input.MediaId, userId);

            //  Om det är en bok och inte redan 'Read', uppdatera status
            if (media != null &&
                media.Type?.ToLower() == "book" &&
                media.Status?.ToLower() != "read")
            {
                media.Status = "Read";
                await _mediaRepository.UpdateForUserAsync(media, userId);
            }

            //  Spara recensionen efter att boken markerats som "Read"
            var reviewToCreate = new Review
            {
                Rating = input.Rating,
                Comment = input.Comment,
                MediaId = input.MediaId,
                UserId = userId
            };

            var savedReview = await _reviewRepository.CreateAsync(reviewToCreate);
            if (savedReview == null) return null;

            return new ReviewDto
            {
                Id = savedReview.Id,
                Rating = savedReview.Rating,
                Comment = savedReview.Comment,
                MediaId = savedReview.MediaId,
                UserId = savedReview.UserId,
                Username = savedReview.User?.Username ?? ""
            };
        }


        //Hämtar alla recensioner kopplade till ett specifikt media.
        public async Task<IEnumerable<ReviewDto>> GetByMediaIdAsync(int mediaId)
        {
            var reviewList = await _reviewRepository.GetByMediaIdAsync(mediaId);

            return reviewList.Select(review => new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                MediaId = review.MediaId,
                UserId = review.UserId,
                Username = review.User?.Username ?? ""
            });
        }

        //Tar bort en recension om den tillhör användaren.
        public async Task<bool> DeleteAsync(int reviewId, int userId)
        {
            return await _reviewRepository.DeleteAsync(reviewId, userId);
        }
    }
}
