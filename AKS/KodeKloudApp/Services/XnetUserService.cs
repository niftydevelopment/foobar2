using KodeKloudApp.Data;
using KodeKloudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KodeKloudApp.Services
{
    public class XnetUserService
    {
        private readonly XnetDbContext _context;

        public XnetUserService(XnetDbContext context)
        {
            _context = context;
        }

        public async Task<List<AspNetUser>> GetAllUsersAsync()
        {
            return await _context.AspNetUsers.ToListAsync();
        }

        public async Task<AspNetUser?> GetUserByIdAsync(string id)
        {
            return await _context.AspNetUsers.FindAsync(id);
        }

        public async Task<AspNetUser> CreateUserAsync(AspNetUser user)
        {
            _context.AspNetUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AspNetUser> CreateUserAsync(string firstName, string lastName, string countryCode, string nationalNumber, string? email = null)
        {
            var user = new AspNetUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
                CountryCode = countryCode,
                NationalNumber = nationalNumber,
                Email = email,
                UserName = email ?? $"{firstName.ToLower()}.{lastName.ToLower()}",
                NormalizedUserName = (email ?? $"{firstName.ToLower()}.{lastName.ToLower()}").ToUpper(),
                NormalizedEmail = email?.ToUpper(),
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            return await CreateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _context.AspNetUsers.FindAsync(id);
            if (user == null)
                return false;

            _context.AspNetUsers.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.AspNetUsers.CountAsync();
        }
    }
}
