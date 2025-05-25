using ApplicationLayer.DTOs;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace ApplicationLayer.Services
{
    public class MediaService
    {
        private readonly IMediaRepository _mediaRepository;

        // Konstruktor som tar in repository via Dependency Injection
        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        // Hämtar alla media från databasen och mappar till MediaDto
        public async Task<IEnumerable<MediaDto>> GetAllAsync(int userId)
        {
            var mediaList = await _mediaRepository.GetAllByUserAsync(userId);

            return mediaList.Select(m => new MediaDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                Creator = m.Creator,
                Type = m.Type,
                Status = m.Status,
                CreatedAt = m.CreatedAt
            });
        }

        // Hämtar en specifik media-post baserat på Id
        public async Task<MediaDto?> GetByIdAsync(int id, int userId)
        {
            var media = await _mediaRepository.GetByIdForUserAsync(id, userId);
            if (media == null) return null;

            return new MediaDto
            {
                Id = media.Id,
                Title = media.Title,
                Genre = media.Genre,
                Creator = media.Creator,
                Type = media.Type,
                Status = media.Status,
                CreatedAt = media.CreatedAt
            };
        }

        // Skapar en ny media-post baserat på data från CreateMediaDto
        public async Task<MediaDto> CreateAsync(CreateMediaDto dto)
        {
            var media = new Media
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Creator = dto.Creator,
                Type = dto.Type,
                Status = dto.Status
            };

            var created = await _mediaRepository.CreateAsync(media);

            return new MediaDto
            {
                Id = created.Id,
                Title = created.Title,
                Genre = created.Genre,
                Creator = created.Creator,
                Type = created.Type,
                Status = created.Status,
                CreatedAt = created.CreatedAt
            };
        }

        //uppdaterar med användare ID
        public async Task UpdateAsync(int id, UpdateMediaDto dto, int userId)
        {
            var media = await _mediaRepository.GetByIdForUserAsync(id, userId);
            if (media == null)
                throw new Exception("Media not found or access denied");

            media.Title = dto.Title;
            media.Genre = dto.Genre;
            media.Creator = dto.Creator;
            media.Type = dto.Type;
            media.Status = dto.Status;

            await _mediaRepository.UpdateForUserAsync(media, userId);
        }

        //tar bort media med användareID
        public async Task DeleteAsync(int id, int userId)
        {
            await _mediaRepository.DeleteForUserAsync(id, userId);
        }
    }
}
