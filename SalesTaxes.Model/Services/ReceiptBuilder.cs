using System.Linq;
using SalesTaxes.Model.Entities;

namespace SalesTaxes.Model.Services
{
    public class ReceiptBuilder
    {
        public Receipt CreateReceipt(PurchasedItem[] purchasedItems)
        {
            var salesTaxes = purchasedItems.Select(p => p.SaleTax).Sum();
            var total = purchasedItems.Select(p => p.Price).Sum();

            return new Receipt()
            {
                PurchasedItems = purchasedItems,
                SalesTaxes = salesTaxes,
                Total = total
            };
        }
    }
}