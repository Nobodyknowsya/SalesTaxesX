using System;
using SalesTaxes;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Model.Services
{
    public class TaxesCalculator : ITaxesCalculator
    {
        private readonly ITaxRateRetriever _taxRateRetriever;

        public TaxesCalculator(ITaxRateRetriever taxRateRetriever)
        {
            if (taxRateRetriever == null) throw new ArgumentNullException(nameof(taxRateRetriever));
            _taxRateRetriever = taxRateRetriever;
        }

        public ITaxRateRetriever TaxRateRetriever { get; set; }

        public decimal CalculateTotalPrice(Item item)
        {
            return IsTaxFree(item)
                ? item.Price
                : item.Price + CalculateItemTaxes(item);
        }

        public decimal CalculateItemTaxes(Item item)
        {
            decimal rate = 0;
            if (item.IsImported)//decorator?
            {
                rate += _taxRateRetriever.GetImportTaxRate();
            }

            if (!IsTaxFree(item))
            {
                rate += _taxRateRetriever.GetTaxRate();
            }

            return Math.Ceiling(item.Price * rate * 20) / 20;
        }

        private bool IsTaxFree(Item item)
        {
            return item.TypeOfItem == TypeOfItem.Book
                   || item.TypeOfItem == TypeOfItem.Food
                   || item.TypeOfItem == TypeOfItem.Medical;
        }
    }
}
