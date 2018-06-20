using System;

namespace SalesTaxes.Model.Entities
{
    public class TaxesCalculator
    {
        public decimal CalculateTaxes(Item item)
        {
            return item.TypeOfItem == TypeOfItem.Book 
                || item.TypeOfItem == TypeOfItem.Food
                || item.TypeOfItem == TypeOfItem.Medical
                ? item.Price
                : item.Price + Math.Round(item.Price*0.1m *20)/20;
        }
    }
}
