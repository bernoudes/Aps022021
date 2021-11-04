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
    public class ProductService
    {
        private MyDbContext _context;
        
        public ProductService(MyDbContext context)
        {
            _context = context;
        }

        //FIND
        public async Task<List<Product>> FindAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        
        //CREATE
        public async Task InsertAsync(Product obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //UPDATA
        public async Task UpdateAsync(Product obj)
        {
            var hasAny = await _context.Product.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _context.Update(obj);
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
                var obj = await _context.Product.FindAsync(id);
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
