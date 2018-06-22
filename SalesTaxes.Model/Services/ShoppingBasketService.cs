using System;
using System.Collections.Generic;
using SalesTaxes.Model.Entities;
using SalesTaxes.Model.Services.Contracts;

namespace SalesTaxes.Model.Services
{
    public class ShoppingBasketService
    {
        private readonly IReceiptBuilder _receiptBuilder;
        private readonly IBasketItemManager _basketItemManager;

        public ShoppingBasketService(IReceiptBuilder receiptBuilder,IBasketItemManager basketItemManager)
        {
            if (receiptBuilder == null) throw new ArgumentNullException(nameof(receiptBuilder));
            if (basketItemManager == null) throw new ArgumentNullException(nameof(basketItemManager));

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