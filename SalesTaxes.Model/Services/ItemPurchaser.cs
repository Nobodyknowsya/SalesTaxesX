using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Model.Services
{
    public class ItemPurchaser
    {
        private readonly ITaxesCalculator _taxesCalculator;

        public ItemPurchaser(ITaxesCalculator taxesCalculator)
        {
            _taxesCalculator = taxesCalculator;
        }

        public PurchasedItem PurchaseItem(Item item)
        {
            var taxes = _taxesCalculator.CalculateItemTaxes(item);

            return new PurchasedItem()
            {
                Description = item.Description,
                Price = item.Price+taxes,
                SaleTax = taxes
            };
        }
    }
}