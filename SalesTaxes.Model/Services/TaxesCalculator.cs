using System;
using System.Collections.Generic;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

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

    public class ShoppingBasketService
    {
        private readonly IReceiptBuilder _receiptBuilder;
        private readonly IBasketItemManager _basketItemManager;

        public ShoppingBasketService(IReceiptBuilder receiptBuilder,IBasketItemManager basketItemManager)
        {
            _receiptBuilder = receiptBuilder;
            _basketItemManager = basketItemManager;
        }

        public Receipt Checkout(Item[] items)
        {
            var purchasedItems = new List<PurchasedItem>();
            foreach (var item in items)
            {
                purchasedItems.Add(_basketItemManager.AddToBasket(item));
            }

            return _receiptBuilder.CreateReceipt(purchasedItems.ToArray());
        }
    }
}
