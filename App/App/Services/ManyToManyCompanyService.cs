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
    public class ManyToManyCompanyService
    {
        private readonly MyDbContext _context;

        public ManyToManyCompanyService(MyDbContext context)
        {
            _context = context;
        }

        public void UpdateCompanyProduct(int Company_id, ICollection<CompanyProduct> product)
        {
            var list = _context.CompanyProduct.Where(x => x.Company_id == Company_id).ToList();
            _context.Set<CompanyProduct>().RemoveRange(list);
            _context.Set<CompanyProduct>().AddRange(product);
        }
        public void UpdateCompanyAgroToxic(int Company_id, ICollection<CompanyAgrotoxic> agroToxic)
        {
            var list = _context.CompanyAgrotoxic.Where(x => x.Company_id == Company_id).ToList();
            _context.Set<CompanyAgrotoxic>().RemoveRange(list);
            _context.Set<CompanyAgrotoxic>().AddRange(agroToxic);
        }
        public void UpdateCompanyIncentive(int Company_id, ICollection<CompanyIncentive> incentive)
        {
            var list = _context.CompanyIncentive.Where(x => x.Company_id == Company_id).ToList();
            _context.Set<CompanyIncentive>().RemoveRange(list);
            _context.Set<CompanyIncentive>().AddRange(incentive);
        }
        public void UpdateCompanyTax(int Company_id, ICollection<CompanyTax> tax)
        {
            var list = _context.CompanyTax.Where(x => x.Company_id == Company_id).ToList();
            _context.Set<CompanyTax>().RemoveRange(list);
            _context.Set<CompanyTax>().AddRange(tax);
        }
    }
}
