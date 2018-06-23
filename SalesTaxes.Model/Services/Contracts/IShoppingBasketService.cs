using SalesTaxes.Model.Entities;

namespace SalesTaxes.Model.Services.Contracts
{
    public interface IShoppingBasketService
    {
        Receipt Checkout(Item[] items);
    }
}