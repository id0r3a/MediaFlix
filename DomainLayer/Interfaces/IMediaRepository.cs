using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    public interface IMediaRepository
    {
        Task<IEnumerable<Media>> GetAllByUserAsync(int userId);
        Task<Media?> GetByIdForUserAsync(int id, int userId);
        Task<Media> CreateAsync(Media media);
        Task UpdateForUserAsync(Media media, int userId);
        Task DeleteForUserAsync(int id, int userId);
    }
}
