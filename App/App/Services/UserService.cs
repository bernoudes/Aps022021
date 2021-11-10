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
    public class UserService
    {
        private MyDbContext _context;

        public UserService (MyDbContext context)
        {
            _context = context;
        }

        //FIND
        public async Task<List<User>> FindAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        

        //CREATE
        public async Task InsertAsync(User obj)
        {
            _context.User.Add(obj);
            await _context.SaveChangesAsync();
        }

        //UPDATE
        public async Task UpdateAsync(User obj)
        {
            var hasAny = await _context.User.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("id not found");
            }
            try
            {
                _context.User.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        //REMOVE
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.User.FindAsync(id);
                _context.User.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
        ////////////////////////////////////////////////////
        ///
       public async Task FindAndCompareFingerPrinterAsync(User obj)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == obj.Email);      
        }
    }
}
