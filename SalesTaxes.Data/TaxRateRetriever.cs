using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Data
{
    public class TaxRateRetriever : ITaxRateRetriever
    {
        public decimal GetTaxRate()
        {
            return 0.1m;
        }

        public decimal GetImportTaxRate()
        {
            return 0.05m;
        }
    }
}
