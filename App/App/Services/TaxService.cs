using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class TaxService
    {
        private MyDbContext _context;

        public TaxService( MyDbContext context)
        {
            _context = context;
        }

        //FIND
        public async Task<List<Tax>> FindAllAsync()
        {
            return await _context.Tax.ToListAsync();
        }

        public async Task<Tax> FindById(int id)
        {
            return await _context.Tax.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //Create
        public async Task InsertAsync(Tax obj)
        {
            _context.Tax.Add(obj);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(Tax obj)
        {
            var hasAny = await _context.Tax.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Tax.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        //REMOVER
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Tax.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
