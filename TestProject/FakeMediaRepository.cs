using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace MediaFlix.Tests.Fakes
{
    public class FakeMediaRepository : IMediaRepository
    {
        private readonly List<Media> _mediaList = new();
        private int _idCounter = 1;

        public Task<IEnumerable<Media>> GetAllByUserAsync(int userId)
        {
            var media = _mediaList.Where(m => m.UserId == userId);
            return Task.FromResult(media);
        }

        public Task<Media?> GetByIdForUserAsync(int id, int userId)
        {
            var media = _mediaList.FirstOrDefault(m => m.Id == id && m.UserId == userId);
            return Task.FromResult(media);
        }

        public Task<Media> CreateAsync(Media media)
        {
            media.Id = _idCounter++;
            media.CreatedAt = DateTime.UtcNow;
            _mediaList.Add(media);
            return Task.FromResult(media);
        }

        public Task UpdateForUserAsync(Media media, int userId)
        {
            var existing = _mediaList.FirstOrDefault(m => m.Id == media.Id && m.UserId == userId);
            if (existing != null)
            {
                existing.Title = media.Title;
                existing.Genre = media.Genre;
                existing.Creator = media.Creator;
                existing.Type = media.Type;
                existing.Status = media.Status;
            }
            return Task.CompletedTask;
        }

        public Task DeleteForUserAsync(int id, int userId)
        {
            var media = _mediaList.FirstOrDefault(m => m.Id == id && m.UserId == userId);
            if (media != null)
            {
                _mediaList.Remove(media);
            }
            return Task.CompletedTask;
        }
    }
}
