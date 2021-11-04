using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.ViewModel
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }

        public List<CheckBoxViewModel> AgroToxicCheck { get; set; } = new List<CheckBoxViewModel>();
        public List<CheckBoxViewModel> TaxCheck { get; set; } = new List<CheckBoxViewModel>();
        public List<CheckBoxViewModel> IncentiveCheck { get; set; } = new List<CheckBoxViewModel>();
        public List<CheckBoxViewModel> ProductCheck { get; set; } = new List<CheckBoxViewModel>();

        public ICollection<AgroToxic> AgroToxic { get; set; }
        public ICollection<Tax> Tax { get; set; }
        public ICollection<Incentive> Incentive { get; set; }
        public ICollection<Product> Product { get; set; }

        public void SetValuesOfCheckinCompany()
        {
            //PRODUCT
            Company.CompanyProduct = new List<CompanyProduct>();
            for (int i =0; i < ProductCheck.Count; i++)
            {
                if(ProductCheck[i].isChecked == true)
                {
                    Company.CompanyProduct.Add(new CompanyProduct
                    {
                        Product_id = ProductCheck[i].Id,
                        Company_id = Company.Id
                    });
                }
            }
            //AGROTOXIC
            Company.CompanyAgrotoxic = new List<CompanyAgrotoxic>();
            for (int i = 0; i < AgroToxicCheck.Count; i++)
            {
                if (AgroToxicCheck[i].isChecked == true)
                {
                    Company.CompanyAgrotoxic.Add(new CompanyAgrotoxic
                    {
                        AgroToxic_id = AgroToxicCheck[i].Id,
                        Company_id = Company.Id
                    });
                }
            }
            //TAX
            Company.CompanyTax = new List<CompanyTax>();
            for (int i = 0; i < TaxCheck.Count; i++)
            {
                if (TaxCheck[i].isChecked == true)
                {
                    Company.CompanyTax.Add(new CompanyTax
                    {
                        Tax_id = TaxCheck[i].Id,
                        Company_id = Company.Id
                    });
                }
            }
            //INCENTIVE
            Company.CompanyIncentive = new List<CompanyIncentive>();
            for (int i = 0; i < IncentiveCheck.Count; i++)
            {
                if (IncentiveCheck[i].isChecked == true)
                {
                    Company.CompanyIncentive.Add(new CompanyIncentive
                    {
                        Incentive_id = IncentiveCheck[i].Id,
                        Company_id = Company.Id
                    });
                }
            }

        }
        public void SetValuesInCheck()
        {
            //PRODUCT
            foreach(var product in Product)
            {
                ProductCheck.Add(new CheckBoxViewModel() { isChecked = false });
                ProductCheck[ProductCheck.Count - 1].Id = product.Id;
                foreach (var item in Company.CompanyProduct)
                {         
                    if (product.Id == item.Product_id)
                    {
                        ProductCheck[ProductCheck.Count - 1].isChecked=true;
                    }
                }
            }
            //AGROTOXIC
            foreach (var agroToxic in AgroToxic)
            {
                AgroToxicCheck.Add(new CheckBoxViewModel() { isChecked = false });
                AgroToxicCheck[AgroToxicCheck.Count - 1].Id = agroToxic.Id;
                foreach (var item in Company.CompanyAgrotoxic)
                { 
                    if (agroToxic.Id == item.AgroToxic_id)
                    {
                        AgroToxicCheck[AgroToxicCheck.Count - 1].isChecked = true;
                    }
                }
            }
            //TAX
            foreach (var tax in Tax)
            {
                TaxCheck.Add(new CheckBoxViewModel() { isChecked = false });
                TaxCheck[TaxCheck.Count - 1].Id = tax.Id;
                foreach (var item in Company.CompanyTax)
                {
                    if (tax.Id == item.Tax_id)
                    {
                        TaxCheck[TaxCheck.Count - 1].isChecked = true;
                    }
                }
            }
            //INCENTIVE
            foreach (var incentive in Incentive)
            {
                IncentiveCheck.Add(new CheckBoxViewModel() { isChecked = false });
                IncentiveCheck[IncentiveCheck.Count - 1].Id = incentive.Id;
                foreach (var item in Company.CompanyIncentive)
                {
                    if (incentive.Id == item.Incentive_id)
                    {
                        IncentiveCheck[IncentiveCheck.Count - 1].isChecked = true;
                    }
                }
            }
        }
    }
}
