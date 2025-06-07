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
                Description = m.Description,
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
                Description = media.Description,
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
                Description= dto.Description,
                Creator = dto.Creator,
                Type = dto.Type,
                Status = dto.Status,
                UserId = dto.UserId
            };

            var created = await _mediaRepository.CreateAsync(media);

            return new MediaDto
            {
                Id = created.Id,
                Title = created.Title,
                Genre = created.Genre,
                Description = created.Description,
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

            if (!string.IsNullOrEmpty(dto.Title))
                media.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Genre))
                media.Genre = dto.Genre;

            if (dto.Description != null)
                media.Description = dto.Description;

            if (dto.Creator != null)
                media.Creator = dto.Creator;

            if (!string.IsNullOrEmpty(dto.Type))
                media.Type = dto.Type;

            if (!string.IsNullOrEmpty(dto.Status))
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
