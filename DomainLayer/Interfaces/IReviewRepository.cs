using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> CreateAsync(Review review);
        Task<IEnumerable<Review>> GetByMediaIdAsync(int mediaId);
        Task<bool> DeleteAsync(int reviewId, int userId);
    }
}
