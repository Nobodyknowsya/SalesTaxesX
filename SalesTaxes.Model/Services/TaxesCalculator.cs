using System;
using SalesTaxes.Model.Entities;

namespace SalesTaxes.Model.Services
{
    public class TaxesCalculator
    {
        public decimal CalculateTotalPrice(Item item)
        {
            return IsTaxFree(item)
                ? item.Price
                : item.Price + CalculateItemTaxes(item);
        }

        public decimal CalculateItemTaxes(Item item)
        {
            decimal rate = 0;
            if (item.IsImported)
            {
                rate += 0.05m;
            }

            if (!IsTaxFree(item))
            {
                rate += 0.1m;
            }
            
            return Math.Round(item.Price * rate * 20) / 20;
        }

        private bool IsTaxFree(Item item)
        {
            return item.TypeOfItem == TypeOfItem.Book
                   || item.TypeOfItem == TypeOfItem.Food
                   || item.TypeOfItem == TypeOfItem.Medical;
        }
    }
}
