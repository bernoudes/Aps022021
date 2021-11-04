using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Data;
using App.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class IncentiveService
    {
        private MyDbContext _context;

        public IncentiveService(MyDbContext context)
        {
            _context = context;
        }

        //FIND
        public async Task<List<Incentive>> FindAllAsync()
        {
            return await _context.Incentive.ToListAsync();
        }

        public async Task<Incentive> FindById(int id)
        {
            return await _context.Incentive.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //Create
        public async Task InsertAsync(Incentive obj)
        {
            _context.Incentive.Add(obj);
            await _context.SaveChangesAsync();
        }

        //UPDATE
        public async Task UpdateAsync(Incentive obj)
        {
            var hasAny = await _context.Incentive.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        //REMOVE
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Incentive.FindAsync(id);
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
