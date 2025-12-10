using System.Security.Cryptography;
using System.Text;
using Gestion.Data;
using Gestion.Models;
using Gestion.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User> RegisterUserAsync(RegisterUserDto dto)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email ya registrado");

            var rolExists = await _appDbContext.Roles.AnyAsync(r => r.Id == dto.RolId);
            if (!rolExists)
                throw new Exception("No se puede crear el usuario porque el rol no existe");

            var user = new User
            {
                Email = dto.Email,
                Nombre = dto.Nombre,
                Password = HashPassword(dto.Password),
                RolId = dto.RolId
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(RegisterUserDto dto)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                throw new Exception("Usuario no encontrado");

            user.Nombre = string.IsNullOrWhiteSpace(dto.Nombre) ? user.Nombre : dto.Nombre;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = HashPassword(dto.Password);

            if (dto.RolId != 0 && dto.RolId != user.RolId)
            {
                var rolExists = await _appDbContext.Roles.AnyAsync(r => r.Id == dto.RolId);
                if (!rolExists)
                    throw new Exception("No se puede actualizar el usuario porque el rol no existe");
                user.RolId = dto.RolId;
            }

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new Exception("Email no encontrado");

            var hashed = HashPassword(password);

            if (user.Password != hashed)
                throw new Exception("Contraseña incorrecta");

            return user;
        }

        public async Task<User> UpdateEmailAsync(string oldEmail, string newEmail)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == oldEmail);

            if (user == null)
                throw new Exception("Usuario no encontrado");

            if (await _appDbContext.Users.AnyAsync(u => u.Email == newEmail))
                throw new Exception("El nuevo email ya está en uso");

            user.Email = newEmail;

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new Exception("Usuario no encontrado");

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
