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
    // Ansvarar för att hämta och spara användardata i databasen via Entity Framework Core.
    public class UserRepository : IUserRepository
    {
        private readonly MediaFlixDbContext _dbContext;

        // Konstruktor som injicerar databaskontexten.
        public UserRepository(MediaFlixDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Skapar och sparar en ny användare i databasen.
        public async Task<User> CreateAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        // Hämtar en användare baserat på e-postadress.
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        // Hämtar en användare baserat på användarnamn.
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        // Hämtar en användare baserat på ID (primärnyckel).
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
