using System;

namespace SalesTaxes.Model.Entities
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
            return IsTaxFree(item)
                ? 0
                :Math.Round(item.Price * 0.1m * 20) / 20;
        }

        private bool IsTaxFree(Item item)
        {
            return item.TypeOfItem == TypeOfItem.Book
                   || item.TypeOfItem == TypeOfItem.Food
                   || item.TypeOfItem == TypeOfItem.Medical;
        }
    }
}
