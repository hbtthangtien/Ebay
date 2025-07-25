using Application.DTOs;
using Application.Interfaces.IServices;
using CoreLayer.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly CloneEbayDbContext _db;
        public AuthService(CloneEbayDbContext db) => _db = db;

        public async Task<LoginUserDTO?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            return await _db.Set<User>()
                            .Where(u => u.Email == email && u.Password == password)
                            .ProjectToType<LoginUserDTO>()
                            .FirstOrDefaultAsync();
        }

    }
}
