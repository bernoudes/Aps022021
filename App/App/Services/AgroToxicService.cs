using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using App.Services.Exceptions;

namespace App.Services
{
    public class AgroToxicService
    {

        //BASIC CRUD FOR AGROTOXIC
        private readonly MyDbContext _context;

        public AgroToxicService(MyDbContext context)
        {
            _context = context;
        }

   
        public async Task<AgroToxic> FindByIdAsync( int id )
        {
            return await _context.AgroToxic.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<List<AgroToxic>> FindAllAsync()
        {
            return await _context.AgroToxic.ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.AgroToxic.FindAsync(id);
                _context.AgroToxic.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task InsertAsync(AgroToxic obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AgroToxic obj)
        {
            var hasAny = await _context.AgroToxic.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

    }
}
