using Microsoft.EntityFrameworkCore;
using Promcoser.DOMAIN.Core.DTOs;
using Promcoser.DOMAIN.Core.Entities;
using Promcoser.DOMAIN.Core.Interfaces;
using Promcoser.DOMAIN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promcoser.DOMAIN.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PromcoserContext _context;

        public UserRepository(PromcoserContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Usuarios>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuarios> GetUsuarioById(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task Insert(Usuarios usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Update(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            int countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _context.Usuarios.Remove(user);
            int countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

        public async Task<Usuarios> SignIn(string email, string pwd)
        {
            return await _context
                .Usuarios
                .Where(u => u.Email == email && u.Password == pwd)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SignUp(Usuarios user)
        {
            await _context.Usuarios.AddAsync(user);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

    }
}
