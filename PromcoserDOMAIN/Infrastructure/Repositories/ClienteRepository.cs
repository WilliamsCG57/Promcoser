using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using PromcoserDOMAIN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromcoserDOMAIN.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly PromcoserDbContext _context;

        public ClienteRepository(PromcoserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Cliente.ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                throw new Exception("Customer not found");
            }
            return cliente;
        }

        public async Task Insert(Cliente cliente)
        {
            await _context.Cliente.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            int countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }
            _context.Cliente.Remove(cliente);
            int countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

    }
}
