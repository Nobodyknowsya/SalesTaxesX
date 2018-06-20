using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Model.Services
{
    public class BasketItemManager
    {
        private readonly ITaxesCalculator _taxesCalculator;

        public BasketItemManager(ITaxesCalculator taxesCalculator)
        {
            _taxesCalculator = taxesCalculator;
        }

        public PurchasedItem AddToBasket(Item item)
        {
            var taxes = _taxesCalculator.CalculateItemTaxes(item);

            return new PurchasedItem()
            {
                Description = item.Description,
                FinalPrice = item.Price+taxes,
                SaleTax = taxes
            };
        }
    }
}