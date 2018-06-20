using SalesTaxes.Model.Entities;

namespace SalesTaxes.Model.Services.Contracts
{
    public interface IReceiptBuilder
    {
        Receipt CreateReceipt(PurchasedItem[] purchasedItems);
    }
}