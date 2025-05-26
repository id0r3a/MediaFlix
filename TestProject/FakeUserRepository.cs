using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Interfaces;
using DomainLayer.Models;

namespace MediaFlix.Tests.Fakes
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _idCounter = 1;

        public Task<User> CreateAsync(User user)
        {
            user.Id = _idCounter++;
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return Task.FromResult(user);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }
    }
}
