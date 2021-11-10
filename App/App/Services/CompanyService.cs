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
    public class CompanyService
    {
        private MyDbContext _context;

        public CompanyService(MyDbContext context)
        {
            _context = context;
        }

        //FIND
        public async Task<List<Company>> FindAllAsync()
        {
            return await _context.Company
                .Include(e=> e.CompanyTax).ThenInclude(tax => tax.Tax)
                .Include(e => e.CompanyAgrotoxic).ThenInclude(agrotoxic => agrotoxic.AgroToxic)
                .Include(e => e.CompanyIncentive).ThenInclude(incetive => incetive.Incentive)
                .Include(e => e.CompanyProduct).ThenInclude(product => product.Product)
                .ToListAsync();
        }

        public async Task<Company> FindByIdAsync(int id)
        {
            return await _context.Company
                .Include(e => e.CompanyTax).ThenInclude(tax => tax.Tax)
                .Include(e => e.CompanyAgrotoxic).ThenInclude(agrotoxic => agrotoxic.AgroToxic)
                .Include(e => e.CompanyIncentive).ThenInclude(incetive => incetive.Incentive)
                .Include(e => e.CompanyProduct).ThenInclude(product => product.Product)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //CREATE
        public async Task InsetAsync(Company obj)
        {
            _context.Company.Add(obj);
            await _context.SaveChangesAsync();
        }

        //UPDATE
        public async Task UpdateAsync(Company obj)
        {
            var hasAny = await _context.Company.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("id not found");
            }
            try
            {
                ManyToManyCompanyService many = new(_context);
                many.UpdateCompanyProduct(obj.Id, obj.CompanyProduct);
                many.UpdateCompanyIncentive(obj.Id, obj.CompanyIncentive);
                many.UpdateCompanyTax(obj.Id, obj.CompanyTax);
                many.UpdateCompanyAgroToxic(obj.Id, obj.CompanyAgrotoxic);
                _context.Company.Update(obj);
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
                var obj = await _context.Company.FindAsync(id);
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
