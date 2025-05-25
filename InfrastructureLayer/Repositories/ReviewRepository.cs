using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    //Ansvarar för databasoperationer som rör recensioner.
    public class ReviewRepository : IReviewRepository
    {
        private readonly MediaFlixDbContext _context;

        public ReviewRepository(MediaFlixDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> CreateAsync(Review review)
        {
            // Kontroll: användaren får bara recensera sin egen media
            var ownedMedia = await _context.Media.FirstOrDefaultAsync(media => media.Id == review.MediaId && media.UserId == review.UserId);
            if (ownedMedia == null)
                return null;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        //Hämtar alla recensioner för ett visst mediaobjekt, inklusive användarinformation.
        public async Task<IEnumerable<Review>> GetByMediaIdAsync(int mediaId)
        {
            return await _context.Reviews
                .Include(review => review.User)
                .Where(review => review.MediaId == mediaId)
                .ToListAsync();
        }

        //Tar bort en recension, men endast om den tillhör den aktuella användaren.
        public async Task<bool> DeleteAsync(int reviewId, int userId)
        {
            var reviewToDelete = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == reviewId && review.UserId == userId);
            if (reviewToDelete == null)
                return false;

            _context.Reviews.Remove(reviewToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
