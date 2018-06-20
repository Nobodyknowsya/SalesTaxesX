using System.Collections.Generic;
using System.Linq;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Model.Services
{
    public class ReceiptBuilder: IReceiptBuilder
    {
        public Receipt CreateReceipt(PurchasedItem[] purchasedItems)
        {
            var distinctItems = purchasedItems.GroupBy(i => new {i.Description, i.SaleTax, i.FinalPrice});
            var receiptDetails = new List<ReceiptDetail>();

            foreach (var item in distinctItems)
            {
                var detail = new ReceiptDetail()
                {
                    Quantity = item.Count(),
                    Description = item.Key.Description,
                    Price = item.Sum(i => i.FinalPrice)
                };

                receiptDetails.Add(detail);
            }

            var salesTaxes = purchasedItems.Select(p => p.SaleTax).Sum();
            var total = purchasedItems.Select(p => p.FinalPrice).Sum();

            return new Receipt()
            {
                Details = receiptDetails.ToArray(),
                PurchasedItems = purchasedItems,
                SalesTaxes = salesTaxes,
                Total = total
            };
        }
    }
}