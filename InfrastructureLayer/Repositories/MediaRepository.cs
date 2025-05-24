using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.Database;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    // Repository som hanterar media-data för specifika användare.
    public class MediaRepository : IMediaRepository
    {
        private readonly MediaFlixDbContext _dbContext;

        public MediaRepository(MediaFlixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Hämtar alla media för en specifik användare.
        public async Task<IEnumerable<Media>> GetAllByUserAsync(int userId)
        {
            return await _dbContext.Media
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        // Hämtar en specifik media-post för en specifik användare.
        public async Task<Media?> GetByIdForUserAsync(int id, int userId)
        {
            return await _dbContext.Media
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
        }

        // Skapar en ny media-post och kopplar den till en användare.
        public async Task<Media> CreateAsync(Media media)
        {
            _dbContext.Media.Add(media);
            await _dbContext.SaveChangesAsync();
            return media;
        }

        // Uppdaterar en befintlig media-post, kontrollerar användartillhörighet.
        public async Task UpdateForUserAsync(Media media, int userId)
        {
            var existing = await GetByIdForUserAsync(media.Id, userId);
            if (existing == null)
                throw new Exception("Media not found or access denied");

            existing.Title = media.Title;
            existing.Genre = media.Genre;
            existing.Creator = media.Creator;
            existing.Type = media.Type;
            existing.Status = media.Status;

            await _dbContext.SaveChangesAsync();
        }

        // Tar bort en media-post för en specifik användare.
        public async Task DeleteForUserAsync(int id, int userId)
        {
            var media = await GetByIdForUserAsync(id, userId);
            if (media != null)
            {
                _dbContext.Media.Remove(media);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
