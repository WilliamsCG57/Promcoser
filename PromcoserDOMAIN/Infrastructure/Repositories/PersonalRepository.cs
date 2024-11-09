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

        public async Task<Personal> SignIn(string email, string pwd)
        {
            return await _dbContext
                    .Personal
                    .Where(u => u.Usuario == email && u.Contrasena == pwd)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> SignUp(Personal personal)
        {
            await _dbContext.Personal.AddAsync(personal);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }


    }

}
