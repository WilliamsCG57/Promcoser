using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using PromcoserDOMAIN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PromcoserDOMAIN.Infrastructure.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly PromcoserDbContext _dbContext;

        public PersonalRepository(PromcoserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Personal> SignIn(string usuario, string pwd)
        {
            return await _dbContext
                    .Personal
                    .Where(u => u.Usuario == usuario && u.Contrasena == pwd)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> SignUp(Personal personal)
        {
            await _dbContext.Personal.AddAsync(personal);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword)
        {
            var personal = await _dbContext.Personal
                .FirstOrDefaultAsync(p => p.Usuario == usuario);

            if (personal == null || personal.Contrasena != oldPassword)
            {
                return false;
            }

            personal.Contrasena = newPassword;
            _dbContext.Personal.Update(personal);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }

}
