using SalesTaxes.Model.Entities;

namespace SalesTaxes.Model.Services.Contracts
{
    public interface IBasketItemManager
    {
        PurchasedItem AddToBasket(Item item);
    }
}