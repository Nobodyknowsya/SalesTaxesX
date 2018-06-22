using System;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Model.Services
{
    public class BasketItemManager : IBasketItemManager
    {
        private readonly ITaxesCalculator _taxesCalculator;

        public BasketItemManager(ITaxesCalculator taxesCalculator)
        {
            if (taxesCalculator == null) throw new ArgumentNullException(nameof(taxesCalculator));
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